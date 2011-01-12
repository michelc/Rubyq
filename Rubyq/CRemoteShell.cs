using System;
using System.Diagnostics;
using System.Text;

namespace Rubyq
{
    public static class CRemoteShell
    {
        private static Process _process;
        public static event DataReceivedEventHandler StdOut = null;
        public static event DataReceivedEventHandler StdError = null;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize(string command, string arguments, string dir)
        {
            if (StdOut == null && StdError == null)
                return;

            string comspec;
            try
            {
                comspec = Environment.GetEnvironmentVariable("comspec");
            }
            catch
            {
                return;
            }

            if (string.IsNullOrEmpty(comspec))
                return;

            _process = new Process
            {
                StartInfo =
                {
                    FileName = command,
                    Arguments = arguments,
                    WorkingDirectory = dir,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            _process.ErrorDataReceived += StdError;
            _process.OutputDataReceived += StdOut;
            _process.Start();

            if (StdError != null)
                _process.BeginErrorReadLine();

            if (StdOut != null)
                _process.BeginOutputReadLine();
        }

        public static bool HasExited()
        {
            // _process.WaitForExit();
            return _process.HasExited;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public static void Execute(string command)
        {
            if (_process == null || (StdOut == null && StdError == null) || _process.HasExited)
            {
                if (_process != null)
                    Terminate();

                // Initialize();
            }

            if (_process != null)
                _process.StandardInput.WriteLine(command);
        }

        /// <summary>
        /// Terminates the process tree (including the parent and the children).
        /// </summary>
        public static void Terminate()
        {
            if (_process == null || _process.HasExited)
                return;

            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                try
                {
                    var pfc = new PerformanceCounter("Process", "Creating Process Id", process.ProcessName);
                    if ((int)pfc.RawValue == _process.Id)
                        process.Kill();
                }
                catch { continue; /* Move on to the next process. */}
            }

            _process.Kill();
            _process.Close();
            _process = null;
        }
    }
}
