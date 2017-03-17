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

        internal void SetForExport(string exportedText)
        {
            Text = "Export";
            AcceptButton = B_Import;

            TB_Input.Text = exportedText;
            TB_Input.ReadOnly = true;
            TB_Input.SelectAll();

            B_Cancel.Visible = false;
            B_Import.Text = "OK";
        }
    }
}
