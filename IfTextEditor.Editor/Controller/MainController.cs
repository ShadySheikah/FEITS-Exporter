using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using IfTextEditor.Editor.Model;
using System.IO;
using System.Reflection;
using IfTextEditor.Update.View;

namespace IfTextEditor.Editor.Controller
{
    public class MainController
    {
        private readonly Interface.IMainView mainView;
        private readonly ConversationModel sourceModel, targetModel;

        internal MainController(Interface.IMainView view, ConversationModel model)
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
                table.Rows.Add(msg.MsgName);
            }

            return table;
        }

        #region Opening

        public bool OpenFile(ModelType type)
        {
            var ofd = new OpenFileDialog
            {
                Filter = Properties.Resources.OpenFileDialogFilter,
                FilterIndex = 1,
                FileName = string.Empty
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return false;


            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.LoadFromFile(ofd.FileName))
                    {
                        if (sourceModel.FileCont.Name == null)
                            sourceModel.FileCont.Name = Path.GetFileNameWithoutExtension(ofd.FileName);

                        mainView.FormName = sourceModel.FileCont.Name;
                        mainView.SetMessageList(MessageListToTable(sourceModel.FileCont.Messages), false);
                    }
                    return true;
                case ModelType.Target:
                    if (targetModel.LoadFromFile(ofd.FileName))
                    {
                        if (targetModel.FileCont.Name == null)
                            targetModel.FileCont.Name = Path.GetFileNameWithoutExtension(ofd.FileName);

                        mainView.FormName = targetModel.FileCont.Name;
                        mainView.SetMessageList(MessageListToTable(targetModel.FileCont.Messages), true);
                    }
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }


            try
            {
                switch (type)
                {
                    case ModelType.Source:
                        if (sourceModel.LoadFromFile(ofd.FileName))
                        {
                            if (sourceModel.FileCont.Name == null)
                                sourceModel.FileCont.Name = Path.GetFileNameWithoutExtension(ofd.FileName);

                            mainView.FormName = sourceModel.FileCont.Name;
                            mainView.SetMessageList(MessageListToTable(sourceModel.FileCont.Messages), false);
                        }
                        return true;
                    case ModelType.Target:
                        if (targetModel.LoadFromFile(ofd.FileName))
                        {
                            if (targetModel.FileCont.Name == null)
                                targetModel.FileCont.Name = Path.GetFileNameWithoutExtension(ofd.FileName);

                            mainView.FormName = targetModel.FileCont.Name;
                            mainView.SetMessageList(MessageListToTable(targetModel.FileCont.Messages), true);
                        }
                        return true;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.OpenErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void OpenFromString(ModelType type)
        {
            string incomingString;
            
            using (var importer = new Import())
            {
                if (importer.ShowDialog() == DialogResult.Cancel)
                    return;

                incomingString = importer.ImportedText;
            }

            if (incomingString == string.Empty)
                return;

            switch (type)
            {
                case ModelType.Source:
                    if (sourceModel.LoadFromString(incomingString))
                    {
                        mainView.FormName = string.Empty;
                        mainView.SetMessageList(MessageListToTable(sourceModel.FileCont.Messages), false);
                    }
                    break;
                case ModelType.Target:
                    if (targetModel.LoadFromString(incomingString))
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
                    if (sourceModel.FileCont.Path == string.Empty)
                        return SaveFileAs(type);

                    return sourceModel.SaveToFile(sourceModel.FileCont.Path);
                case ModelType.Target:
                    if (targetModel.FileCont.Path == string.Empty)
                        return SaveFileAs(type);

                    return targetModel.SaveToFile(targetModel.FileCont.Path);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public bool SaveFileAs(ModelType type)
        {
            var sfd = new SaveFileDialog
            {
                Filter = Properties.Resources.SaveFileDialogFilter,
                FilterIndex = 1
            };

            switch (type)
            {
                case ModelType.Source:
                    sfd.FileName = sourceModel.FileCont.Name;

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return false;

                    switch (Path.GetExtension(sfd.FileName))
                    {
                        case ".fe":
                            if (sourceModel.ExportYaml(sfd.FileName))
                            {
                                MessageBox.Show("Parsed file saved to file. YAML serialization is still in development, so please be aware that there is a very good chance these files will not be supported in the future.", "Export to YAML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            break;
                        default:
                            if (sourceModel.SaveToFile(sfd.FileName))
                            {
                                //TODO: Name
                                mainView.FormName = sourceModel.FileCont.Name = Path.GetFileNameWithoutExtension(sfd.FileName);
                                return true;
                            }
                            break;
                    }

                    return false;
                case ModelType.Target:
                    sfd.FileName = targetModel.FileCont.Name;

                    if (sfd.ShowDialog() != DialogResult.OK)
                        return false;

                    switch (Path.GetExtension(sfd.FileName))
                    {
                        case ".fe":
                            if (targetModel.ExportYaml(sfd.FileName))
                            {
                                MessageBox.Show("Parsed file saved to file. YAML serialization is still in development, so please be aware that there is a very good chance these files will not be supported in the future.", "Export to YAML", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            break;
                        default:
                            if (targetModel.SaveToFile(sfd.FileName))
                            {
                                mainView.FormName = targetModel.FileCont.Name = Path.GetFileNameWithoutExtension(sfd.FileName);
                                return true;
                            }
                            break;
                    }

                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void ExportCompiledText(ModelType type)
        {
            if ((type == ModelType.Source && sourceModel.FileCont.Messages.Count <= 0) ||
                (type == ModelType.Target && targetModel.FileCont.Messages.Count <= 0))
            {
                MessageBox.Show(Properties.Resources.ExportFail, Properties.Resources.ExportFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var exporter = new Import())
            {
                if (type == ModelType.Source)
                    exporter.SetForExport(sourceModel.FileCont, sourceModel.MessageIndex);
                else
                    exporter.SetForExport(targetModel.FileCont, targetModel.MessageIndex);

                exporter.ShowDialog();
            }
        }
        #endregion

        #region Raws

        internal string GetFileRaw(ModelType type)
        {
            return type == ModelType.Source ? sourceModel.ExportYaml() : targetModel.ExportYaml();
        }

        internal bool SetFileRaw(ModelType type)
        {
            if (type == ModelType.Source)
            {
                if (!sourceModel.LoadFromString(mainView.SourceParsed))
                    return false;

                mainView.FormName = sourceModel.FileCont.Name;
                mainView.SetMessageList(MessageListToTable(sourceModel.FileCont.Messages), false);
                return true;
            }

            if (!targetModel.LoadFromString(mainView.TargetParsed))
                return false;

            mainView.FormName = targetModel.FileCont.Name;
            mainView.SetMessageList(MessageListToTable(targetModel.FileCont.Messages), true);
            return true;
        }
        #endregion

        public bool RemoveMessage(int index, ModelType type)
        {
            if (MessageBox.Show(Properties.Resources.RemoveMessage, string.Format(Properties.Resources.RemoveMessageTitle, type == ModelType.Source ? "Source" : "Target"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return false;

            return type == ModelType.Source ? sourceModel.RemoveMessage(index) : targetModel.RemoveMessage(index);
        }

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

        private void SetPage(int index, ModelType type)
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

                    var srcLines = new string[sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenLine.Count
                        + sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].Comments.Keys.Count];
                    for (int i = 0; i < srcLines.Length; i++)
                    {
                        if (sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].Comments.ContainsKey(i))
                            srcLines[i] = sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].Comments[i];
                    }

                    foreach (string str in sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenLine)
                    {
                        for (int i = 0; i < srcLines.Length; i++)
                        {
                            if (srcLines[i] == null)
                            {
                                srcLines[i] = str;
                                break;
                            }
                        }
                    }

                    mainView.SourceText = string.Join(Environment.NewLine, srcLines);
                    break;
                case ModelType.Target:
                    targetModel.UpdatePageCommands(targetModel.PageIndex);

                    var tarLines = new string[targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenLine.Count
                        + targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].Comments.Count];
                    for (int i = 0; i < tarLines.Length; i++)
                    {
                        if (targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].Comments.ContainsKey(i))
                            tarLines[i] = targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].Comments[i];
                    }

                    foreach (string str in targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenLine)
                    {
                        for (int i = 0; i < tarLines.Length; i++)
                        {
                            if (tarLines[i] == null)
                            {
                                tarLines[i] = str;
                                break;
                            }
                        }
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
                    var srcText = new List<string>();
                    for (int i = 0; i < newSrcText.Length; i++)
                    {
                        if (newSrcText[i].StartsWith("//"))
                            srcComments.Add(i, newSrcText[i]);
                        else
                            srcText.Add(newSrcText[i]);
                    }

                    sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].Comments = srcComments;
                    sourceModel.FileCont.Messages[sourceModel.MessageIndex].Pages[sourceModel.PageIndex].SpokenLine = srcText;
                    mainView.SourcePreviewImage = sourceModel.RenderPreview(sourceModel.PageIndex, PreviewFormat.Normal);
                    break;
                case ModelType.Target:
                    if (targetModel.FileCont.Messages.Count < 1)
                        return;

                    string[] newTarText = mainView.TargetText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var tarComments = new Dictionary<int, string>();
                    var tarText = new List<string>();
                    for (int i = 0; i < newTarText.Length; i++)
                    {
                        if (newTarText[i].StartsWith("//"))
                            tarComments.Add(i, newTarText[i]);
                        else
                            tarText.Add(newTarText[i]);
                    }

                    targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].Comments = tarComments;
                    targetModel.FileCont.Messages[targetModel.MessageIndex].Pages[targetModel.PageIndex].SpokenLine = tarText;
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

        public static void UpdateProgram()
        {
            Update.Program.CheckUpdatesAtRuntime();
        }

        public static void UpdateSettings()
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
