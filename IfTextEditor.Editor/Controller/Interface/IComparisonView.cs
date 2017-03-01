using System.Collections.Generic;
using FEITS.Model;
using System.Drawing;

namespace FEITS.Controller
{
    public interface IComparisonView : IMainView
    {
        //Message controls
        int SourceMsgListIndex { get; set; }
        int SourceCurrentPage { get; set; }
        string SourceCurrentLine { get; set; }
        bool SourcePrevLine { get; set; }
        bool SourceNextLine { get; set; }
        Image SourcePreviewImage { get; set; }

        //Options
        bool SimultaneousControl { get; set; }
        int SourcePlayerGender { get; set; }

        //Status
        string SourcePageCount { set; }

        void SetSourceMessageList(List<MessageBlock> messages);
    }
}
