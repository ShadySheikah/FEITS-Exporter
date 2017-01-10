using FEITS.Controller;
using FEITS.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace FEITS.View
{
    public partial class TwoFileForm : Form, IComparisonView
    {
        private TwoFileController cont;
        private bool setClose;

        public TwoFileForm()
        {
            InitializeComponent();
            TB_CurrentLine_Source.InitializeChild();
            TB_CurrentLine_Target.InitializeChild();

            PB_PreviewBox_Source.AllowDrop = true;
            PB_PreviewBox_Target.AllowDrop = true;
        }

        public void SetController(MainController controller)
        {
            cont = (TwoFileController) controller;
        }

        public void SetMessageList(List<MessageBlock> messages)
        {
            if (messages.Count < 1)
                return;

            var bs = new BindingSource {DataSource = messages};

            LB_MessageList_Target.ValueMember = "Prefix";
            LB_MessageList_Target.DataSource = bs;
        }

        public void SetSourceMessageList(List<MessageBlock> messages)
        {
            if (messages.Count < 1)
                return;

            var bs = new BindingSource {DataSource = messages};

            LB_MessageList_Source.ValueMember = "Prefix";
            LB_MessageList_Source.DataSource = bs;
        }

        #region Properties

        //Message controls
        public int MsgListIndex
        {
            get { return LB_MessageList_Target.SelectedIndex; }
            set { LB_MessageList_Target.SelectedIndex = value; }
        }

        public int SourceMsgListIndex
        {
            get { return LB_MessageList_Source.SelectedIndex; }
            set { LB_MessageList_Source.SelectedIndex = value; }
        }

        public int CurrentPage
        {
            get { return int.Parse(TB_CurrentPage_Target.Text); }
            set { TB_CurrentPage_Target.Text = (value + 1).ToString(); }
        }

        public int SourceCurrentPage
        {
            get { return int.Parse(TB_CurrentPage_Source.Text); }
            set { TB_CurrentPage_Source.Text = (value + 1).ToString(); }
        }

        public string CurrentLine
        {
            get { return TB_CurrentLine_Target.Text; }
            set
            {
                TB_CurrentLine_Target.Text = value;
                TB_CurrentLine_Target.ClearUndo();
            }
        }

        public string SourceCurrentLine
        {
            get { return TB_CurrentLine_Source.Text; }
            set
            {
                TB_CurrentLine_Source.Text = value;
                TB_CurrentLine_Source.ClearUndo();
            }
        }

        public bool PrevLine
        {
            get { return B_PrevPage_Target.Enabled; }
            set { B_PrevPage_Target.Enabled = value; }
        }

        public bool SourcePrevLine
        {
            get { return B_PrevPage_Source.Enabled; }
            set { B_PrevPage_Source.Enabled = value; }
        }

        public bool NextLine
        {
            get { return B_NextPage_Target.Enabled; }
            set { B_NextPage_Target.Enabled = value; }
        }

        public bool SourceNextLine
        {
            get { return B_NextPage_Source.Enabled; }
            set { B_NextPage_Source.Enabled = value; }
        }

        public Image PreviewImage
        {
            get { return PB_PreviewBox_Target.Image; }
            set { PB_PreviewBox_Target.Image = value; }
        }

        public Image SourcePreviewImage
        {
            get { return PB_PreviewBox_Source.Image; }
            set { PB_PreviewBox_Source.Image = value; }
        }

        //Settings
        public bool SimultaneousControl
        {
            get { return MI_SimultaneousControl.Checked; }
            set { MI_SimultaneousControl.Checked = value; }
        }

        public string ProtagonistName
        {
            get { return TB_PlayerName.Text; }
            set { TB_PlayerName.Text = value; }
        }

        public bool EnableBackgrounds
        {
            get { return MI_EnableBackgrounds.Checked; }
            set { MI_EnableBackgrounds.Checked = value; }
        }

        //TODO(Robin): Duplicate code
        public PlayerGender PlayerGender
        {
            get { return MI_PlayerGender_Target.GetGender(); }
            set { MI_PlayerGender_Target.SetGender(value); }
        }

        public PlayerGender SourcePlayerGender
        {
            get { return MI_PlayerGender_Source.GetGender(); }
            set { MI_PlayerGender_Source.SetGender(value); }
        }

        public int CurrentTextbox
        {
            get
            {
                foreach (ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
                {
                    if (mi.Checked)
                        return MI_TBStyles.DropDownItems.IndexOf(mi);
                }

                return -1;
            }
            set
            {
                var menuItem = (ToolStripMenuItem) MI_TBStyles.DropDownItems[value];

                foreach (ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
                {
                    if (mi == menuItem)
                        mi.Checked = true;
                    else
                        mi.Checked = false;
                }
            }
        }

        //Status
        public string FormName
        {
            get { return Text; }
            set
            {
                if (value != string.Empty)
                    Text = value + " - FEITS Exporter";
                else
                    Text = "FEITS Exporter";
            }
        }

        public string ApplicationStatus
        {
            set { TSL_Status.Text = value; }
        }

        public string PageCount
        {
            set { TSL_PageNumber_Target.Text = "Target: " + CurrentPage.ToString() + " / " + value; }
        }

        public string SourcePageCount
        {
            set { TSL_PageNumber_Source.Text = "Source: " + SourceCurrentPage.ToString() + "/ " + value; }
        }

        #endregion

        private void LB_MessageList_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            cont.SetCurrentMessage();
        }

        private void LB_MessageList_Source_SelectedIndexChanged(object sender, EventArgs e)
        {
            cont.SetCurrentSourceMessage();
        }

        private void TB_CurrentLine_Target_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TB_CurrentLine_Target.Text.Length <= 0 && LB_MessageList_Target.Items.Count <= 0)
            {
                TB_CurrentLine_Target.Enabled = false;
            }
            else
            {
                TB_CurrentLine_Target.Enabled = true;
                cont.OnMsgLineChanged();
            }
        }

        private void TB_CurrentLine_Source_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TB_CurrentLine_Source.Text.Length <= 0 && LB_MessageList_Source.Items.Count <= 0)
            {
                TB_CurrentLine_Source.Enabled = false;
            }
            else
            {
                TB_CurrentLine_Source.Enabled = true;
                cont.OnSourceMsgLineChanged();
            }
        }

        private void TB_CurrentLine_Target_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
            {
                if (e.Key == System.Windows.Input.Key.Enter &&
                    System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    TB_CurrentLine_Target.ReadOnly = true;

                    if (PrevLine)
                        cont.PreviousPage();
                }
                else if (e.Key == System.Windows.Input.Key.Enter)
                {
                    TB_CurrentLine_Target.ReadOnly = true;

                    if (NextLine)
                        cont.NextPage();
                }
            }

            if (e.Key == System.Windows.Input.Key.PageUp)
            {
                if (LB_MessageList_Target.SelectedIndex > 0)
                    LB_MessageList_Target.SelectedIndex--;
            }
            else if (e.Key == System.Windows.Input.Key.PageDown)
            {
                if (LB_MessageList_Target.SelectedIndex < LB_MessageList_Target.Items.Count - 1)
                    LB_MessageList_Target.SelectedIndex++;
            }

            if (SimultaneousControl)
            {
                if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
                {
                    if (e.Key == System.Windows.Input.Key.Enter &&
                        System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                    {
                        TB_CurrentLine_Source.ReadOnly = true;

                        if (SourcePrevLine)
                            cont.PreviousSourcePage();
                    }
                    else if (e.Key == System.Windows.Input.Key.Enter)
                    {
                        TB_CurrentLine_Source.ReadOnly = true;

                        if (SourceNextLine)
                            cont.NextSourcePage();
                    }
                }

                if (e.Key == System.Windows.Input.Key.PageUp)
                {
                    if (LB_MessageList_Source.SelectedIndex > 0)
                        LB_MessageList_Source.SelectedIndex--;
                }
                else if (e.Key == System.Windows.Input.Key.PageDown)
                {
                    if (LB_MessageList_Source.SelectedIndex < LB_MessageList_Source.Items.Count - 1)
                        LB_MessageList_Source.SelectedIndex++;
                }
            }
        }

        private void TB_CurrentLine_Source_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (SimultaneousControl)
            {
                TB_CurrentLine_Target_PreviewKeyDown(sender, e);
            }
            else
            {
                if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
                {
                    if (e.Key == System.Windows.Input.Key.Enter &&
                        System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                    {
                        TB_CurrentLine_Source.ReadOnly = true;

                        if (SourcePrevLine)
                            cont.PreviousSourcePage();
                    }
                    else if (e.Key == System.Windows.Input.Key.Enter)
                    {
                        TB_CurrentLine_Source.ReadOnly = true;

                        if (SourceNextLine)
                            cont.NextSourcePage();
                    }
                }

                if (e.Key == System.Windows.Input.Key.PageUp)
                {
                    if (LB_MessageList_Source.SelectedIndex > 0)
                        LB_MessageList_Source.SelectedIndex--;
                }
                else if (e.Key == System.Windows.Input.Key.PageDown)
                {
                    if (LB_MessageList_Source.SelectedIndex < LB_MessageList_Source.Items.Count - 1)
                        LB_MessageList_Source.SelectedIndex++;
                }
            }
        }

        private void TB_CurrentLine_Target_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (TB_CurrentLine_Target.ReadOnly)
                    TB_CurrentLine_Target.ReadOnly = false;
            }

            if (SimultaneousControl)
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (TB_CurrentLine_Source.ReadOnly)
                        TB_CurrentLine_Source.ReadOnly = false;
                }
            }
        }

        private void TB_CurrentLine_Source_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (SimultaneousControl)
            {
                TB_CurrentLine_Target_PreviewKeyUp(sender, e);
            }
            else
            {
                if (e.Key == System.Windows.Input.Key.Enter)
                {
                    if (TB_CurrentLine_Source.ReadOnly)
                        TB_CurrentLine_Source.ReadOnly = false;
                }
            }
        }

        private void TB_CurrentLine_Target_ChildChanged(object sender, ChildChangedEventArgs e)
        {
            var ctr = (TB_CurrentLine_Target.Child as System.Windows.Controls.TextBox);
            if (ctr == null)
                return;
            ctr.TextChanged += TB_CurrentLine_Target_TextChanged;
            ctr.PreviewKeyDown += TB_CurrentLine_Target_PreviewKeyDown;
            ctr.PreviewKeyUp += TB_CurrentLine_Target_PreviewKeyUp;
        }

        private void TB_CurrentLine_Source_ChildChanged(object sender, ChildChangedEventArgs e)
        {
            var ctr = (TB_CurrentLine_Source.Child as System.Windows.Controls.TextBox);
            if (ctr == null)
                return;
            ctr.TextChanged += TB_CurrentLine_Source_TextChanged;
            ctr.PreviewKeyDown += TB_CurrentLine_Source_PreviewKeyDown;
            ctr.PreviewKeyUp += TB_CurrentLine_Source_PreviewKeyUp;
        }

        private void B_PrevLine_Target_Click(object sender, EventArgs e)
        {
            cont.PreviousPage();

            if (SimultaneousControl && SourcePrevLine)
                cont.PreviousSourcePage();
        }

        private void B_PrevLine_Source_Click(object sender, EventArgs e)
        {
            cont.PreviousSourcePage();

            if (SimultaneousControl && PrevLine)
                cont.PreviousPage();
        }

        private void B_NextLine_Target_Click(object sender, EventArgs e)
        {
            cont.NextPage();

            if (SimultaneousControl && SourceNextLine)
                cont.NextSourcePage();
        }

        private void B_NextLine_Source_Click(object sender, EventArgs e)
        {
            cont.NextSourcePage();

            if (SimultaneousControl && NextLine)
                cont.NextPage();
        }

        private void TB_CurrentPage_Target_Leave(object sender, EventArgs e)
        {
            cont.GoToPage(int.Parse(TB_CurrentPage_Target.Text) - 1);

            if (SimultaneousControl && TB_CurrentPage_Source.Text != TB_CurrentPage_Target.Text)
            {
                TB_CurrentPage_Source.Text = TB_CurrentPage_Target.Text;
                cont.GoToSourcePage(int.Parse(TB_CurrentPage_Source.Text) - 1);
            }
        }

        private void TB_CurrentPage_Source_Leave(object sender, EventArgs e)
        {
            cont.GoToSourcePage(int.Parse(TB_CurrentPage_Source.Text) - 1);

            if (SimultaneousControl && TB_CurrentPage_Target.Text != TB_CurrentPage_Source.Text)
            {
                TB_CurrentPage_Target.Text = TB_CurrentPage_Source.Text;
                cont.GoToPage(int.Parse(TB_CurrentPage_Target.Text) - 1);
            }
        }

        private void TB_CurrentPage_Target_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cont.GoToPage(int.Parse(TB_CurrentPage_Target.Text) - 1);

                if (SimultaneousControl && TB_CurrentPage_Source.Text != TB_CurrentPage_Target.Text)
                {
                    TB_CurrentPage_Source.Text = TB_CurrentPage_Target.Text;
                    cont.GoToSourcePage(int.Parse(TB_CurrentPage_Source.Text) - 1);
                }
            }
        }

        private void TB_CurrentPage_Source_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cont.GoToSourcePage(int.Parse(TB_CurrentPage_Source.Text) - 1);

                if (SimultaneousControl && TB_CurrentPage_Target.Text != TB_CurrentPage_Source.Text)
                {
                    TB_CurrentPage_Target.Text = TB_CurrentPage_Source.Text;
                    cont.GoToPage(int.Parse(TB_CurrentPage_Target.Text) - 1);
                }
            }
        }

        private void TB_PlayerName_TextChanged(object sender, EventArgs e)
        {
            cont.OnNameChanged();
        }

        private void MI_Open_Target_Click(object sender, EventArgs e)
        {
            if (cont.OpenFile())
                ApplicationStatus = "File opened successfully";
            else
                ApplicationStatus = "Error opening file";
        }

        private void MI_Open_Source_Click(object sender, EventArgs e)
        {
            if (cont.OpenSourceFile())
                ApplicationStatus = "File opened successfully";
            else
                ApplicationStatus = "Error opening file";
        }

        private void MI_Save_Target_Click(object sender, EventArgs e)
        {
            if (CurrentLine != string.Empty)
            {
                if (!cont.SaveFile())
                {
                    if (!cont.SaveFileAs())
                    {
                        MessageBox.Show("Target file could not be saved.", "Error");
                        ApplicationStatus = "Could not save target";
                        return;
                    }
                }

                ApplicationStatus = "Target saved at " + DateTime.Now.ToShortTimeString();
            }
            else
            {
                MessageBox.Show("There is no file to save.", "Alert");
                return;
            }
        }

        private void MI_Save_Source_Click(object sender, EventArgs e)
        {
            if (SourceCurrentLine != string.Empty)
            {
                if (!cont.SaveSourceFile())
                {
                    if (!cont.SaveSourceFileAs())
                    {
                        MessageBox.Show("Source file could not be saved.", "Error");
                        ApplicationStatus = "Could not save source";
                        return;
                    }
                }

                ApplicationStatus = "Source saved at " + DateTime.Now.ToShortTimeString();
            }
            else
            {
                MessageBox.Show("There is no file to save.", "Alert");
                return;
            }
        }

        private void MI_SaveAs_Target_Click(object sender, EventArgs e)
        {
            if (CurrentLine != string.Empty)
            {
                if (cont.SaveFileAs())
                    ApplicationStatus = "Target saved at " + DateTime.Now.ToShortTimeString();
                else
                    ApplicationStatus = "Could not save target";
            }
            else
            {
                MessageBox.Show("There is no file to save.", "Alert");
            }
        }

        private void MI_SaveAs_Source_Click(object sender, EventArgs e)
        {
            if (SourceCurrentLine != string.Empty)
            {
                if (cont.SaveSourceFileAs())
                    ApplicationStatus = "Source saved at " + DateTime.Now.ToShortTimeString();
                else
                    ApplicationStatus = "Could not save source";
            }
            else
            {
                MessageBox.Show("There is no file to save.", "Alert");
            }
        }

        private void MI_Import_Target_Click(object sender, EventArgs e)
        {
            if (cont.ImportMessageScript())
            {
                FormName = "";
                ApplicationStatus = "Target import successful";
            }
            else
            {
                ApplicationStatus = "Target import failed";
            }
        }

        private void MI_Import_Source_Click(object sender, EventArgs e)
        {
            if (cont.ImportSourceMessageScript())
            {
                FormName = "";
                ApplicationStatus = "Source import successful";
            }
            else
            {
                ApplicationStatus = "Source import failed";
            }
        }

        private void MI_Export_Target_Click(object sender, EventArgs e)
        {
            cont.ExportMessageScript(false);
        }

        private void MI_Export_Source_Click(object sender, EventArgs e)
        {
            cont.ExportSourceMessageScript(false);
        }

        private void MI_ExportAllMessages_Target_Click(object sender, EventArgs e)
        {
            cont.ExportMessageScript(true);
        }

        private void MI_ExportAllMessages_Source_Click(object sender, EventArgs e)
        {
            cont.ExportSourceMessageScript(true);
        }

        private void MI_EditLine_Target_Click(object sender, EventArgs e)
        {
            cont.EditMessageScript(true);
        }

        private void MI_EditLine_Source_Click(object sender, EventArgs e)
        {
            cont.EditSourceMessageScript(true);
        }

        private void MI_Message_Target_Click(object sender, EventArgs e)
        {
            cont.EditMessageScript(false);
        }

        private void MI_Message_Source_Click(object sender, EventArgs e)
        {
            cont.EditSourceMessageScript(false);
        }

        private void MI_CheckableItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            if (!item.Checked)
                item.Checked = true;
        }

        private void MI_PlayerGender_Target_CheckedChanged(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;

            if (!item.Checked)
                return;

            foreach (ToolStripMenuItem mi in MI_PlayerGender_Target.DropDownItems)
            {
                if (mi != item)
                    mi.Checked = false;
            }

            cont.OnPlayerGenderChanged();
        }

        private void MI_PlayerGender_Source_CheckedChanged(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;

            if (!item.Checked)
                return;

            foreach (ToolStripMenuItem mi in MI_PlayerGender_Source.DropDownItems)
            {
                if (mi != item)
                    mi.Checked = false;
            }

            cont.OnSourcePlayerGenderChanged();
        }

        private void MI_TBItem_CheckedChanged(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;

            if (!item.Checked)
                return;

            foreach (ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
            {
                if (mi != item)
                {
                    mi.Checked = false;
                }
            }

            cont.OnTextboxChanged();
        }

        private void MI_Reminder_Click(object sender, EventArgs e)
        {
            cont.ShowFriendlyReminder();
        }

        private void MI_ReturnToNormal_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MI_Exit_Click(object sender, EventArgs e)
        {
            cont.ExitApplication();
        }

        private void MI_EnableBackgrounds_CheckedChanged(object sender, EventArgs e)
        {
            cont.OnBackgroundEnabledChanged();
        }

        private void PB_PreviewBox_Click(object sender, EventArgs e)
        {
            var box = (PictureBox) sender;

            if (box.Image == null)
                return;

            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                if (
                    MessageBox.Show("Save the current conversation?", "Save Conversation As Image",
                        MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                if (box.Name == "PB_PreviewBox_Target")
                    cont.SavePreview(true);
                else
                    cont.SaveSourcePreview(true);
            }
            else if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                if (MessageBox.Show("Save the current image?", "Save Preview as Image", MessageBoxButtons.YesNo) !=
                    DialogResult.Yes)
                    return;

                if (box.Name == "PB_PreviewBox_Source")
                    cont.SavePreview(false);
                else
                    cont.SaveSourcePreview(false);
            }
        }

        private void PB_PreviewBox_DragDrop(object sender, DragEventArgs e)
        {
            cont.HandleNewBackgroundImage(e);
        }

        private void PB_PreviewBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void LB_MessageList_Target_MouseDown(object sender, MouseEventArgs e)
        {
            LB_MessageList_Target.Focus();
        }

        private void LB_MessageList_Source_MouseDown(object sender, MouseEventArgs e)
        {
            LB_MessageList_Source.Focus();
        }

        private void MI_SpellCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (MI_SpellCheck.Checked)
            {
                TB_CurrentLine_Target.SpellCheckEnabled = true;
                TB_CurrentLine_Source.SpellCheckEnabled = true;
            }
            else
            {
                TB_CurrentLine_Target.SpellCheckEnabled = false;
                TB_CurrentLine_Source.SpellCheckEnabled = false;
            }
        }

        private void CompactMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                if (SimultaneousControl)
                {
                    if (LB_MessageList_Source.SelectedIndex > 0)
                        LB_MessageList_Source.SelectedIndex--;
                }

                if (LB_MessageList_Target.SelectedIndex > 0)
                    LB_MessageList_Target.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                if (SimultaneousControl)
                {
                    if (LB_MessageList_Source.SelectedIndex < LB_MessageList_Source.Items.Count - 1)
                        LB_MessageList_Source.SelectedIndex++;
                }

                if (LB_MessageList_Target.SelectedIndex < LB_MessageList_Target.Items.Count - 1)
                    LB_MessageList_Target.SelectedIndex++;
            }
        }

        private void CompactMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (setClose)
                return;

            if (LB_MessageList_Source.Items.Count < 1)
            {
                e.Cancel = true;
                setClose = true;
                cont.ReturnToSingleFile();
                return;
            }

            var result =
                MessageBox.Show(
                    "Stop the comparison? All unsaved changes to the source will be lost, but target data will be retained.",
                    "End Comparison", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                e.Cancel = true;
                setClose = true;
                cont.ReturnToSingleFile();
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}