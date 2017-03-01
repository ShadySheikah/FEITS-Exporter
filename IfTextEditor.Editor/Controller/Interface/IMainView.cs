using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IfTextEditor.Editor.Model;

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
        int SourcePageCount { set; }
        string SourceText { get; set; }
        bool SourceNextLine { get; set; }
        bool SourcePrevLine { get; set; }
        Image SourcePreviewImage { get; set; }
        //Gender

        //Target
        int TargetMsgIndex { get; set; }
        int TargetPageIndex { get; set; }
        int TargetPageCount { set; }
        string TargetText { get; set; }
        bool TargetNextLine { get; set; }
        bool TargetPrevLine { get; set; }
        Image TargetPreviewImage { get; set; }
        //Gender

        //Settings
        string ProtagonistName { get; set; }
        bool BackgroundEnabled { get; set; }
        bool SyncNavigation { get; set; }
        int CurrentTextboxTheme { get; set; }

        void SetController(MainController controller);
        void SetMessageList(DataTable messageTable, bool target);
    }
}
