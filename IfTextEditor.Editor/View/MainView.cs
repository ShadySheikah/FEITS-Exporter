using IfTextEditor.Editor.Controller;
using IfTextEditor.Editor.Controller.Interface;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using IfTextEditor.Editor.View.Lexers;
using ScintillaNET;

namespace IfTextEditor.Editor.View
{
    public partial class MainView : Form, IMainView
    {
        //Fields
        private MainController cont;
        private readonly YamlLexer yLex = new YamlLexer();

        private ModelType lastNavigated = ModelType.Target;
        private bool sourceTextDirty,
                     sourceRawDirty,
                     targetTextDirty,
                     targetRawDirty;

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
            set
            {
                int curRow = value;
                if (curRow >= DG_SourceMessageList.Rows.Count)
                    curRow = DG_SourceMessageList.Rows.Count - 1;
                DG_SourceMessageList.CurrentCell = DG_SourceMessageList[0, curRow];
            }
        }

        public int SourcePageIndex
        {
            get { return Convert.ToInt32(TB_SourcePage.Text) - 1; }
            set { TB_SourcePage.Text = (value + 1).ToString(); }
        }

        public int SourcePageCount
        {
            get { return Convert.ToInt32(SL_Source.Text.Substring(SL_Source.Text.IndexOf('/') + 1)); }
            set { SL_Source.Text = $"Source: {SourcePageIndex + 1}/{value}"; }
        }

        public string SourceText
        {
            get { return TB_SourceText.Text; }
            set { TB_SourceText.Text = value; }
        }

        public string SourceParsed
        {
            get { return TB_SourceRaw.Text; }
            set { TB_SourceRaw.Text = value; }
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
            set
            {
                int curRow = value;
                if (curRow >= DG_TargetMessageList.Rows.Count)
                    curRow = DG_TargetMessageList.Rows.Count - 1;
                DG_TargetMessageList.CurrentCell = DG_TargetMessageList[0, curRow];
            }
        }

        public int TargetPageIndex
        {
            get { return Convert.ToInt32(TB_TargetPage.Text) - 1; }
            set { TB_TargetPage.Text = (value + 1).ToString(); }
        }

        public int TargetPageCount
        {
            get { return Convert.ToInt32(SL_Target.Text.Substring(SL_Target.Text.IndexOf('/') + 1)); }
            set { SL_Target.Text = $"Target: {TargetPageIndex + 1}/{value}"; }
        }

        public string TargetText
        {
            get { return TB_TargetText.Text; }
            set { TB_TargetText.Text = value; }
        }

        public string TargetParsed
        {
            get { return TB_TargetRaw.Text; }
            set { TB_TargetRaw.Text = value; }
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
            get { return B_Sync.Checked; }
            set { B_Sync.Checked = value; }
        }

        public bool BackupFiles
        {
            get { return MI_ItemRecovery.Checked; }
            set { MI_ItemRecovery.Checked = value; }
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

        public void ResetTextboxUndo(ModelType type)
        {
            if (type == ModelType.Source)
                TB_SourceText.EmptyUndoBuffer();
            else
                TB_TargetText.EmptyUndoBuffer();
        }

        #region Yaml

        private void SetEditorBoxStyle(Scintilla tb)
        {
            tb.StyleResetDefault();
            tb.Styles[Style.Default].Font = "Verdana";
            tb.Styles[Style.Default].Size = 11;
            tb.StyleClearAll();
        }

        private void SetYamlStyle(Scintilla tb)
        {
            tb.StyleResetDefault();
            tb.Styles[Style.Default].Font = "Consolas";
            tb.Styles[Style.Default].Size = 10;
            tb.StyleClearAll();

            tb.Styles[YamlLexer.StyleDefault].ForeColor = Color.Black;
            tb.Styles[YamlLexer.StyleKey].ForeColor = Color.Brown;
            tb.Styles[YamlLexer.StyleValue].ForeColor = Color.DarkSlateBlue;
            tb.Styles[YamlLexer.StyleString].ForeColor = Color.DarkBlue;
            tb.Styles[YamlLexer.StyleLiteral].ForeColor = Color.Black;
            tb.Styles[YamlLexer.StyleComment].ForeColor = Color.LightGray;

            tb.Lexer = Lexer.Container;
        }

        private void TB_SourceRaw_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            var box = (Scintilla)sender;
            int startPos = box.GetEndStyled();
            int endPos = e.Position;

            yLex.Style(box, startPos, endPos);
        }

