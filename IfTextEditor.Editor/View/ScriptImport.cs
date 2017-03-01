using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using FEITS.Controller;

namespace FEITS.View
{
    public partial class ScriptImport : Form, IExportImportView
    {
        private ImportExportController cont;

        public ScriptImport()
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
            get { return LBL_Warning.Text; }
            set { LBL_Warning.Text = value; }
        }

        public bool AllowImport
        {
            get { return B_LoadScript.Enabled; }
            set
            {
                B_LoadScript.Enabled = value;

                if (B_LoadScript.Enabled)
                    B_LoadScript.DialogResult = DialogResult.OK;
                else
                    B_LoadScript.DialogResult = DialogResult.None;
            }
        }

        public bool ContainsGenderCode { get; set; }

        private void TB_ScriptInput_TextChanged(object sender, EventArgs e)
        {
            cont.OnImportMsgChanged();
        }

        private void TB_ScriptInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                TB_ScriptInput.SelectAll();
        }

        private void TB_ScriptInput_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                MessageText = (string)e.Data.GetData(DataFormats.UnicodeText); ;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TB_ScriptInput_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.All;
        }
    }
}
