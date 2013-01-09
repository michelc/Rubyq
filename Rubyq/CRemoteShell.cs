using System;
using System.Diagnostics;
using System.Text;

namespace Rubyq
{
    public class CRemoteShell
    {
        private Process _process;
        public event DataReceivedEventHandler StdOut = null;
        public event DataReceivedEventHandler StdError = null;
        public event EventHandler StdExit = null;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(string command, string arguments, string dir)
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
                    StandardErrorEncoding = Encoding.UTF8,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            _process.ErrorDataReceived += StdError;
            _process.OutputDataReceived += StdOut;
            _process.Exited += StdExit;
            _process.Start();
            _process.PriorityClass = ProcessPriorityClass.AboveNormal;

            if (StdOut != null)
                _process.BeginOutputReadLine();

            if (StdError != null)
                _process.BeginErrorReadLine();
        }

        public bool HasExited()
        {
            // _process.WaitForExit();
            return _process.HasExited;
        }

        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(string command)
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
        public void Terminate()
        {
            if (_process == null)
                return;

            if (_process.HasExited)
            {
                StdOut = null;
                StdError = null;
                return;
            }

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
