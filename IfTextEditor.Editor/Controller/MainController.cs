using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IfTextEditor.Editor.Model;
using System.IO;
using System.Reflection;

namespace IfTextEditor.Editor.Controller
{
    public class MainController
    {
        private readonly Interface.IMainView mainView;
        private readonly ConversationModel sourceModel, targetModel;

        public MainController(Interface.IMainView view, ConversationModel model)
        {
            mainView = view;
            targetModel = model;
            sourceModel = new ConversationModel();
            mainView.SetController(this);
        }

        private DataTable MessageListToTable(IEnumerable<FileContainer.Message> messages)
        {
            var table = new DataTable();
            table.Columns.Add();

            foreach (FileContainer.Message msg in messages)
            {
                table.Rows.Add(msg.MessageName);
            }

            return table;
        }

        #region Opening

        public bool OpenFile(ModelType type)
        {
            //SKSurface surface = SKSurface.Create(400, 240, SKColorType.Rgba8888, SKAlphaType.Premul);
            //SKCanvas canvas = surface.Canvas;

            var ofd = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|Parsed text (*.fe)|*.fe|All files (*.*)|*.*",
                FilterIndex = 1,
                FileName = string.Empty
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return false;

            try
            {
                switch (type)
                {
                    case ModelType.Source:
                        if (sourceModel.LoadFromFile(ofd.FileName))
                        {
                            if (sourceModel.FileCont.FileName == null)
                                sourceModel.FileCont.FileName = Path.GetFileNameWithoutExtension(ofd.FileName);

                            mainView.FormName = sourceModel.FileCont.FileName;
                            mainView.SetMessageList(MessageListToTable(sourceModel.FileCont.Messages), false);
                        }
                        return true;
                    case ModelType.Target:
                        if (targetModel.LoadFromFile(ofd.FileName))
                        {
                            if (targetModel.FileCont.FileName == null)
                                targetModel.FileCont.FileName = Path.GetFileNameWithoutExtension(ofd.FileName);

                            mainView.FormName = targetModel.FileCont.FileName;
                            mainView.SetMessageList(MessageListToTable(targetModel.FileCont.Messages), true);
                        }
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void OpenFromString(ModelType type)
        {
            //TODO: New Form open stuff here
            string tempString = "";

            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.LoadFromString(tempString))
                    {
                        mainView.FormName = string.Empty;
                        mainView.SetMessageList(MessageListToTable(sourceModel.FileCont.Messages), false);
                    }
                    break;
                case ModelType.Target:
                    if (targetModel.LoadFromString(tempString))
                    {
                        mainView.FormName = string.Empty;
                        mainView.SetMessageList(MessageListToTable(targetModel.FileCont.Messages), true);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        #endregion

        #region Saving

        public bool SaveFile(ModelType type)
        {
            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.FileCont.FilePath == string.Empty)
                        return SaveFileAs(type);

                    return sourceModel.SaveToFile(sourceModel.FileCont.FilePath);
                case ModelType.Target:
                    if (targetModel.FileCont.FilePath == string.Empty)
                        return SaveFileAs(type);

                    return targetModel.SaveToFile(targetModel.FileCont.FilePath);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public bool SaveFileAs(ModelType type)
        {
            var sfd = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|Parsed text (*.fe)|*.fe",
                FilterIndex = 1
            };

            switch (type)
            {
                case ModelType.Source:
                    sfd.FileName = sourceModel.FileCont.FileName;

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return false;

                    if (sourceModel.SaveToFile(sfd.FileName))
                    {
                        //TODO: Name
                        mainView.FormName = sourceModel.FileCont.FileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        return true;
                    }

                    return false;
                case ModelType.Target:
                    sfd.FileName = targetModel.FileCont.FileName;

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return false;

                    if (targetModel.SaveToFile(sfd.FileName))
                    {
                        mainView.FormName = targetModel.FileCont.FileName = Path.GetFileNameWithoutExtension(sfd.FileName);
                        return true;
                    }

                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        #endregion

        public void SetMessage(int index, ModelType type)
        {
            if (index < 0)
                index = 0;

            switch (type)
            {
                case ModelType.Source:
                    mainView.SourceMsgIndex = sourceModel.MessageIndex = index;
                    SetPage(0, type);
                    break;
                case ModelType.Target:
                    mainView.TargetMsgIndex = targetModel.MessageIndex = index;
                    SetPage(0, type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void SetPage(int index, ModelType type)
        {
            if (index < 0)
                index = 0;

            switch (type)
            {
                case ModelType.Source:
                    if (index >= sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages.Count)
                        index = sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages.Count - 1;

                    mainView.SourcePageIndex = sourceModel.PageIndex = index;
                    mainView.SourcePageCount = sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages.Count;

                    mainView.SourcePrevLine = sourceModel.PageIndex > 0;
                    mainView.SourceNextLine = sourceModel.PageIndex < sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages.Count;
                    SetCurrentLine(type);
                    break;
                case ModelType.Target:
                    if (index >= targetModel.FileCont.Messages[targetModel.MessageIndex].Pages.Count)
                        index = targetModel.FileCont.Messages[targetModel.MessageIndex].Pages.Count - 1;

                    mainView.TargetPageIndex = targetModel.PageIndex = index;
                    mainView.TargetPageCount = targetModel.FileCont.Messages[targetModel.MessageIndex].Pages.Count;

                    mainView.TargetPrevLine = targetModel.PageIndex > 0;
                    mainView.TargetNextLine = targetModel.PageIndex < targetModel.FileCont.Messages[targetModel.MessageIndex].Pages.Count - 1;
                    SetCurrentLine(type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void SetCurrentLine(ModelType type)
        {
            switch (type)
            {
                case ModelType.Source:
                    sourceModel.UpdatePageCommands(sourceModel.PageIndex);

                    var srcLines = new string[sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenText.Keys.Count + sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].ExtraComment.Keys.Count];
                    for (int i = 0; i < srcLines.Length; i++)
                    {
                        if (sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenText.ContainsKey(i))
                            srcLines[i] = sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenText[i];
                        else if (sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].ExtraComment.ContainsKey(i))
                            srcLines[i] = sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].ExtraComment[i];
                    }

                    mainView.SourceText = string.Join(Environment.NewLine, srcLines);
                    break;
                case ModelType.Target:
                    targetModel.UpdatePageCommands(targetModel.PageIndex);

                    var tarLines = new string[targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenText.Keys.Count + targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].ExtraComment.Keys.Count];
                    for (int i = 0; i < tarLines.Length; i++)
                    {
                        if (targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenText.ContainsKey(i))
                            tarLines[i] = targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenText[i];
                        else if (targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].ExtraComment.ContainsKey(i))
                            tarLines[i] = targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].ExtraComment[i];
                    }

                    mainView.TargetText = string.Join(Environment.NewLine, tarLines);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void NextPage(ModelType type)
        {
            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.FileCont.Messages.Count > 0)
                        SetPage(sourceModel.PageIndex + 1, type);
                    break;
                case ModelType.Target:
                    if(targetModel.FileCont.Messages.Count > 0)
                        SetPage(targetModel.PageIndex + 1, type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void PrevPage(ModelType type)
        {
            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.FileCont.Messages.Count <= 0)
                        return;

                    sourceModel.IterateCommandsToIndex(sourceModel.PageIndex);
                    SetPage(sourceModel.PageIndex - 1, type);
                    break;
                case ModelType.Target:
                    if (targetModel.FileCont.Messages.Count <= 0)
                        return;

                    targetModel.IterateCommandsToIndex(targetModel.PageIndex - 1);
                    SetPage(targetModel.PageIndex - 1, type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void GoToPage(int page, ModelType type)
        {
            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.FileCont.Messages.Count <= 0)
                        return;

                    if (page >= sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages.Count)
                        page = sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages.Count - 1;
                    else if (page < 0)
                        page = 0;

                    sourceModel.IterateCommandsToIndex(page);
                    SetPage(page, type);
                    break;
                case ModelType.Target:
                    if (targetModel.FileCont.Messages.Count <= 0)
                        return;

                    if (page >= targetModel.FileCont.Messages[targetModel.MessageIndex].Pages.Count)
                        page = targetModel.FileCont.Messages[targetModel.MessageIndex].Pages.Count - 1;
                    else if (page < 0)
                        page = 0;

                    targetModel.IterateCommandsToIndex(page);
                    SetPage(page, type);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void OnTextChanged(ModelType type)
        {
            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.FileCont.Messages.Count < 1)
                        return;

                    string[] newSrcText = mainView.SourceText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var srcComments = new Dictionary<int, string>();
                    var srcText = new Dictionary<int, string>();
                    for (int i = 0; i < newSrcText.Length; i++)
                    {
                        if (newSrcText[i].StartsWith("//"))
                            srcComments.Add(i, newSrcText[i]);
                        else
                            srcText.Add(i, newSrcText[i]);
                    }

                    sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].ExtraComment = srcComments;
                    sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenText = srcText;
                    mainView.SourcePreviewImage = sourceModel.RenderPreview(sourceModel.PageIndex, PreviewFormat.Normal);
                    break;
                case ModelType.Target:
                    if (targetModel.FileCont.Messages.Count < 1)
                        return;

                    string[] newTarText = mainView.TargetText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var tarComments = new Dictionary<int, string>();
                    var tarText = new Dictionary<int, string>();
                    for (int i = 0; i < newTarText.Length; i++)
                    {
                        if (newTarText[i].StartsWith("//"))
                            tarComments.Add(i, newTarText[i]);
                        else
                            tarText.Add(i, newTarText[i]);
                    }

                    targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].ExtraComment = tarComments;
                    targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenText = tarText;
                    mainView.TargetPreviewImage = targetModel.RenderPreview(targetModel.PageIndex, PreviewFormat.Normal);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void OnNameChanged()
        {
            sourceModel.PlayerName = targetModel.PlayerName = mainView.ProtagonistName;
            UpdateCurrentPage();
        }

        public void OnTextboxChanged()
        {
            sourceModel.TextboxIndex = targetModel.TextboxIndex = mainView.CurrentTextboxTheme;
            UpdateCurrentPage();
        }

        public void OnPlayerGenderChanged()
        {
            throw new NotImplementedException();
        }

        public void OnBackgroundEnabledChanged()
        {
            sourceModel.BackgroundEnabled = targetModel.BackgroundEnabled = mainView.BackgroundEnabled;
            UpdateCurrentPage();
        }

        public void OnBackgroundImageChanged(DragEventArgs e)
        {
            string filePath = ((string[]) e.Data.GetData(DataFormats.FileDrop))[0];

            if (!File.Exists(filePath))
                return;

            try
            {
                Image background = Image.FromFile(filePath);

                if (background.Width < 1 || background.Height < 1)
                    return;

                sourceModel.BackgroundImage = targetModel.BackgroundImage = background;
                UpdateCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error updating background", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCurrentPage()
        {
            mainView.SourcePreviewImage = sourceModel.RenderPreview(sourceModel.PageIndex, PreviewFormat.Normal);
            mainView.TargetPreviewImage = targetModel.RenderPreview(targetModel.PageIndex, PreviewFormat.Normal);
        }

        public void UpdateProgram()
        {
            Update.Program.CheckUpdatesAtRuntime();
        }

        public void UpdateSettings()
        {
            Update.Program.SetUpdatePreferences();
        }
    }
}

public enum ModelType
{
    Source,
    Target
}
