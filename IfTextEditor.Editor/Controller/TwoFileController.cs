using FEITS.Model;
using FEITS.View;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace FEITS.Controller
{
    public class TwoFileController : MainController
    {
        private IComparisonView sourceView;
        private ConversationModel sourceConv;

        private CompactMainForm hiddenForm;

        public TwoFileController(IComparisonView view, ConversationModel model) : base(view, model)
        {
            sourceView = view;
            sourceConv = new ConversationModel();

            sourceView.SetController(this);
            sourceView.SetMessageList(model.File.MessageList);

            SetOptions();
        }

        #region Menu Bar
        public override bool OpenFile()
        {
            bool value = base.OpenFile();
            if (value)
                SetViewName();
            return value;
        }

        public bool OpenSourceFile()
        {
            ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.FileName = string.Empty;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if(sourceConv.File.LoadFromFile(ofd.FileName))
                    {
                        sourceView.SetSourceMessageList(sourceConv.File.MessageList);
                        sourceConv.File.FileName = Path.GetFileNameWithoutExtension(ofd.FileName);
                        SetViewName();
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could Not Read File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return false;
        }

        public bool SaveSourceFile()
        {
            if(sourceConv.File.FilePath != string.Empty)
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
            bool value = base.SaveFileAs();
            if (value)
                SetViewName();
            return value;
        }

        public bool SaveSourceFileAs()
        {
            sfd.Filter = "Text files (*.txt)|*.txt";
            sfd.FilterIndex = 1;

            if (sourceConv.File.FileName != string.Empty)
                sfd.FileName = sourceConv.File.FileName;

            if(sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if(sourceConv.File.SaveToFile(sfd.FileName))
                    {
                        sourceConv.File.FileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        SetViewName();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "File Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return false;
        }

        public bool ImportSourceMessageScript()
        {
            using (ScriptImport messageImporter = new ScriptImport())
            {
                ImportExportController importCont = new ImportExportController(messageImporter, "");

                if (messageImporter.ShowDialog() == DialogResult.OK)
                {
                    if (sourceConv.File.LoadFromString(importCont.MessageScript))
                    {
                        sourceView.SetSourceMessageList(sourceConv.File.MessageList);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("There was a problem parsing the message. Please double-check the text and try again.", "Failed to Import Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return false;
            }
        }

        public void ExportSourceMessageScript(bool allMessages)
        {
            ScriptExport messageExporter = new ScriptExport();
            try
            {
                ImportExportController exportCont = new ImportExportController(messageExporter, allMessages ? sourceConv.File.CompileFileText() : sourceConv.File.MessageList[sourceConv.MessageIndex].CompileMessage(false));
                messageExporter.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Nothing to export.", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (messageExporter != null)
                    messageExporter.Dispose();
            }
        }

        public void EditSourceMessageScript(bool currentLineOnly)
        {
            if (sourceConv.File.MessageList.Count < 1)
            {
                MessageBox.Show("There is nothing to edit.", "Cannot Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string rawLine = string.Empty;
            if(currentLineOnly)
            {
                sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].UpdateRawWithNewDialogue();
                rawLine = sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].RawLine;
                rawLine = rawLine.Replace(Environment.NewLine, "\\n").Replace("\n", "\\n");
            }

            using (DirectEdit messageEdit = new DirectEdit())
            {
                ImportExportController editCont = new ImportExportController(messageEdit, currentLineOnly ? rawLine : sourceConv.File.MessageList[sourceConv.MessageIndex].CompileMessage(false));

                if(messageEdit.ShowDialog() == DialogResult.OK)
                {
                    string newMessage = string.Empty;
                    if (currentLineOnly)
                    {
                        sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].RawLine = editCont.MessageScript;

                        foreach(MessageLine msg in sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines)
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

            sourceView.SourcePlayerGender = sourceConv.File.MessageList[sourceConv.MessageIndex].Prefix.Contains("PCF") ? 1 : 0;
        }

        public void NextSourcePage()
        {
            if(sourceConv.File.MessageList.Count > 0)
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

        public void SetCurrentSourceLine()
        {
            sourceView.SourceCurrentPage = sourceConv.LineIndex;
            sourceView.SourcePageCount = sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Count.ToString();
            sourceView.SourceCurrentLine = sourceConv.GetParsedCommands(sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex]);
            sourceView.SourcePrevLine = (sourceConv.LineIndex > 0) ? true : false;
            sourceView.SourceNextLine = (sourceConv.LineIndex < sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines.Count - 1) ? true : false;
        }

        public void OnSourceMsgLineChanged()
        {
            try
            {
                sourceConv.File.MessageList[sourceConv.MessageIndex].MessageLines[sourceConv.LineIndex].SpokenText = sourceView.SourceCurrentLine;

                if (sourceConv.File.MessageList.Count > 0)
                    sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
            }
            catch
            {

            }
        }

        public override void OnNameChanged()
        {
            base.OnNameChanged();

            sourceConv.PlayerName = sourceView.ProtagonistName;

            if (sourceConv.File.MessageList.Count > 0)
                sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
        }

        public override void OnTextboxChanged()
        {
            base.OnTextboxChanged();

            sourceConv.TextboxIndex = sourceView.CurrentTextbox;

            if (sourceConv.File.MessageList.Count > 0)
                sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
        }

        public void OnSourcePlayerGenderChanged()
        {
            sourceConv.PlayerGender = sourceView.SourcePlayerGender;

            if (sourceConv.File.MessageList.Count > 0)
                sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
        }

        public override void OnBackgroundEnabledChanged()
        {
            sourceConv.EnableBackgrounds = sourceView.EnableBackgrounds;
            if (sourceConv.File.MessageList.Count > 0)
                sourceView.SourcePreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);

            base.OnBackgroundEnabledChanged();
        }

        public override bool HandleNewBackgroundImage(DragEventArgs e)
        {
            bool value = base.HandleNewBackgroundImage(e);

            if(value)
            {
                try
                {
                    string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                    sourceConv.BackgroundImage = Image.FromFile(file);

                    if (sourceConv.File.MessageList.Count > 0)
                        sourceView.PreviewImage = sourceConv.RenderPreviewBox(sourceView.SourceCurrentLine);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Background Could Not Be Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return value;
        }

        public void SaveSourcePreview(bool fullConversation)
        {
            sfd.Filter = "PNG Files (*.png)|*.png";

            if(fullConversation)
            {
                sfd.FileName = sourceConv.File.FileName + "_Conversation";

                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    Image imageFile = sourceConv.RenderConversation();
                    sourceConv.GetCommandsUpUntilIndex();
                    SetCurrentSourceLine();

                    imageFile.Save(sfd.FileName, ImageFormat.Png);
                }
            }
            else
            {
                sfd.FileName = sourceConv.File.FileName + "_Page" + sourceView.SourceCurrentPage.ToString();

                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    Image imageFile = sourceView.SourcePreviewImage;
                    imageFile.Save(sfd.FileName, ImageFormat.Png);
                }
            }
        }

        public void ReturnToSingleFile()
        {
            hiddenForm.SetMessageList(GetConversationFile().MessageList);
            hiddenForm.Show();

            TwoFileForm form = (TwoFileForm)sourceView;
            form.Dispose();
        }
    }
}
