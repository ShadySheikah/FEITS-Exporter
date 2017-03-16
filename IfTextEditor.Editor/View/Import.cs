using System;
using System.Windows.Forms;

namespace IfTextEditor.Update.View
{
    public partial class Import : Form
    {
        internal string ImportedText { get; set; } = string.Empty;

        public Import()
        {
            InitializeComponent();
        }

        private void TB_Input_TextChanged(object sender, EventArgs e)
        {
            ImportedText = TB_Input.Text;
        }
    }
}
