namespace Rubyq
{
    partial class Rubyq
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.Canvas = new System.Windows.Forms.SplitContainer();
            this.Position = new System.Windows.Forms.Label();
            this.Shell = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Open = new System.Windows.Forms.Button();
            this.New = new System.Windows.Forms.Button();
            this.InputArea = new System.Windows.Forms.RichTextBox();
            this.OutputArea = new System.Windows.Forms.RichTextBox();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.SaveFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.Canvas.Panel1.SuspendLayout();
            this.Canvas.Panel2.SuspendLayout();
            this.Canvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Margin = new System.Windows.Forms.Padding(6);
            this.Canvas.Name = "Canvas";
            this.Canvas.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Canvas.Panel1
            // 
            this.Canvas.Panel1.Controls.Add(this.Position);
            this.Canvas.Panel1.Controls.Add(this.Shell);
            this.Canvas.Panel1.Controls.Add(this.Run);
            this.Canvas.Panel1.Controls.Add(this.Save);
            this.Canvas.Panel1.Controls.Add(this.Open);
            this.Canvas.Panel1.Controls.Add(this.New);
            this.Canvas.Panel1.Controls.Add(this.InputArea);
            // 
            // Canvas.Panel2
            // 
            this.Canvas.Panel2.Controls.Add(this.OutputArea);
            this.Canvas.Size = new System.Drawing.Size(639, 404);
            this.Canvas.SplitterDistance = 265;
            this.Canvas.TabIndex = 1;
            // 
            // Position
            // 
            this.Position.AutoSize = true;
            this.Position.Location = new System.Drawing.Point(250, 12);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(44, 13);
            this.Position.TabIndex = 7;
            this.Position.Text = "Position";
            // 
            // Shell
            // 
            this.Shell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Shell.Location = new System.Drawing.Point(100, 7);
            this.Shell.Name = "Shell";
            this.Shell.Size = new System.Drawing.Size(75, 23);
            this.Shell.TabIndex = 6;
            this.Shell.Text = "Shell";
            this.Shell.UseVisualStyleBackColor = true;
            this.Shell.Click += new System.EventHandler(this.Shell_Click);
            // 
            // Run
            // 
            this.Run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Run.Location = new System.Drawing.Point(10, 7);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 5;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save.Location = new System.Drawing.Point(554, 7);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 4;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Open
            // 
            this.Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Open.Location = new System.Drawing.Point(461, 7);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(75, 23);
            this.Open.TabIndex = 3;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // New
            // 
            this.New.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.New.Location = new System.Drawing.Point(368, 7);
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(75, 23);
            this.New.TabIndex = 2;
            this.New.Text = "New";
            this.New.UseVisualStyleBackColor = true;
            this.New.Click += new System.EventHandler(this.New_Click);
            // 
            // InputArea
            // 
            this.InputArea.AcceptsTab = true;
            this.InputArea.AllowDrop = true;
            this.InputArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.InputArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputArea.DetectUrls = false;
            this.InputArea.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputArea.HideSelection = false;
            this.InputArea.Location = new System.Drawing.Point(10, 36);
            this.InputArea.Name = "InputArea";
            this.InputArea.ShowSelectionMargin = true;
            this.InputArea.Size = new System.Drawing.Size(619, 226);
            this.InputArea.TabIndex = 0;
            this.InputArea.Text = "";
            this.InputArea.DragDrop += new System.Windows.Forms.DragEventHandler(this.InputArea_DragDrop);
            this.InputArea.DragEnter += new System.Windows.Forms.DragEventHandler(this.InputArea_DragEnter);
            this.InputArea.TextChanged += new System.EventHandler(this.InputArea_TextChanged);
            this.InputArea.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InputArea_KeyUp);
            this.InputArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InputArea_MouseUp);
            // 
            // OutputArea
            // 
            this.OutputArea.AcceptsTab = true;
            this.OutputArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputArea.DetectUrls = false;
            this.OutputArea.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputArea.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.OutputArea.Location = new System.Drawing.Point(10, 3);
            this.OutputArea.Name = "OutputArea";
            this.OutputArea.Size = new System.Drawing.Size(619, 123);
            this.OutputArea.TabIndex = 0;
            this.OutputArea.Text = "";
            // 
            // Rubyq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 404);
            this.Controls.Add(this.Canvas);
            this.KeyPreview = true;
            this.Name = "Rubyq";
            this.Text = "Rubyq";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Rubyq_FormClosing);
            this.Load += new System.EventHandler(this.Rubyq_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Rubyq_KeyDown);
            this.Canvas.Panel1.ResumeLayout(false);
            this.Canvas.Panel1.PerformLayout();
            this.Canvas.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.Canvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer Canvas;
        private System.Windows.Forms.RichTextBox InputArea;
        private System.Windows.Forms.RichTextBox OutputArea;
        private System.Windows.Forms.Button New;
        private System.Windows.Forms.Button Open;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Shell;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.SaveFileDialog SaveFile;
        private System.Windows.Forms.Label Position;



    }
}

