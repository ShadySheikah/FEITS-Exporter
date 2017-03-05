using IfTextEditor.Editor.Controller;
using IfTextEditor.Editor.Controller.Interface;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IfTextEditor.Editor.Model;

namespace IfTextEditor.Editor.View
{
    public partial class MainView : Form, IMainView
    {
        //Fields
        private MainController cont;

        #region Properties

        #region Form

        public string FormName
        {
            set
            {
                if (value != string.Empty)
                    Text = value + " - " + "If Text Editor";
                else
                    Text = "If Text Editor";
            }
        }
        public string AppStatus { set { SL_AppStatus.Text = value; } }
        #endregion

        #region Source

        public int SourceMsgIndex
        {
            get
            {
                if (DG_SourceMessageList.CurrentRow == null)
                    throw new NullReferenceException("DG_SourceMessageList is null.");
                return DG_SourceMessageList.CurrentRow.Index;
            }
            set { DG_SourceMessageList.CurrentCell = DG_SourceMessageList[0, value]; }
        }

        public int SourcePageIndex
        {
            get { return Convert.ToInt32(TB_SourcePage.Text) - 1; }
            set { TB_SourcePage.Text = (value + 1).ToString(); }
        }

        public int SourcePageCount
        {
            set { SL_Source.Text = $"Source: {SourcePageIndex}/{value}"; }
        }

        public string SourceText
        {
            get { return TB_SourceText.Text; }
            set { TB_SourceText.Text = value; }
        }

        public bool SourceNextLine
        {
            get { return B_SourceNext.Enabled; }
            set { B_SourceNext.Enabled = value; }
        }

        public bool SourcePrevLine
        {
            get { return B_SourcePrev.Enabled; }
            set { B_SourcePrev.Enabled = value; }
        }

        public Image SourcePreviewImage
        {
            get { return PB_SourcePreview.Image; }
            set { PB_SourcePreview.Image = value; }
        }
        #endregion

        #region Target

        public int TargetMsgIndex
        {
            get
            {
                if (DG_TargetMessageList.CurrentRow == null)
                    throw new NullReferenceException("DG_TargetMessageList is null.");
                return DG_TargetMessageList.CurrentRow.Index;
            }
            set { DG_TargetMessageList.CurrentCell = DG_TargetMessageList[0, value]; }
        }

        public int TargetPageIndex
        {
            get { return Convert.ToInt32(TB_TargetPage.Text) - 1; }
            set { TB_TargetPage.Text = (value + 1).ToString(); }
        }

        public int TargetPageCount
        {
            set { SL_Target.Text = $"Target: {TargetPageIndex}/{value}"; }
        }

        public string TargetText
        {
            get { return TB_TargetText.Text; }
            set { TB_TargetText.Text = value; }
        }

        public bool TargetNextLine
        {
            get { return B_TargetNext.Enabled; }
            set { B_TargetNext.Enabled = value; }
        }

        public bool TargetPrevLine
        {
            get { return B_TargetPrev.Enabled; }
            set { B_TargetPrev.Enabled = value; }
        }

        public Image TargetPreviewImage
        {
            get { return PB_TargetPreview.Image; }
            set { PB_TargetPreview.Image = value; }
        }
        #endregion

        #region Settings

        public string ProtagonistName
        {
            get { return TB_PlayerName.Text; }
            set { TB_PlayerName.Text = value; }
        }

        public bool BackgroundEnabled
        {
            get { return MI_EnableBackgrounds.Checked; }
            set { MI_EnableBackgrounds.Checked = value; }
        }

        public bool SyncNavigation
        {
            get { return B_SyncNavigation.Checked; }
            set { B_SyncNavigation.Checked = value; }
        }

