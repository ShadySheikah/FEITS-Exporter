using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IfTextEditor.Editor.Model;

namespace IfTextEditor.Editor.Controller.Interface
{
    public interface IDebugView
    {
        //Message
        int MsgIndex { get; set; }
        int PageIndex { get; set; }
        int PageCount { get; set; }
        string SourceText { get; set; }
        string PageText { get; set; }

        void SetController(MainController controller);
        void SetMessageList(List<FileContainer.Message> messages);
    }
}
