using FEITS.Controller;
using FEITS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace FEITS.View
{
    public partial class CompactMainForm : Form, IMainView
    {
        private MainController cont;

        public CompactMainForm()
        {
            InitializeComponent();
            TB_CurrentLine.InitializeChild();
            //SetCustomDictionary();

            PB_PreviewBox.AllowDrop = true;
        }

        public void SetController(MainController controller)
        {
            cont = controller;
            cont.SetOptions();
        }

        public void SetMessageList(List<MessageBlock> messages)
        {
            var bs = new BindingSource {DataSource = messages};

            LB_MessageList.ValueMember = "Prefix";
            LB_MessageList.DataSource = bs;
        }

        private void SetCustomDictionary()
        {
            var dictionary = System.Windows.Controls.SpellCheck.GetCustomDictionaries((System.Windows.Controls.TextBox) TB_CurrentLine.Child);
            dictionary.Add(new Uri(@"pack://application:,,,/FEITS Exporter;component/Resources/txt/FE_Dictionary.lex"));
        }

        public IList GetCustomDictionary()
        {
            return System.Windows.Controls.SpellCheck.GetCustomDictionaries((System.Windows.Controls.TextBox) TB_CurrentLine.Child);
        }

        #region Properties
        //Message controls
        public int MsgListIndex
        {
            get { return LB_MessageList.SelectedIndex; }
            set { LB_MessageList.SelectedIndex = value; }
        }

        public int CurrentPage
        {
            get { return int.Parse(TB_CurrentPage.Text); }
            set { TB_CurrentPage.Text = (value + 1).ToString(); }
        }

        public string CurrentLine
        {
            get { return TB_CurrentLine.Text; }
            set { TB_CurrentLine.Text = value; TB_CurrentLine.ClearUndo(); }
        }

        public bool PrevLine
        {
            get { return B_PrevLine.Enabled; }
            set { B_PrevLine.Enabled = value; }
        }

        public bool NextLine
        {
            get { return B_NextLine.Enabled; }
            set { B_NextLine.Enabled = value; }
        }

        public Image PreviewImage
        {
            get { return PB_PreviewBox.Image; }
            set { PB_PreviewBox.Image = value; }
        }

        //Settings
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

        public PlayerGender PlayerGender
        {
            get
            {
                foreach(ToolStripMenuItem mi in MI_PlayerGender.DropDownItems)
                {
                    if (mi.Checked)
                        return (PlayerGender) MI_PlayerGender.DropDownItems.IndexOf(mi);
                }

                return PlayerGender.None;
            }
            set
            {
                var menuItem = (ToolStripMenuItem)MI_PlayerGender.DropDownItems[(int) value];

                foreach(ToolStripMenuItem mi in MI_PlayerGender.DropDownItems)
                {
                    mi.Checked = mi == menuItem;
                }
            }
        }

        public int CurrentTextbox
        {
            get
            {
                foreach(ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
                {
                    if (mi.Checked)
                        return MI_TBStyles.DropDownItems.IndexOf(mi);
                }

                return -1;
            }
            set
            {
                var menuItem = (ToolStripMenuItem)MI_TBStyles.DropDownItems[value];

                foreach(ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
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
            set { TSL_PageNumber.Text = "Page: " + CurrentPage.ToString() + " / " + value; }
        }

        #endregion

        private void LB_MessageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cont.SetCurrentMessage();
        }

        private void TB_CurrentLine_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TB_CurrentLine.Text.Length <= 0 && LB_MessageList.Items.Count <= 0)
            {
                TB_CurrentLine.Enabled = false;
            }
            else
            {
                TB_CurrentLine.Enabled = true;
                cont.OnMsgLineChanged();
            }
        }

        private void TB_CurrentLine_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
            {
                if (e.Key == System.Windows.Input.Key.Enter && System.Windows.Input.Keyboard.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control))
                {
                    TB_CurrentLine.ReadOnly = true;

                    if (PrevLine)
                        cont.PreviousPage();
                }
                else if(e.Key == System.Windows.Input.Key.Enter)
                {
                    TB_CurrentLine.ReadOnly = true;

                    if (NextLine)
                        cont.NextPage();
                }
            }

            if (e.Key == System.Windows.Input.Key.PageUp)
            {
                if (LB_MessageList.SelectedIndex > 0)
                    LB_MessageList.SelectedIndex--;
            }
            else if (e.Key == System.Windows.Input.Key.PageDown)
            {
                if (LB_MessageList.SelectedIndex < LB_MessageList.Items.Count - 1)
                    LB_MessageList.SelectedIndex++;
            }
        }

        private void TB_CurrentLine_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                if (TB_CurrentLine.ReadOnly)
                    TB_CurrentLine.ReadOnly = false;
            }
        }

        private void TB_CurrentLine_ChildChanged(object sender, ChildChangedEventArgs e)
        {
            var ctr = (TB_CurrentLine.Child as System.Windows.Controls.TextBox);
            if (ctr == null)
                return;
            ctr.TextChanged += TB_CurrentLine_TextChanged;
            ctr.PreviewKeyDown += TB_CurrentLine_PreviewKeyDown;
            ctr.PreviewKeyUp += TB_CurrentLine_PreviewKeyUp;
        }

        private void B_PrevLine_Click(object sender, EventArgs e)
        {
            cont.PreviousPage();
            TB_CurrentLine.Font = new Font(Font.OriginalFontName, 12f, FontStyle.Regular);
        }

        private void B_NextLine_Click(object sender, EventArgs e)
        {
            cont.NextPage();
            TB_CurrentLine.Font = new Font(Font.OriginalFontName, 12f, FontStyle.Regular);
        }

        private void TB_CurrentPage_Leave(object sender, EventArgs e)
        {
            cont.GoToPage(int.Parse(TB_CurrentPage.Text) - 1);
            TB_CurrentLine.Font = new Font(Font.OriginalFontName, 12f, FontStyle.Regular);
        }

        private void TB_PlayerName_TextChanged(object sender, EventArgs e)
        {
            cont.OnNameChanged();
        }

        private void MI_Open_Click(object sender, EventArgs e)
        {
            if(cont.OpenFile())
                ApplicationStatus = "File opened successfully";
            else
                ApplicationStatus = "Error opening file";
        }

        private void MI_Save_Click(object sender, EventArgs e)
        {
            if (CurrentLine != string.Empty)
            {
                if (!cont.SaveFile())
                {
                    if (!cont.SaveFileAs())
                    {
                        MessageBox.Show("File could not be saved.", "Error");
                        ApplicationStatus = "Could not save file";
                        return;
                    }
                }

                ApplicationStatus = "File saved at " + DateTime.Now.ToShortTimeString();
            }
            else
            {
                MessageBox.Show("There is no file to save.", "Alert");
                return;
            }
        }

        private void MI_SaveAs_Click(object sender, EventArgs e)
        {
            if(CurrentLine != string.Empty)
            {
                if (cont.SaveFileAs())
                    ApplicationStatus = "File saved at " + DateTime.Now.ToShortTimeString();
                else
                    ApplicationStatus = "Could not save file";
            }
            else
            {
                MessageBox.Show("There is no file to save.", "Alert");
            }
        }

        private void MI_Import_Click(object sender, EventArgs e)
        {
            if (cont.ImportMessageScript())
            {
                FormName = "";
                //FD_Save.FileName = string.Empty;
                ApplicationStatus = "Import successful";
            }
            else
            {
                ApplicationStatus = "Import failed";
            }
        }

        private void MI_Export_Click(object sender, EventArgs e)
        {
            cont.ExportMessageScript(false);
        }

        private void exportAllMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cont.ExportMessageScript(true);
        }

        private void MI_EditLine_Click(object sender, EventArgs e)
        {
            cont.EditMessageScript(true);
        }

        private void MI_Message_Click(object sender, EventArgs e)
        {
            cont.EditMessageScript(false);
        }

        private void MI_CheckableItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            if (!item.Checked)
                item.Checked = true;
        }

        private void MI_PlayerGender_CheckedChanged(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            if (!item.Checked)
                return;

            foreach(ToolStripMenuItem mi in MI_PlayerGender.DropDownItems)
            {
                if (mi != item)
                    mi.Checked = false;
            }

            cont.OnPlayerGenderChanged();
        }

        private void MI_TBItem_CheckedChanged(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;

            if (!item.Checked)
                return;

            foreach (ToolStripMenuItem mi in MI_TBStyles.DropDownItems)
            {
                if(mi != item)
                {
                    mi.Checked = false;
                }
            }

            cont.OnTextboxChanged();
        }

        private void MI_HalfBox_Click(object sender, EventArgs e)
        {
            cont.OpenHalfBoxEditor();
        }

        private void MI_Reminder_Click(object sender, EventArgs e)
        {
            cont.ShowFriendlyReminder();
        }

        private void M_Exit_Click(object sender, EventArgs e)
        {
            cont.ExitApplication();
        }

        private void TB_CurrentPage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                cont.GoToPage(int.Parse(TB_CurrentPage.Text) - 1);
        }

        private void MI_EnableBackgrounds_CheckedChanged(object sender, EventArgs e)
        {
            cont.OnBackgroundEnabledChanged();
        }

        private void PB_PreviewBox_Click(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;

            if (box.Image == null)
                return;

            if((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                if (MessageBox.Show("Save the current conversation?", "Save Conversation As Image", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                cont.SavePreview(true);
            }
            else if((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                if (MessageBox.Show("Save the current image?", "Save Preview as Image", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                cont.SavePreview(false);
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

        private void LB_MessageList_MouseDown(object sender, MouseEventArgs e)
        {
            LB_MessageList.Focus();
        }

        private void CompactMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.Enter)
            {
                if (PrevLine)
                    cont.PreviousPage();
            }
            else if (e.Shift && e.KeyCode == Keys.Enter)
            {
                if (NextLine)
                    cont.NextPage();
            }
        }

        private void MI_SpellCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (MI_SpellCheck.Checked)
                TB_CurrentLine.SpellCheckEnabled = true;
            else
                TB_CurrentLine.SpellCheckEnabled = false;
        }

        private void CompactMainForm_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                if (LB_MessageList.SelectedIndex > 0)
                    LB_MessageList.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                if (LB_MessageList.SelectedIndex < LB_MessageList.Items.Count - 1)
                    LB_MessageList.SelectedIndex++;
            }
        }

        private void CompactMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit? All unsaved changes will be lost.", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void MI_CompareMode_Click(object sender, EventArgs e)
        {
            cont.SetupCompareMode();
        }
    }
}