        public int CurrentTextboxTheme
        {
            get
            {
                foreach (ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
                {
                    if (mi.Checked)
                        return MI_TBStyles.DropDownItems.IndexOf(mi);
                }

                throw new ArgumentOutOfRangeException("MI_TBStyles");
            }
            set
            {
                var menuItem = (ToolStripMenuItem) MI_TBStyles.DropDownItems[value];

                foreach (ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
                {
                    mi.Checked = (mi == menuItem);
                }
            }
        }
        #endregion
        #endregion

        public MainView()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            cont = controller;
        }

        public void SetMessageList(DataTable messageTable, bool target)
        {
            if (target)
                DG_TargetMessageList.DataSource = messageTable;
            else
                DG_SourceMessageList.DataSource = messageTable;
        }

        #region Events

        #region Opening

        private void MI_SourceOpen_Click(object sender, EventArgs e)
        {
            AppStatus = cont.OpenFile(ModelType.Source) ? "Source file opened successfully." : "Source file could not be opened.";
        }

        private void MI_TargetOpen_Click(object sender, EventArgs e)
        {
            AppStatus = cont.OpenFile(ModelType.Target) ? "Target file opened successfully." : "Target file could not be opened";
        }

        private void MI_SourceImport_Click(object sender, EventArgs e)
        {

        }

        private void MI_TargetImport_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Saving
        
        #endregion

        #region Message List

        private void DG_SourceMessageList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SourceMsgIndex = 0;
        }

        private void DG_TargetMessageList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            TargetMsgIndex = 0;
        }

        private void DG_SourceMessageList_SelectionChanged(object sender, EventArgs e)
        {
            cont.SetMessage(SourceMsgIndex, ModelType.Source);
        }

        private void DG_TargetMessageList_SelectionChanged(object sender, EventArgs e)
        {
            cont.SetMessage(TargetMsgIndex, ModelType.Target);
        }
        #endregion

        #region Navigation

        private void TB_SourceText_TextChanged(object sender, EventArgs e)
        {
            cont.OnTextChanged(ModelType.Source);
        }

        private void TB_TargetText_TextChanged(object sender, EventArgs e)
        {
            cont.OnTextChanged(ModelType.Target);
        }

        private void B_SourceNext_Click(object sender, EventArgs e)
        {
            cont.NextPage(ModelType.Source);
        }

        private void B_TargetNext_Click(object sender, EventArgs e)
        {
            cont.NextPage(ModelType.Target);
        }

        private void B_SourcePrev_Click(object sender, EventArgs e)
        {
            cont.PrevPage(ModelType.Source);
        }

        private void B_TargetPrev_Click(object sender, EventArgs e)
        {
            cont.PrevPage(ModelType.Target);
        }

        private void TB_SourcePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cont.GoToPage(int.Parse(TB_SourcePage.Text) - 1, ModelType.Source);
        }

        private void TB_TargetPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cont.GoToPage(int.Parse(TB_TargetPage.Text) - 1, ModelType.Target);
        }

        private void TB_SourcePage_Leave(object sender, EventArgs e)
        {
            int newPage = int.Parse(TB_SourcePage.Text) - 1;

            if (newPage != SourcePageIndex)
                cont.GoToPage(newPage, ModelType.Source);
        }

        private void TB_TargetPage_Leave(object sender, EventArgs e)
        {
            int newPage = int.Parse(TB_TargetPage.Text) - 1;

            if (newPage != TargetPageIndex)
                cont.GoToPage(newPage, ModelType.Target);
        }
        #endregion

        #endregion

        private void MI_SourceSave_Click(object sender, EventArgs e)
        {
            cont.SaveFile(ModelType.Source);
        }

        private void MI_TargetSave_Click(object sender, EventArgs e)
        {
            cont.SaveFile(ModelType.Target);
        }

        private void MI_SourceSaveAs_Click(object sender, EventArgs e)
        {
            cont.SaveFileAs(ModelType.Source);
        }

        private void MI_TargetSaveAs_Click(object sender, EventArgs e)
        {
            cont.SaveFileAs(ModelType.Target);
        }

        private void TB_PlayerName_TextChanged(object sender, EventArgs e)
        {
            cont.OnNameChanged();
        }

        private void MI_EnableBackgrounds_CheckedChanged(object sender, EventArgs e)
        {
            cont.OnBackgroundEnabledChanged();
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MI_UpdateCheck_Click(object sender, EventArgs e)
        {
            cont.UpdateProgram();
        }

        private void MI_UpdateSettings_Click(object sender, EventArgs e)
        {
            cont.UpdateSettings();
        }
    }
}
