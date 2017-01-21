using System;
using System.Windows.Forms;
using FEITS.Controller;

namespace FEITS.View
{
    public partial class ScriptExport : Form, IExportImportView
    {
        private ImportExportController cont;

        public ScriptExport()
        {
            InitializeComponent();
        }

        public void SetController(ImportExportController controller)
        {
            cont = controller;
        }

        public string MessageText
        {
            get { return TB_ScriptInput.Text; }
            set { TB_ScriptInput.Text = value; }
        }

        public string StatusText
        {
            get { return LBL_CharInfo.Text; }
            set { LBL_CharInfo.Text = value; }
        }

        public bool AllowImport { get; set; }

        public bool ContainsGenderCode { get; set; }

        private void TB_ScriptInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                TB_ScriptInput.SelectAll();
        }

        private void TB_ScriptInput_TextChanged(object sender, EventArgs e)
        {
            StatusText = "Character count: " + TB_ScriptInput.TextLength;
        }
    }
}
