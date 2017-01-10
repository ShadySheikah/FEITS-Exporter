using FEITS.Model;
using FEITS.View;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FEITS.Properties;

namespace FEITS.Controller
{
    public class MainController
    {
        private IMainView MainView { get; }
        private ConversationModel MainConversation { get; }

        protected OpenFileDialog OpenFileDialog { get; } = new OpenFileDialog();
        protected SaveFileDialog SaveFileDialog { get; } = new SaveFileDialog();

        public bool IsReminderOpen { private get; set; }

        public MainController(IMainView view, ConversationModel conversationModel)
        {
            MainView = view;
            MainConversation = conversationModel;
            MainView.SetController(this);
            //mainView.SetMessageList(conv.File.MessageList);
        }

        #region Menu Bar

        public virtual bool OpenFile()
        {
            OpenFileDialog.Filter = Resources.TextFileOrAllFileFilter;
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.FileName = string.Empty;

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (MainConversation.File.LoadFromFile(OpenFileDialog.FileName))
                    {
                        MainView.FormName =
                            MainConversation.File.FileName = Path.GetFileNameWithoutExtension(OpenFileDialog.FileName);
                        MainView.SetMessageList(MainConversation.File.MessageList);
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
            if (string.IsNullOrEmpty(MainConversation.File.FilePath)) return false;

            MainConversation.File.SaveToFile(MainConversation.File.FilePath);
            return true;
        }

        public virtual bool SaveFileAs()
        {
            SaveFileDialog.Filter = Resources.TextFileFilter;
            SaveFileDialog.FilterIndex = 1;

            if (!string.IsNullOrEmpty(MainConversation.File.FileName))
                SaveFileDialog.FileName = MainConversation.File.FileName;

            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (MainConversation.File.SaveToFile(SaveFileDialog.FileName))
                    {
                        MainView.FormName =
                            MainConversation.File.FileName = Path.GetFileNameWithoutExtension(SaveFileDialog.FileName);
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
            var messageImporter = new ScriptImport();
            try
            {
                var importCont = new ImportExportController(messageImporter, "");
                var dialogResult = messageImporter.ShowDialog();

                if (dialogResult == DialogResult.OK)
                {
                    if (MainConversation.File.LoadFromString(importCont.MessageScript))
                    {
                        MainView.SetMessageList(MainConversation.File.MessageList);
                    }
                    else
                    {
                        MessageBox.Show(
                            "There was a problem parsing the message. Please double-check the text and try again.",
                            "Failed to Import Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            finally
            {
                messageImporter.Dispose();
            }
            return true;
        }

        public void ExportMessageScript(bool allMessages)
        {
            var messageExporter = new ScriptExport();
            try
            {
                var exportCont = new ImportExportController(messageExporter,
                    allMessages
                        ? MainConversation.File.CompileFileText()
                        : MainConversation.File.MessageList[MainConversation.MessageIndex].CompileMessage(false));
                messageExporter.ShowDialog();
            }
            catch
            {
                //TODO(Robin): Better logging
                MessageBox.Show("Nothing to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                messageExporter.Dispose();
            }
        }

        public void EditMessageScript(bool currentLineOnly)
        {
            var messageList = MainConversation.File.MessageList;
            if (messageList.Count < 1)
            {
                MessageBox.Show("There is nothing to edit.", "Cannot Edit", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var rawLine = string.Empty;
            if (currentLineOnly)
            {
                messageList[MainConversation.MessageIndex].MessageLines[MainConversation.LineIndex]
                    .UpdateRawWithNewDialogue();
                rawLine = messageList[MainConversation.MessageIndex].MessageLines[MainConversation.LineIndex].RawLine;
                rawLine = rawLine.Replace(Environment.NewLine, "\\n").Replace("\n", "\\n");
            }

            using (var messageEdit = new DirectEdit())
            {
                var editCont = new ImportExportController(messageEdit,
                    currentLineOnly ? rawLine : messageList[MainConversation.MessageIndex].CompileMessage(false));

                if (messageEdit.ShowDialog() != DialogResult.OK) return;

                var newMessage = string.Empty;
                if (currentLineOnly)
                {
                    messageList[MainConversation.MessageIndex].MessageLines[MainConversation.LineIndex].RawLine =
                        editCont.MessageScript;

                    newMessage = messageList[MainConversation.MessageIndex].MessageLines
                                                                           .Aggregate(newMessage,
                                                                               (current, msg) => current + msg.RawLine);
                }
                else
                {
                    newMessage = editCont.MessageScript;
                }

                messageList[MainConversation.MessageIndex].MessageLines.Clear();
                messageList[MainConversation.MessageIndex].ParseMessage(newMessage);

                if (currentLineOnly)
                    SetCurrentLine();
                else
                    SetCurrentMessage();
            }
        }

        public void OpenHalfBoxEditor()
        {
            using (var halfBox = new HalfBoxTester())
            {
                var hbCont = new HalfBoxController(halfBox);
                halfBox.ShowDialog();
            }
        }

        public void ShowFriendlyReminder()
        {
            if (IsReminderOpen) return;

            IsReminderOpen = true;
            var reminder = new FriendlyReminder();
            reminder.SetController(this);
            reminder.Show();
        }

        public void ExitApplication()
        {
            Application.Exit();
        }

        #endregion

        protected ParsedFileContainer GetConversationFile()
        {
            return MainConversation.File;
        }

        public void SetOptions()
        {
            MainView.ProtagonistName = MainConversation.PlayerName;
            MainView.CurrentTextbox = MainConversation.TextboxIndex;
            MainView.EnableBackgrounds = MainConversation.EnableBackgrounds;
        }

        public void LoadAssets()
        {
            var dictList = ((CompactMainForm) MainView).GetCustomDictionary();
            var loader = new LoadingPopup();
            loader.BeginLoading(dictList);
            loader.ShowDialog();
        }

        public void SetCurrentMessage()
        {
            MainConversation.MessageIndex = MainView.MsgListIndex;
            Debug.WriteLine(MainConversation.MessageIndex);
            SetCurrentLine();

            MainView.PlayerGender = MainConversation.GetPlayerGenderFromMessageList();
        }

        public void NextPage()
        {
            if (MainConversation.File.MessageList.Count <= 0) return;

            MainConversation.LineIndex++;
            SetCurrentLine();
        }

        public void PreviousPage()
        {
            if (MainConversation.File.MessageList.Count <= 0) return;

            MainConversation.LineIndex--;
            MainConversation.GetCommandsUpUntilIndex();
            SetCurrentLine();
        }

        public void GoToPage(int page)
        {
            var messageList = MainConversation.File.MessageList;

            if (messageList.Count <= 0) return;

            if (page >= messageList[MainConversation.MessageIndex].MessageLines.Count)
                page = messageList[MainConversation.MessageIndex].MessageLines.Count - 1;

            if (page < 0)
                page = 0;

            MainConversation.LineIndex = page;
            MainConversation.GetCommandsUpUntilIndex();
            SetCurrentLine();
        }

        private void SetCurrentLine()
        {
            var conv = MainConversation;
            var lineIndex = conv.LineIndex;
            var msgList = conv.File.MessageList;
            var msgIndex = conv.MessageIndex;

            MainView.CurrentPage = lineIndex;
            MainView.PageCount = msgList[msgIndex].MessageLines.Count.ToString();
            MainView.CurrentLine = conv.GetParsedCommands(msgList[msgIndex].MessageLines[lineIndex]);
            MainView.PrevLine = (lineIndex > 0);
            MainView.NextLine = (lineIndex < msgList[msgIndex].MessageLines.Count - 1);
        }

        public void OnMsgLineChanged()
        {
            try
            {
                MainConversation.File.MessageList[MainConversation.MessageIndex].MessageLines[MainConversation.LineIndex
                    ].SpokenText = MainView.CurrentLine;

                //TODO(Robin): Test for null here but count elsewhere, why?
                if (MainConversation.File.MessageList[MainConversation.MessageIndex] != null)
                    MainView.PreviewImage = MainConversation.RenderPreviewBox(MainView.CurrentLine);
            }
            catch
            {
                //TODO(Robin): ???
            }
        }

        public virtual void OnNameChanged()
        {
            MainConversation.PlayerName = MainView.ProtagonistName;

            ResyncMainViewPreview();
        }

        public virtual void OnTextboxChanged()
        {
            MainConversation.TextboxIndex = MainView.CurrentTextbox;

            ResyncMainViewPreview();
        }

        private void ResyncMainViewPreview()
        {
            if (MainConversation.File.MessageList.Count > 0)
                MainView.PreviewImage = MainConversation.RenderPreviewBox(MainView.CurrentLine);
        }

        public void OnPlayerGenderChanged()
        {
            MainConversation.PlayerGender = MainView.PlayerGender;

            ResyncMainViewPreview();
        }

        public virtual void OnBackgroundEnabledChanged()
        {
            MainConversation.EnableBackgrounds = MainView.EnableBackgrounds;

            ResyncMainViewPreview();

            if (MainView.EnableBackgrounds)
            {
                MessageBox.Show(
                    "Backgrounds enabled." + Environment.NewLine +
                    "Drag/drop an image onto the Picture Box to change the background." + Environment.NewLine +
                    "Disable and then re-enable to reset the background image.", "Alert", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        public virtual bool HandleNewBackgroundImage(DragEventArgs e)
        {
            var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];

            if (File.Exists(file))
            {
                try
                {
                    var img = Image.FromFile(file);
                    if (img.Width > 1 && img.Height > 1)
                    {
                        MainConversation.BackgroundImage = img;

                        ResyncMainViewPreview();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Background Could Not Be Changed", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }

            return false;
        }

        public void SavePreview(bool fullConversation)
        {
            SaveFileDialog.Filter = Resources.PngFileFilter;

            if (fullConversation)
            {
                SaveFileDialog.FileName = MainConversation.File.FileName + "_Conversation";

                if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;

                var imageFile = MainConversation.RenderConversation();
                MainConversation.GetCommandsUpUntilIndex();
                SetCurrentLine();

                imageFile.Save(SaveFileDialog.FileName, ImageFormat.Png);
            }
            else
            {
                SaveFileDialog.FileName = MainConversation.File.FileName + "_Page" + MainView.CurrentPage;

                if (SaveFileDialog.ShowDialog() != DialogResult.OK) return;

                var imageFile = MainView.PreviewImage;
                imageFile.Save(SaveFileDialog.FileName, ImageFormat.Png);
            }
        }

        public void SetupCompareMode()
        {
            var compareForm = new TwoFileForm();
            var controller = new TwoFileController(compareForm, MainConversation);
            var form = (CompactMainForm) MainView;
            controller.SetNormalFormRefs(form);
            form.Hide();

            compareForm.Show();
        }
    }
}