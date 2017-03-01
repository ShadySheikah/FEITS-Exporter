using System.Drawing;

namespace FEITS.Controller
{
    public interface IHalfBoxView
    {
        string CurrentLine { get; set; }
        Image PreviewImage { get; set; }
        int CurrentTextboxIndex { get; set; }
        bool CanExport { get; set; }

        void SetController(HalfBoxController controller);
    }
}
