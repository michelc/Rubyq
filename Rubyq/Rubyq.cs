﻿using System;
using System.Diagnostics;
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
            editor = new QEditor(InputArea);

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
            
            // Always save current text as temp file for next running
            File.WriteAllText(RubyqFile, editor.Text, Encoding.UTF8);

            // Ask for saving if current text has changed
            // (user can cancel the closing)
            e.Cancel = !DoYouWantToSave();
        }

        /// <summary>
        /// Keyboard shorcuts
        /// </summary>
        private void Rubyq_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        if (Run.Enabled) FileRun();
                        break;
                    case Keys.N:
                        if (New.Enabled) FileNew();
                        break;
                    case Keys.O:
                        if (Open.Enabled) FileOpen(null);
                        break;
                    case Keys.S:
                        if (Save.Enabled) FileSave();
                        break;
                    case Keys.V:
                        if (editor.Enabled)
                        {
                            if (Clipboard.ContainsText())
                            {
                                string text = Clipboard.GetText(TextDataFormat.Text);
                                editor.Paste(text);
                            }
                            e.Handled = true;
                        }
                        break;
                }
            }
        }

        private void Run_Click(object sender, EventArgs e)
        {
            FileRun();
        }

        private void New_Click(object sender, EventArgs e)
        {
            FileNew();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            FileOpen(null);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void InputArea_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files.Length > 0)
                {
                    FileOpen(files[0]);
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                var text = e.Data.GetData(DataFormats.Text) as string;
                editor.Paste(text);
            }
        }

        private void InputArea_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void InputArea_KeyUp(object sender, KeyEventArgs e)
        {
            ResetPosition();
        }

        private void InputArea_MouseUp(object sender, MouseEventArgs e)
        {
            ResetPosition();
        }

        private void InputArea_TextChanged(object sender, EventArgs e)
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
            // var encoding = Encoding.GetEncoding(1252);
            File.WriteAllText(RubyqFile, editor.Text, Encoding.UTF8);

            // Create a batch file to run current code
            var cmd = "@echo off\ntitle {1} {2}\nchcp 1252 > null\ncd {0}\n{1} {2}\npause > null";
            var bat = string.Format(cmd, RubyDirectory, RubyExecutable, RubyqFile);
            File.WriteAllText(RubyqBat, bat.ToString(), Encoding.ASCII);

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

        private void FileOpen(string FileName)
        {
            editor.Focus();

            // Ask for saving if current text has changed
            if (!DoYouWantToSave())
            {
                // User has cancelled
                return;
            }

            // Display open file dialog
            if (string.IsNullOrEmpty(FileName))
            {
                OpenFile.FileName = string.Empty;
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    FileName = OpenFile.FileName;
                }
            }

            // Load the selected file
            LoadFileText(FileName);
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
            New.Enabled = enabled;
            Open.Enabled = enabled;
            Save.Enabled = enabled;
            if (enabled)
            {
                ResetTitle();
                editor.Focus();
            }
            InputArea.Refresh();
        }

        /// <summary>
        /// Display cursor position
        /// </summary>
        private void ResetPosition()
        {
            Position.Text = editor.Line.ToString() + " x " + editor.Col.ToString();
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
            ResetPosition();
        }
    }
}