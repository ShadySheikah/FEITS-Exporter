using IfTextEditor.Editor.Controller;
using IfTextEditor.Editor.Controller.Interface;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

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

        private bool IsTwoPane
        {
            get { return MI_TwoPanes.Checked; }
            set { MI_TwoPanes.Checked = value; }
        }
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
            set { SL_Source.Text = $"Source: {SourcePageIndex + 1}/{value}"; }
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
            set { SL_Target.Text = $"Target: {TargetPageIndex + 1}/{value}"; }
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
            IsTwoPane = false;
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
            cont.OpenFromString(ModelType.Source);
        }

        private void MI_TargetImport_Click(object sender, EventArgs e)
        {
            cont.OpenFromString(ModelType.Target);
        }
        #endregion

        #region Saving

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

        private void MI_SourceExport_Click(object sender, EventArgs e)
        {
            cont.ExportCompiledText(ModelType.Source);
        }

        private void MI_TargetExport_Click(object sender, EventArgs e)
        {
            cont.ExportCompiledText(ModelType.Target);
        }
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

        private void DG_SourceMessageList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            cont.SetMessage(SourceMsgIndex, ModelType.Source);
        }

        private void DG_TargetMessageList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            cont.SetMessage(TargetMsgIndex, ModelType.Target);
        }

        private void DG_SourceMessageList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || DG_SourceMessageList.CurrentRow == null)
                return;

            if (cont.RemoveMessage(DG_SourceMessageList.CurrentCell.RowIndex, ModelType.Source))
            {
                int curIndex = DG_SourceMessageList.CurrentCell.RowIndex;
                DG_SourceMessageList.Rows.RemoveAt(curIndex);

                if (curIndex <= DG_SourceMessageList.Rows.Count - 1)
                    DG_SourceMessageList.CurrentCell = DG_SourceMessageList[0, curIndex];
                else if (DG_SourceMessageList.Rows.Count > 0)
                    DG_SourceMessageList.CurrentCell = DG_SourceMessageList[0, DG_SourceMessageList.Rows.Count - 1];
                else
                {
                    PB_SourcePreview.Image = null;
                    TB_SourceText.Text = string.Empty;
                }
            }
        }

        private void DG_TargetMessageList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || DG_TargetMessageList.CurrentRow == null)
                return;

            if (cont.RemoveMessage(DG_TargetMessageList.CurrentCell.RowIndex, ModelType.Target))
            {
                int curIndex = DG_TargetMessageList.CurrentCell.RowIndex;
                DG_TargetMessageList.Rows.RemoveAt(curIndex);

                if (curIndex <= DG_TargetMessageList.Rows.Count - 1)
                    DG_TargetMessageList.CurrentCell = DG_TargetMessageList[0, curIndex];
                else if (DG_TargetMessageList.Rows.Count > 0)
                    DG_TargetMessageList.CurrentCell = DG_TargetMessageList[0, DG_TargetMessageList.Rows.Count - 1];
                else
                {
                    PB_TargetPreview.Image = null;
                    TB_TargetText.Text = string.Empty;
                }
            }
        }
        #endregion

        #region Editing

        private void TB_PlayerName_TextChanged(object sender, EventArgs e)
        {
            cont.OnNameChanged();
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

        #region View

        private void MI_EnableBackgrounds_CheckedChanged(object sender, EventArgs e)
        {
            cont.OnBackgroundEnabledChanged();
        }

        private void MI_TwoPanes_CheckedChanged(object sender, EventArgs e)
        {
            var btn = (ToolStripMenuItem)sender;
            if (btn == null)
                throw new Exception("Button not found!");

            if (btn.Checked)
            {
                Size = new Size(1280, 540);
                SC_PreviewMain.Panel1Collapsed = false;
                MI_Source.Visible = true;
                SL_Source.Visible = true;
            }
            else
            {
                Size = new Size(640, 540);
                SC_PreviewMain.Panel1Collapsed = true;
                MI_Source.Visible = false;
                SL_Source.Visible = false;
            }
        }
        #endregion

        #region Update

        private void MI_UpdateCheck_Click(object sender, EventArgs e)
        {
            MainController.UpdateProgram();
        }

        private void MI_UpdateSettings_Click(object sender, EventArgs e)
        {
            MainController.UpdateSettings();
        }
        #endregion

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
