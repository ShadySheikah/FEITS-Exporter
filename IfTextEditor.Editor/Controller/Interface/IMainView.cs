using System.Data;
using System.Drawing;

namespace IfTextEditor.Editor.Controller.Interface
{
    public interface IMainView
    {
        //Form
        string FormName { set; }
        string AppStatus { set; }

        //Source
        int SourceMsgIndex { get; set; }
        int SourcePageIndex { get; set; }
        int SourcePageCount { get; set; }
        string SourceText { get; set; }
        string SourceParsed { get; set; }
        bool SourceNextLine { get; set; }
        bool SourcePrevLine { get; set; }
        Image SourcePreviewImage { get; set; }
        //Gender

        //Target
        int TargetMsgIndex { get; set; }
        int TargetPageIndex { get; set; }
        int TargetPageCount { get; set; }
        string TargetText { get; set; }
        string TargetParsed { get; set; }
        bool TargetNextLine { get; set; }
        bool TargetPrevLine { get; set; }
        Image TargetPreviewImage { get; set; }
        //Gender

        //Settings
        string ProtagonistName { get; set; }
        bool BackgroundEnabled { get; set; }
        bool SyncNavigation { get; set; }
        bool BackupFiles { get; set; }
        int CurrentTextboxTheme { get; set; }

        void SetController(MainController controller);
        void SetMessageList(DataTable messageTable, bool target);
        void ResetTextboxUndo(ModelType type);
    }
}
