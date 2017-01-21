using FEITS.Model;
using FEITS.View;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using FEITS.Properties;

namespace FEITS.Controller
{
    public class TwoFileController : MainController
    {
        private IComparisonView sourceView;
        private ConversationModel sourceConv;

        private CompactMainForm hiddenForm;

        public TwoFileController(IComparisonView view, ConversationModel conversationModel) : base(view, conversationModel)
        {
            sourceView = view;
            sourceConv = new ConversationModel();

            sourceView.SetController(this);
            sourceView.SetMessageList(conversationModel.File.MessageList);

            SetOptions();
        }

        #region Menu Bar

        public override bool OpenFile()
        {
            var value = base.OpenFile();
            if (value)
                SetViewName();
            return value;
        }

        public bool OpenSourceFile()
        {
            OpenFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            OpenFileDialog.FilterIndex = 1;
            OpenFileDialog.FileName = string.Empty;

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (sourceConv.File.LoadFromFile(OpenFileDialog.FileName))
                    {
                        sourceView.SetSourceMessageList(sourceConv.File.MessageList);
                        sourceConv.File.FileName = Path.GetFileNameWithoutExtension(OpenFileDialog.FileName);
                        SetViewName();
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

        public bool SaveSourceFile()
        {
            if (sourceConv.File.FilePath != string.Empty)
            {
                sourceConv.File.SaveToFile(sourceConv.File.FilePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool SaveFileAs()
        {
            var value = base.SaveFileAs();
            if (value)
                SetViewName();
            return value;
        }

        public bool SaveSourceFileAs()
        {
            SaveFileDialog.Filter = Resources.TextFileFilter;
            SaveFileDialog.FilterIndex = 1;

            if (sourceConv.File.FileName != string.Empty)
                SaveFileDialog.FileName = sourceConv.File.FileName;

            if (SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (sourceConv.File.SaveToFile(SaveFileDialog.FileName))
                    {
                        sourceConv.File.FileName = Path.GetFileNameWithoutExtension(SaveFileDialog.FileName);
                        SetViewName();
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

        public bool ImportSourceMessageScript()
        {
            using (var messageImporter = new ScriptImport())
            {
                var importCont = new ImportExportController(messageImporter, "");

                if (messageImporter.ShowDialog() == DialogResult.OK)
                {
                    if (sourceConv.File.LoadFromString(importCont.MessageScript))
                    {
                        sourceView.SetSourceMessageList(sourceConv.File.MessageList);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(
                            "There was a problem parsing the message. Please double-check the text and try again.",
                            "Failed to Import Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return false;
            }
        }

        public void ExportSourceMessageScript(bool allMessages)
        {
            var messageExporter = new ScriptExport();
            try
            {
                var exportCont = new ImportExportController(messageExporter,
                    allMessages
                        ? sourceConv.File.CompileFileText()
                        : sourceConv.File.MessageList[sourceConv.MessageIndex].CompileMessage(false));
                messageExporter.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Nothing to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                messageExporter.Dispose();
            }
        }

        public void EditSourceMessageScript(bool currentLineOnly)
        {
            if (sourceConv.File.MessageList.Count < 1)
            {
                MessageBox.Show("There is nothing to edit.", "Cannot Edit", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var rawLine = string.Empty;
            if (currentLineOnly)
            {
                sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex]
                    .UpdateRawWithNewDialogue();
                rawLine =
                    sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].RawLine;
                rawLine = rawLine.Replace(Environment.NewLine, "\\n").Replace("\n", "\\n");
            }

            using (var messageEdit = new DirectEdit())
            {
                var editCont = new ImportExportController(messageEdit,
                    currentLineOnly
                        ? rawLine
                        : sourceConv.File.MessageList[sourceConv.MessageIndex].CompileMessage(false));

                if (messageEdit.ShowDialog() == DialogResult.OK)
                {
                    var newMessage = string.Empty;
                    if (currentLineOnly)
                    {
                        sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].RawLine
                            = editCont.MessageScript;

                        foreach (var msg in sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines)
                        {
                            newMessage += msg.RawLine;
                        }
                    }
                    else
                    {
                        newMessage = editCont.MessageScript;
                    }

                    sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Clear();
                    sourceConv.File.MessageList[sourceConv.MessageIndex].ParseMessage(newMessage);

                    if (currentLineOnly)
                        SetCurrentSourceLine();
                    else
                        SetCurrentSourceMessage();
                }
            }
        }

        #endregion

        public void SetNormalFormRefs(CompactMainForm form)
        {
            hiddenForm = form;
        }

        private void SetViewName()
        {
            sourceView.FormName = sourceConv.File.FileName + " / " + GetConversationFile().FileName;
        }

        public void SetCurrentSourceMessage()
        {
            sourceConv.MessageIndex = sourceView.SourceMsgListIndex;
            SetCurrentSourceLine();

            sourceView.SourcePlayerGender = sourceConv.GetPlayerGenderFromMessageList();
        }

        public void NextSourcePage()
        {
            if (sourceConv.File.MessageList.Count > 0)
            {
                sourceConv.LineIndex++;
                SetCurrentSourceLine();
            }
        }

        public void PreviousSourcePage()
        {
            if (sourceConv.File.MessageList.Count > 0)
            {
                sourceConv.LineIndex--;
                sourceConv.GetCommandsUpUntilIndex();
                SetCurrentSourceLine();
            }
        }

        public void GoToSourcePage(int page)
        {
            if (sourceConv.File.MessageList.Count > 0)
            {
                if (page >= sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Count)
                    page = sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Count - 1;

                if (page < 0)
                    page = 0;

                sourceConv.LineIndex = page;
                sourceConv.GetCommandsUpUntilIndex();
                SetCurrentSourceLine();
            }
        }

        //TODO(Robin): Parameterize
        public void SetCurrentSourceLine()
        {
            sourceView.SourceCurrentPage = sourceConv.LineIndex;
            sourceView.SourcePageCount =
                sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Count.ToString();
            sourceView.SourceCurrentLine =
                sourceConv.GetParsedCommands(
                    sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex]);
            sourceView.SourcePrevLine = (sourceConv.LineIndex > 0) ? true : false;
            sourceView.SourceNextLine = (sourceConv.LineIndex <
                                         sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Count - 1)
                ? true
                : false;
        }

        //TODO(Robin): Parameterize
        public void OnSourceMsgLineChanged()
        {
            try
            {
                sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].SpokenText =
                    sourceView.SourceCurrentLine;

                if (sourceConv.File.MessageList.Count > 0)
                    sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
            }
            catch
            {
                //TODO(Robin): ???
            }
        }

        public override void OnNameChanged()
        {
            base.OnNameChanged();

            sourceConv.PlayerName = sourceView.ProtagonistName;

            ResyncSourceViewPreview();
        }

        private void ResyncSourceViewPreview()
        {
            if (sourceConv.File.MessageList.Count > 0)
                sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
        }

        public override void OnTextboxChanged()
        {
            base.OnTextboxChanged();

            sourceConv.TextboxIndex = sourceView.CurrentTextbox;

            ResyncSourceViewPreview();
        }

        public void OnSourcePlayerGenderChanged()
        {
            sourceConv.PlayerGender = sourceView.SourcePlayerGender;

            ResyncSourceViewPreview();
        }

        public override void OnBackgroundEnabledChanged()
        {
            sourceConv.EnableBackgrounds = sourceView.EnableBackgrounds;
            ResyncSourceViewPreview();

            base.OnBackgroundEnabledChanged();
        }

        public override bool HandleNewBackgroundImage(DragEventArgs e)
        {
            var value = base.HandleNewBackgroundImage(e);

            if (value)
            {
                try
                {
                    var file = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];
                    sourceConv.BackgroundImage = Image.FromFile(file);

                    if (sourceConv.File.MessageList.Count > 0)
                        sourceView.PreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Background Could Not Be Changed", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            return value;
        }

        public void SaveSourcePreview(bool fullConversation)
        {
            SaveFileDialog.Filter = "PNG Files (*.png)|*.png";

            if (fullConversation)
            {
                SaveFileDialog.FileName = sourceConv.File.FileName + "_Conversation";

                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var imageFile = sourceConv.RenderConversation();
                    sourceConv.GetCommandsUpUntilIndex();
                    SetCurrentSourceLine();

                    imageFile.Save(SaveFileDialog.FileName, ImageFormat.Png);
                }
            }
            else
            {
                SaveFileDialog.FileName = sourceConv.File.FileName + "_Page" + sourceView.SourceCurrentPage.ToString();

                if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var imageFile = sourceView.SourcePreviewImage;
                    imageFile.Save(SaveFileDialog.FileName, ImageFormat.Png);
                }
            }
        }

        public void ReturnToSingleFile()
        {
            hiddenForm.SetMessageList(GetConversationFile().MessageList);
            hiddenForm.Show();

            var form = (TwoFileForm) sourceView;
            form.Dispose();
        }
    }
}