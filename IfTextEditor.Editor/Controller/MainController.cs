using FEITS.Model;
using FEITS.View;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace FEITS.Controller
{
    public class MainController
    {
        private IMainView mainView;
        private ConversationModel conv;

        protected OpenFileDialog ofd = new OpenFileDialog();
        protected SaveFileDialog sfd = new SaveFileDialog();

        private bool reminderOpen;
        public bool ReminderOpen { set { reminderOpen = value; } }

        public MainController(IMainView view, ConversationModel model)
        {
            mainView = view;
            conv = model;
            mainView.SetController(this);
            //mainView.SetMessageList(conv.File.MessageList);
        }

        #region Menu Bar
        public virtual bool OpenFile()
        {
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.FileName = string.Empty;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (conv.File.LoadFromFile(ofd.FileName))
                    {
                        mainView.FormName = conv.File.FileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                        mainView.SetMessageList(conv.File.MessageList);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could Not Read File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to save to a previous filepath.
        /// Returns true if successful, false if not.
        /// </summary>
        public bool SaveFile()
        {
            if(conv.File.FilePath != string.Empty)
            {
                conv.File.SaveToFile(conv.File.FilePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool SaveFileAs()
        {
            sfd.Filter = "Text files (*.txt)|*.txt";
            sfd.FilterIndex = 1;

            if (conv.File.FileName != string.Empty)
                sfd.FileName = conv.File.FileName;

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (conv.File.SaveToFile(sfd.FileName))
                    {
                        mainView.FormName = conv.File.FileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "File Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return false;
        }

        public bool ImportMessageScript()
        {
            ScriptImport messageImporter = new ScriptImport();
            try
            {
                ImportExportController importCont = new ImportExportController(messageImporter, "");
                DialogResult dialogResult = messageImporter.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    if (conv.File.LoadFromString(importCont.MessageScript))
                    {
                        mainView.SetMessageList(conv.File.MessageList);
                    }
                    else
                    {
                        MessageBox.Show("There was a problem parsing the message. Please double-check the text and try again.", "Failed to Import Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            finally
            {
                if (messageImporter != null)
                    messageImporter.Dispose();
            }
            return true;
        }

        public void ExportMessageScript(bool allMessages)
        {
            ScriptExport messageExporter = new ScriptExport();
            try
            {
                ImportExportController exportCont = new ImportExportController(messageExporter, allMessages ? conv.File.CompileFileText() : conv.File.MessageList[conv.MessageIndex].CompileMessage(false));
                messageExporter.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Nothing to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if(messageExporter != null)
                    messageExporter.Dispose();
            }
        }

        public void EditMessageScript(bool currentLineOnly)
        {
            if(conv.File.MessageList.Count < 1)
            {
                MessageBox.Show("There is nothing to edit.", "Cannot Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string rawLine = string.Empty;
            if (currentLineOnly)
            {
                conv.File.MessageList[conv.MessageIndex].MessageLines[conv.LineIndex].UpdateRawWithNewDialogue();
                rawLine = conv.File.MessageList[conv.MessageIndex].MessageLines[conv.LineIndex].RawLine;
                rawLine = rawLine.Replace(Environment.NewLine, "\\n").Replace("\n", "\\n");
            }

            using (DirectEdit messageEdit = new DirectEdit())
            {
                ImportExportController editCont = new ImportExportController(messageEdit, currentLineOnly ? rawLine : conv.File.MessageList[conv.MessageIndex].CompileMessage(false));

                if (messageEdit.ShowDialog() == DialogResult.OK)
                {
                    string newMessage = string.Empty;
                    if (currentLineOnly)
                    {
                        conv.File.MessageList[conv.MessageIndex].MessageLines[conv.LineIndex].RawLine = editCont.MessageScript;

                        foreach (MessageLine msg in conv.File.MessageList[conv.MessageIndex].MessageLines)
                        {
                            newMessage += msg.RawLine;
                        }
                    }
                    else
                    {
                        newMessage = editCont.MessageScript;
                    }

                    conv.File.MessageList[conv.MessageIndex].MessageLines.Clear();
                    conv.File.MessageList[conv.MessageIndex].ParseMessage(newMessage);

                    if (currentLineOnly)
                        SetCurrentLine();
                    else
                        SetCurrentMessage();
                }
            }
        }

        public void OpenHalfBoxEditor()
        {
            using (HalfBoxTester halfBox = new HalfBoxTester())
            {
                HalfBoxController hbCont = new HalfBoxController(halfBox);
                halfBox.ShowDialog();
            }
        }

        public void ShowFriendlyReminder()
        {
            if(!reminderOpen)
            {
                reminderOpen = true;
                FriendlyReminder reminder = new FriendlyReminder();
                reminder.SetController(this);
                reminder.Show();
            }
        }

        public void ExitApplication()
        {
            Application.Exit();
        }
        #endregion

        protected ParsedFileContainer GetConversationFile()
        {
            return conv.File;
        }

        public void SetOptions()
        {
            mainView.ProtagonistName = conv.PlayerName;
            mainView.CurrentTextbox = conv.TextboxIndex;
            mainView.EnableBackgrounds = conv.EnableBackgrounds;
        }

        public void LoadAssets()
        {
            LoadingPopup loader = new LoadingPopup();
            loader.BeginLoading();
            loader.ShowDialog();
        }

        public void SetCurrentMessage()
        {
            conv.MessageIndex = mainView.MsgListIndex;
            Console.WriteLine(conv.MessageIndex);
            SetCurrentLine();

            mainView.PlayerGender = conv.File.MessageList[conv.MessageIndex].Prefix.Contains("PCF") ? 1 : 0;
        }

        public void NextPage()
        {
            if(conv.File.MessageList.Count > 0)
            {
                conv.LineIndex++;
                SetCurrentLine();
            }
        }

        public void PreviousPage()
        {
            if (conv.File.MessageList.Count > 0)
            {
                conv.LineIndex--;
                conv.GetCommandsUpUntilIndex();
                SetCurrentLine();
            }
        }

        public void GoToPage(int page)
        {
            if (conv.File.MessageList.Count > 0)
            {
                if (page >= conv.File.MessageList[conv.MessageIndex].MessageLines.Count)
                    page = conv.File.MessageList[conv.MessageIndex].MessageLines.Count - 1;

                if (page < 0)
                    page = 0;

                conv.LineIndex = page;
                conv.GetCommandsUpUntilIndex();
                SetCurrentLine();
            }
        }

        private void SetCurrentLine()
        {
            mainView.CurrentPage = conv.LineIndex;
            mainView.PageCount = conv.File.MessageList[conv.MessageIndex].MessageLines.Count.ToString();
            mainView.CurrentLine = conv.GetParsedCommands(conv.File.MessageList[conv.MessageIndex].MessageLines[conv.LineIndex]);
            mainView.PrevLine = (conv.LineIndex > 0) ? true : false;
            mainView.NextLine = (conv.LineIndex < conv.File.MessageList[conv.MessageIndex].MessageLines.Count - 1) ? true : false;
        }

        public void OnMsgLineChanged()
        {
            try
            {
                conv.File.MessageList[conv.MessageIndex].MessageLines[conv.LineIndex].SpokenText = mainView.CurrentLine;

                if (conv.File.MessageList[conv.MessageIndex] != null)
                    mainView.PreviewImage = conv.RenderPreviewBox(mainView.CurrentLine);
            }
            catch
            {
                
            }
        }

        public virtual void OnNameChanged()
        {
            conv.PlayerName = mainView.ProtagonistName;

            if (conv.File.MessageList.Count > 0)
                mainView.PreviewImage = conv.RenderPreviewBox(mainView.CurrentLine);
        }

        public virtual void OnTextboxChanged()
        {
            conv.TextboxIndex = mainView.CurrentTextbox;

            if (conv.File.MessageList.Count > 0)
                mainView.PreviewImage = conv.RenderPreviewBox(mainView.CurrentLine);
        }

        public void OnPlayerGenderChanged()
        {
            conv.PlayerGender = mainView.PlayerGender;

            if (conv.File.MessageList.Count > 0)
                mainView.PreviewImage = conv.RenderPreviewBox(mainView.CurrentLine);
        }

        public virtual void OnBackgroundEnabledChanged()
        {
            conv.EnableBackgrounds = mainView.EnableBackgrounds;

            if (conv.File.MessageList.Count > 0)
                mainView.PreviewImage = conv.RenderPreviewBox(mainView.CurrentLine);

            if (mainView.EnableBackgrounds)
            {
                MessageBox.Show("Backgrounds enabled." + Environment.NewLine + "Drag/drop an image onto the Picture Box to change the background." + Environment.NewLine + "Disable and then re-enable to reset the background image.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public virtual bool HandleNewBackgroundImage(DragEventArgs e)
        {
            string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

            if(File.Exists(file))
            {
                try
                {
                    Image img = Image.FromFile(file);
                    if(img.Width > 1 && img.Height > 1)
                    {
                        conv.BackgroundImage = img;

                        if (conv.File.MessageList.Count > 0)
                            mainView.PreviewImage = conv.RenderPreviewBox(mainView.CurrentLine);

                        return true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Background Could Not Be Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return false;
        }

        public void SavePreview(bool fullConversation)
        {
            sfd.Filter = "PNG Files (*.png)|*.png";
            
            if(fullConversation)
            {
                sfd.FileName = conv.File.FileName + "_Conversation";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Image imageFile = conv.RenderConversation();
                    conv.GetCommandsUpUntilIndex();
                    SetCurrentLine();

                    imageFile.Save(sfd.FileName, ImageFormat.Png);
                }
            }
            else
            {
                sfd.FileName = conv.File.FileName + "_Page" + mainView.CurrentPage.ToString();
                
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    Image imageFile = mainView.PreviewImage;
                    imageFile.Save(sfd.FileName, ImageFormat.Png);
                }
            }
        }

        public void SetupCompareMode()
        {
            TwoFileForm compareForm = new TwoFileForm();
            TwoFileController controller = new TwoFileController(compareForm, conv);
            CompactMainForm form = (CompactMainForm)mainView;
            controller.SetNormalFormRefs(form);
            form.Hide();

            compareForm.Show();
        }
    }
}
