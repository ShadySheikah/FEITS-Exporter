using FEITS.Properties;
using FEITS.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEITS.View;
using System.Windows.Forms;

namespace FEITS.Controller
{
    public class HalfBoxController
    {
        private IHalfBoxView view;
        private Image[] textboxes = { Resources.HalfBox, Resources.HalfBox_Nohr, Resources.HalfBox_Hoshido };

        public HalfBoxController(IHalfBoxView v)
        {
            view = v;
            view.SetController(this);
            SetControls();
        }

        private void SetControls()
        {
            view.CurrentTextboxIndex = 0;
            view.CurrentLine = "This is an example\r\nmessage.";
        }

        public Image UpdatePreview()
        {
            Image hb = textboxes[view.CurrentTextboxIndex].Clone() as Bitmap;
            Image text = AssetGeneration.DrawString(new Bitmap(165, 50), view.CurrentLine.Replace(Environment.NewLine, "\n"), 0, 22, Color.FromArgb(68, 8, 0)) as Bitmap;

            using (Graphics g = Graphics.FromImage(hb))
            {
                g.DrawImage(text, new Point(10, 0));
                g.DrawImage(Resources.KeyPress, new Point(view.PreviewImage.Width - 30, view.PreviewImage.Height - hb.Height + 32));
            }

            return hb;
        }

        public void ExportText()
        {
            using (ScriptExport lineExporter = new ScriptExport())
            {
                ImportExportController exportCont = new ImportExportController(lineExporter, view.CurrentLine.Replace(Environment.NewLine, "\\n"));
                DialogResult dialogResult = lineExporter.ShowDialog();
            }
        }
    }
}
