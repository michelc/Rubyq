using System.Windows.Forms;

namespace Rubyq
{
    public class QEditor
    {
        private RichTextBox Ctrl;

        private int OriginalLength;
        private int OriginalHash;

        public QEditor(RichTextBox ctrl)
        {
            Ctrl = ctrl;
            OriginalLength = this.Text.Length;
            OriginalHash = this.Text.GetHashCode();
        }

        public int Col
        {
            get
            {
                var index = Ctrl.SelectionStart;
                var col = index - Ctrl.GetFirstCharIndexOfCurrentLine() + 1;
                return col;
            }
        }

        public bool Enabled
        {
            get
            {
                return Ctrl.Enabled;
            }
            set
            {
                Ctrl.Enabled = value;
            }
        }

        public string FileName { get; set; }

        public void Focus()
        {
            Ctrl.Focus();
        }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Text);
            }
        }

        public bool IsOriginal
        {
            get
            {
                bool original = true;
                if (OriginalLength != this.Text.Length)
                {
                    original = false;
                }
                else if (OriginalHash != this.Text.GetHashCode())
                {
                    original = false;
                }
                return original;
            }
        }

        public int Line
        {
            get
            {
                var index = Ctrl.SelectionStart;
                var line = Ctrl.GetLineFromCharIndex(index) + 1;
                return line;
            }
        }

        public void Load(string text)
        {
            this.Text = text;
            OriginalLength = this.Text.Length;
            OriginalHash = this.Text.GetHashCode();
        }

        public string Text
        {
            get
            {
                return Ctrl.Text;
            }
            set
            {
                Ctrl.Text = value;
            }
        }

    }
}
