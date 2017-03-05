using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IfTextEditor.Editor.Controller;
using IfTextEditor.Editor.Controller.Interface;
using IfTextEditor.Editor.Model;

namespace IfTextEditor.Editor.View
{
    public partial class DebugForm : Form, IDebugView
    {
        private MainController cont;

        public DebugForm()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            cont = controller;
        }

        public void SetMessageList(List<FileContainer.Message> messages)
        {
            var bs = new BindingSource {DataSource = messages};

            CB_Message.ValueMember = "MessageName";
            CB_Message.DataSource = bs;
        }

        #region Properties

        public int MsgIndex
        {
            get { return CB_Message.SelectedIndex; }
            set { CB_Message.SelectedIndex = value; }
        }

        public int PageIndex
        {
            get { return decimal.ToInt32(NUD_Page.Value - 1); }
            set { NUD_Page.Value = value + 1; }
        }

        public int PageCount
        {
            get { return decimal.ToInt32(NUD_Page.Maximum); }
            set { NUD_Page.Maximum = Convert.ToDecimal(value); }
        }

        public string SourceText
        {
            get { return TB_Input.Text; }
            set { TB_Input.Text = value; }
        }

        public string PageText
        {
            get { return TB_ParsedOutput.Text; }
            set { TB_ParsedOutput.Text = value; }
        }
        #endregion

        private void B_Parse_Click(object sender, EventArgs e)
        {
            //cont.OpenFromString(SourceText);
        }

        private void CB_Message_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cont.SetMessage(MsgIndex);
        }

        private void NUD_Page_ValueChanged(object sender, EventArgs e)
        {
            //cont.SetPage(PageIndex);
        }
    }
}