        private void TB_TargetRaw_StyleNeeded(object sender, StyleNeededEventArgs e)
        {
            var box = (Scintilla)sender;
            int startPos = box.GetEndStyled();
            int endPos = e.Position;

            yLex.Style(box, startPos, endPos);
        }
        #endregion

        #region Events

        #region Form

        private void MainView_Load(object sender, EventArgs e)
        {
            //Raws
            SetYamlStyle(TB_SourceRaw);
            SetYamlStyle(TB_TargetRaw);

            //Line Editors
            SetEditorBoxStyle(TB_SourceText);
            SetEditorBoxStyle(TB_TargetText);
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((sourceTextDirty || targetTextDirty || sourceRawDirty || targetRawDirty)
                && MessageBox.Show(Properties.Resources.ExitWarning, Properties.Resources.ExitPromptTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                e.Cancel = true;
        }

        private void MainView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Opening

        private void MI_SourceOpen_Click(object sender, EventArgs e)
        {
            AppStatus = cont.OpenFile(ModelType.Source) ? "Source file opened successfully." : "Source file could not be opened.";
            sourceTextDirty = false;
        }

        private void MI_TargetOpen_Click(object sender, EventArgs e)
        {
            AppStatus = cont.OpenFile(ModelType.Target) ? "Target file opened successfully." : "Target file could not be opened";
            targetTextDirty = false;
        }

        private void MI_SourceImport_Click(object sender, EventArgs e)
        {
            cont.OpenFromString(ModelType.Source);
            sourceTextDirty = false;
        }

        private void MI_TargetImport_Click(object sender, EventArgs e)
        {
            cont.OpenFromString(ModelType.Target);
            targetTextDirty = false;
        }
        #endregion

        #region Saving

        private void MI_SourceSave_Click(object sender, EventArgs e)
        {
            if (cont.SaveFile(ModelType.Source))
                sourceTextDirty = false;
        }

        private void MI_TargetSave_Click(object sender, EventArgs e)
        {
            if (cont.SaveFile(ModelType.Target))
                targetTextDirty = false;
        }

        private void MI_SourceSaveAs_Click(object sender, EventArgs e)
        {
            if (cont.SaveFileAs(ModelType.Source))
                sourceTextDirty = false;
        }

        private void MI_TargetSaveAs_Click(object sender, EventArgs e)
        {
            if (cont.SaveFileAs(ModelType.Target))
                targetTextDirty = false;
        }

        private void MI_SourceExportText_Click(object sender, EventArgs e)
        {
            cont.ExportCompiledText(ModelType.Source);
        }

        private void MI_TargetExportText_Click(object sender, EventArgs e)
        {
            cont.ExportCompiledText(ModelType.Target);
        }

        private void MI_SourceExportImage_Click(object sender, EventArgs e)
        {
            cont.SavePreviewImage(ModelType.Source);
        }

        private void MI_TargetExportImage_Click(object sender, EventArgs e)
        {
            cont.SavePreviewImage(ModelType.Target);
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
            bool sDirty = sourceTextDirty;
            cont.SetMessage(SourceMsgIndex, ModelType.Source);
            sourceTextDirty = sDirty;

            if (!B_Sync.Checked || DG_TargetMessageList.Rows.Count <= 0 || SourceMsgIndex >= DG_TargetMessageList.Rows.Count)
                return;

            bool tDirty = targetTextDirty;
            cont.SetMessage(SourceMsgIndex, ModelType.Target);
            targetTextDirty = tDirty;
        }

        private void DG_TargetMessageList_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool tDirty = targetTextDirty;
            cont.SetMessage(TargetMsgIndex, ModelType.Target);
            targetTextDirty = tDirty;

            if (!B_Sync.Checked || DG_SourceMessageList.Rows.Count <= 0 || TargetMsgIndex >= DG_SourceMessageList.Rows.Count)
                return;

            bool sDirty = sourceTextDirty;
            cont.SetMessage(TargetMsgIndex, ModelType.Source);
            sourceTextDirty = sDirty;
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

        private void TB_SourceText_TextChanged(object sender, EventArgs e)
        {
            cont.OnTextChanged(ModelType.Source);
            sourceTextDirty = true;
        }

        private void TB_TargetText_TextChanged(object sender, EventArgs e)
        {
            cont.OnTextChanged(ModelType.Target);
            targetTextDirty = true;
        }

        private void TB_PlayerName_TextChanged(object sender, EventArgs e)
        {
            cont.OnNameChanged();
        }
        #endregion

        #region Direct Edit

        private void TB_SourceRaw_TextChanged(object sender, EventArgs e)
        {
            if (!sourceRawDirty)
                sourceRawDirty = true;
        }

        private void TB_TargetRaw_TextChanged(object sender, EventArgs e)
        {
            if (!targetRawDirty)
                targetRawDirty = true;
        }

        private void TC_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;
            if (tc.SelectedIndex == 0 && sourceRawDirty)
            {
                sourceRawDirty = false;

                int curMsg = SourceMsgIndex;
                int curPage = SourcePageIndex;

                if (!cont.SetFileRaw(ModelType.Source))
                    MessageBox.Show("An error occured while parsing text. Changes made may have been lost.");

                if (curMsg > DG_SourceMessageList.RowCount - 1)
                    curMsg = DG_SourceMessageList.RowCount - 1;
                if (curPage > SourcePageCount - 1)
                    curPage = SourcePageCount - 1;

                cont.SetMessage(curMsg, ModelType.Source);
                cont.GoToPage(curPage, ModelType.Source);
                return;
            }

            SourceParsed = cont.GetFileRaw(ModelType.Source);
            TB_SourceRaw.EmptyUndoBuffer();
            sourceRawDirty = false;
        }

        private void TC_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tc = (TabControl)sender;
            if (tc.SelectedIndex == 0 && targetRawDirty)
            {
                targetRawDirty = false;

                int curMsg = TargetMsgIndex;
                int curPage = TargetPageIndex;

                if (!cont.SetFileRaw(ModelType.Target))
                    MessageBox.Show("An error occured while parsing text. Changes made may have been lost.");

                if (curMsg > DG_TargetMessageList.RowCount - 1)
                    curMsg = DG_TargetMessageList.RowCount - 1;
                if (curPage > TargetPageCount - 1)
                    curPage = TargetPageCount - 1;

                cont.SetMessage(curMsg, ModelType.Target);
                cont.GoToPage(curPage, ModelType.Target);
                return;
            }

            TargetParsed = cont.GetFileRaw(ModelType.Target);
            TB_TargetRaw.EmptyUndoBuffer();
            targetRawDirty = false;
        }


