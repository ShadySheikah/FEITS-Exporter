using FEITS.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FEITS.Controller;

namespace FEITS.View
{
    public partial class HalfBoxTester : Form, IHalfBoxView
    {
        private HalfBoxController cont;

        public HalfBoxTester()
        {
            InitializeComponent();
        }

        public void SetController(HalfBoxController controller)
        {
            cont = controller;
        }

        public string CurrentLine
        {
            get { return TB_Line.Text; }
            set { TB_Line.Text = value; }
        }

        public Image PreviewImage
        {
            get { return PB_TextBox.Image; }
            set { PB_TextBox.Image = value; }
        }

        public int CurrentTextboxIndex
        {
            get { return CB_TB.SelectedIndex; }
            set { CB_TB.SelectedIndex = value; }
        }

        public bool CanExport
        {
            get { return B_Export.Enabled; }
            set { B_Export.Enabled = value; }
        }

        private void TB_Line_TextChanged(object sender, EventArgs e)
        {
            if(TB_Line.Text == string.Empty)
            {
                CanExport = false;
            }
            else
            {
                CanExport = true;
            }

            PB_TextBox.Image = cont.UpdatePreview();
        }

        private void B_Export_Click(object sender, EventArgs e)
        {
            if (CurrentLine == string.Empty)
                return;

            cont.ExportText();
        }

        private void CB_TB_SelectedIndexChanged(object sender, EventArgs e)
        {
            PB_TextBox.Image = cont.UpdatePreview();
        }

        private void TB_Line_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                TB_Line.SelectAll();
        }
    }
}
