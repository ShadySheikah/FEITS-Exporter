using FEITS.Properties;
using FEITS.Model;
using System;
using System.Drawing;
using FEITS.View;

namespace FEITS.Controller
{
    public class HalfBoxController
    {
        private IHalfBoxView View { get; }
        private Image[] Textboxes { get; } = {Resources.HalfBox, Resources.HalfBox_Nohr, Resources.HalfBox_Hoshido};

        public HalfBoxController(IHalfBoxView view)
        {
            View = view;
            View.SetController(this);
            SetControls();
        }

        private void SetControls()
        {
            View.CurrentTextboxIndex = 0;
            View.CurrentLine = "This is an example\r\nmessage.";
        }

        public Image UpdatePreview()
        {
            Image hb = (Bitmap) Textboxes[View.CurrentTextboxIndex].Clone();
            Image text =
                (Bitmap)
                    AssetGeneration.DrawString(new Bitmap(165, 50), View.CurrentLine.Replace(Environment.NewLine, "\n"),
                        0, 22, Color.FromArgb(68, 8, 0));

            using (var g = Graphics.FromImage(hb))
            {
                g.DrawImage(text, new Point(10, 0));
                g.DrawImage(Resources.KeyPress,
                    new Point(View.PreviewImage.Width - 30, View.PreviewImage.Height - hb.Height + 32));
            }

            return hb;
        }

        public void ExportText()
        {
            using (var lineExporter = new ScriptExport())
            {
                //TODO(Robin): ???
                var exportCont = new ImportExportController(lineExporter,
                    View.CurrentLine.Replace(Environment.NewLine, "\\n"));
                var dialogResult = lineExporter.ShowDialog();
            }
        }
    }
}