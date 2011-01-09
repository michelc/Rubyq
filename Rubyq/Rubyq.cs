using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Rubyq
{
    public partial class Rubyq : Form
    {
        protected string RubyDirectory;
        protected string RubyExecutable;
        protected string RubyqFile;
        protected string RubyqBat;

        protected QEditor editor;

        protected bool HasChanged;

        public Rubyq()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Application start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rubyq_Load(object sender, EventArgs e)
        {
            // RichTextBox encapsulation
            editor = new QEditor(TextArea);

            // Ruby directory on my PC
            RubyDirectory = @"c:\Ruby";

            // Ruby executable on my PC
            RubyExecutable = Path.Combine(RubyDirectory, @"bin\ruby.exe");

            // File name to save current code
            RubyqFile = Path.Combine(RubyDirectory, "Rubyq.rb");

            // Batch name to run a ruby shell
            RubyqBat = Path.Combine(RubyDirectory, "Rubyq.bat");

            // Reload text from last running
            LoadFileText(RubyqFile);
        }

        /// <summary>
        /// Application end
        /// </summary>
        private void Rubyq_FormClosing(object sender, FormClosingEventArgs e)
        {
            editor.Focus();

            // Ask for saving if current text has changed
            // (user can cancel the closing)
            e.Cancel = !DoYouWantToSave();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            FileRun();
        }

        private void Shell_Click(object sender, EventArgs e)
        {
            FileShell();
        }

        private void New_Click(object sender, EventArgs e)
        {
            FileNew();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            if (!HasChanged)
            {
                HasChanged = true;
                ResetTitle();
            }
            else if (!Save.Enabled)
            {
                ResetTitle();
            }
            else if (!Run.Enabled)
            {
                ResetTitle();
            }
        }

        /// <summary>
        /// Execute current code
        /// </summary>
        private void FileRun()
        {
            editor.Focus();

            // Freeze UI
            ResetForm(false);

            // Save current code in temp file
            File.WriteAllText(RubyqFile, editor.Text, Encoding.UTF8);

            // Execute current code
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.WorkingDirectory = RubyDirectory;
            p.StartInfo.FileName = RubyExecutable;
            p.StartInfo.Arguments = "-Ku " + RubyqFile;
            p.Start();

            // Display results
            string output = p.StandardOutput.ReadToEnd();
            output = output.Replace("\r\n", "\r");
            output = output.Replace("\r", Environment.NewLine);
            OutputArea.ForeColor = SystemColors.WindowText;
            OutputArea.AppendText(output);

            // Display bugs
            string error = p.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                if (OutputArea.TextLength > 0)
                {
                    OutputArea.AppendText(Environment.NewLine);
                }
                error = Environment.NewLine + error.Replace("\n", Environment.NewLine);
                error = error.Replace(Environment.NewLine + RubyqFile + ":", Environment.NewLine + "Line ");
                error = error.Replace(Environment.NewLine + RubyqFile.Replace(@"\", "/") + ":", Environment.NewLine + "Line ");
                OutputArea.SelectionColor = Color.Red;
                OutputArea.AppendText(error);
            }

            // Fits console display
            OutputArea.SelectionStart = OutputArea.TextLength;
            OutputArea.ScrollToCaret();

            // Unfreeze UI
            ResetForm(true);
        }

        private void FileShell()
        {
            editor.Focus();

            // Freeze UI
            ResetForm(false);

            // Save current code in temp file (with no diacritics)
            byte[] bytes = Encoding.GetEncoding(1251).GetBytes(editor.Text);
            string text = Encoding.ASCII.GetString(bytes);
            File.WriteAllText(RubyqFile, text, Encoding.ASCII);

            // Create a batch file to run current code
            var cmd = "@echo off\ntitle {1} -Ku {2}\ncd {0}\n{1} -Ku {2}\n@pause > null";
            var bat = string.Format(cmd, RubyDirectory, RubyExecutable, RubyqFile);
            System.IO.File.WriteAllText(RubyqBat, bat.ToString(), Encoding.ASCII);

            // Execute batch
            Process p = new Process();
            p.StartInfo.WorkingDirectory = RubyDirectory;
            p.StartInfo.FileName = RubyqBat;
            p.Start();
            p.WaitForExit();
            
            // Unfreeze UI
            ResetForm(true);
        }

        private void FileNew()
        {
            editor.Focus();

            // Ask for saving if text has changed
            if (!DoYouWantToSave())
            {
                // User has cancelled
                return;
            }

            // Load an empty file
            LoadFileText(string.Empty);
        }

        private void FileOpen()
        {
            editor.Focus();

            // Ask for saving if current text has changed
            if (!DoYouWantToSave())
            {
                // User has cancelled
                return;
            }

            // Display open file dialog
            OpenFile.FileName = string.Empty;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                // Load the selected file
                LoadFileText(OpenFile.FileName);
            }
        }

        private void FileSave()
        {
            editor.Focus();

            // Ask for a file name if current file has no one
            if (string.IsNullOrEmpty(editor.FileName))
            {
                // Displays save file dialog
                SaveFile.FileName = string.Empty;
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    // 
                    editor.FileName = SaveFile.FileName;
                }
            }

            // Save with current file name
            if (!string.IsNullOrEmpty(editor.FileName))
            {
                // Write current text
                File.WriteAllText(editor.FileName, editor.Text, Encoding.UTF8);
                // And reload it (for UI updates)
                LoadFileText(editor.FileName);
            }
        }

        /// <summary>
        /// Displays window title
        /// </summary>
        private void ResetTitle()
        {
            string title = "Rubyq - ";
            if (string.IsNullOrEmpty(editor.FileName))
            {
                title += "Untitled";
            }
            else
            {
                title += editor.FileName;
            }
            if (HasChanged)
            {
                title += " *";
            }
            this.Text = title;
            Run.Enabled = !editor.IsEmpty;
            Shell.Enabled = !editor.IsEmpty;
            Save.Enabled = !editor.IsOriginal && !editor.IsEmpty;
        }

        /// <summary>
        /// Freeze or unfreeze UI before or after Run & Shell
        /// </summary>
        /// <param name="enabled">true = freeze / false = unfreeze</param>
        private void ResetForm(bool enabled)
        {
            editor.Enabled = enabled;
            Run.Enabled = enabled;
            Shell.Enabled = enabled;
            New.Enabled = enabled;
            Open.Enabled = enabled;
            Save.Enabled = enabled;
            if (enabled)
            {
                ResetTitle();
                editor.Focus();
            }
            else
            {
                OutputArea.Clear();
            }
            TextArea.Refresh();
        }

        /// <summary>
        /// Ask user wether to save current text (only when it has changed)
        /// </summary>
        /// <returns>false when user has cancelled</returns>
        private bool DoYouWantToSave()
        {
            if (!editor.IsOriginal)
            {
                var message = "Do you want to save <" + editor.FileName + "> file?";
                if (string.IsNullOrEmpty(editor.FileName))
                {
                    message = "Do you want to save the current text in a new file?";
                }
                var response = MessageBox.Show(message, "Text Modified", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (response == DialogResult.Cancel)
                {
                    return false;
                }
                if (response == DialogResult.Yes)
                {
                    Save_Click(null, null);
                }
            }
            return true;
        }

        /// <summary>
        /// Load a file
        /// </summary>
        /// <param name="FileName">The file name or an empty string to clear text editor</param>
        private void LoadFileText(string FileName)
        {
            string text = string.Empty;
            if (string.IsNullOrEmpty(FileName))
            {
                // No file name, no content
                text = string.Empty;
            }
            else if (File.Exists(FileName))
            {
                // Really load a file content
                text = File.ReadAllText(FileName, Encoding.UTF8);
                if (FileName == RubyqFile)
                {
                    // Remove file name when temp file name
                    FileName = string.Empty;
                }
            }
            else
            {
                // Remove file name when file not found
                FileName = string.Empty;
            }

            // Load editor with content
            editor.Load(text);
            
            // Update UI
            editor.FileName = FileName;
            HasChanged = false;
            ResetTitle();
        }

    }
}