        #endregion

        #region Navigation

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.Return)
            {
                if (TB_SourceText.Focused || TB_TargetText.Focused)
                {
                    cont.PrevPage(TB_SourceText.Focused ? ModelType.Source : ModelType.Target);
                    if (B_Sync.Checked)
                        cont.PrevPage(TB_SourceText.Focused ? ModelType.Target : ModelType.Source);
                }
                else
                {
                    cont.PrevPage(lastNavigated);
                    if (B_Sync.Checked)
                        cont.PrevPage(lastNavigated != ModelType.Source ? ModelType.Source : ModelType.Target);
                }

                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == Keys.Return)
            {
                if (TB_SourceText.Focused || TB_TargetText.Focused)
                {
                    cont.NextPage(TB_SourceText.Focused ? ModelType.Source : ModelType.Target);
                    if (B_Sync.Checked)
                        cont.NextPage(TB_SourceText.Focused ? ModelType.Target : ModelType.Source);
                }
                else
                {
                    cont.NextPage(lastNavigated);
                    if (B_Sync.Checked)
                        cont.NextPage(lastNavigated != ModelType.Source ? ModelType.Source : ModelType.Target);
                }

                e.SuppressKeyPress = true;
            }
        }

        private void B_SourceNext_Click(object sender, EventArgs e)
        {
            lastNavigated = ModelType.Source;
            cont.NextPage(ModelType.Source);

            if (B_Sync.Checked)
                cont.NextPage(ModelType.Target);
        }

        private void B_TargetNext_Click(object sender, EventArgs e)
        {
            lastNavigated = ModelType.Target;
            cont.NextPage(ModelType.Target);

            if (B_Sync.Checked)
                cont.NextPage(ModelType.Source);
        }

        private void B_SourcePrev_Click(object sender, EventArgs e)
        {
            lastNavigated = ModelType.Source;
            cont.PrevPage(ModelType.Source);

            if (B_Sync.Checked)
                cont.PrevPage(ModelType.Target);
        }

        private void B_TargetPrev_Click(object sender, EventArgs e)
        {
            lastNavigated = ModelType.Target;
            cont.PrevPage(ModelType.Target);

            if (B_Sync.Checked)
                cont.PrevPage(ModelType.Source);
        }

        private void TB_SourcePage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lastNavigated = ModelType.Source;
                int newPage = int.Parse(TB_SourcePage.Text) - 1;
                cont.GoToPage(newPage, ModelType.Source);

                if (B_Sync.Checked)
                    cont.GoToPage(newPage, ModelType.Target);
            }
        }

        private void TB_TargetPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lastNavigated = ModelType.Target;
                int newPage = int.Parse(TB_TargetPage.Text) - 1;
                cont.GoToPage(newPage, ModelType.Target);

                if(B_Sync.Checked)
                    cont.GoToPage(newPage, ModelType.Source);
            }
        }

        private void TB_SourcePage_Leave(object sender, EventArgs e)
        {
            lastNavigated = ModelType.Source;
            int newPage = int.Parse(TB_SourcePage.Text) - 1;
            if (newPage == SourcePageIndex)
                return;

            cont.GoToPage(newPage, ModelType.Source);

            if (B_Sync.Checked)
                cont.GoToPage(newPage, ModelType.Target);
        }

        private void TB_TargetPage_Leave(object sender, EventArgs e)
        {
            lastNavigated = ModelType.Target;
            int newPage = int.Parse(TB_TargetPage.Text) - 1;
            if (newPage == TargetPageIndex)
                return;

            cont.GoToPage(newPage, ModelType.Target);

            if (B_Sync.Checked)
                cont.GoToPage(newPage, ModelType.Source);
        }
        #endregion

        #region View

        private void MI_EnableBackgrounds_CheckedChanged(object sender, EventArgs e)
        {
            cont.OnBackgroundEnabledChanged();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aboutBox = new AboutBox())
            {
                aboutBox.ShowDialog();
            }
        }

        private void batchFileProcessingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var proc = new BatchProcessor())
                proc.ShowDialog();
        }

        private void MI_ItemRecovery_CheckedChanged(object sender, EventArgs e)
        {
            if (!MI_ItemRecovery.Checked)
                return;

            MessageBox.Show("Backups can be found in the \"ITE Backups\" folder in your temp directory.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cont.AutosaveBackup();
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

        #endregion

    }
}
