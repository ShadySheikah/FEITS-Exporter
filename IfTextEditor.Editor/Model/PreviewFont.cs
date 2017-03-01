using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace IfTextEditor.Editor.Model
{
    public class PreviewFont
    {
        private static SKTypeface typeface;
        private static Dictionary<char, int> charWidths;

        public static void Initialize()
        {
            ObtainFont();
            EstablishCharWidths();
        }

        private static void ObtainFont()
        {
            byte[] font = Resources.Properties.Resources.Chiaro;
            var stream = new SKManagedStream(new MemoryStream(font));
            typeface = SKTypeface.FromStream(stream);

        }

        private static void EstablishCharWidths()
        {
            charWidths = new Dictionary<char, int>();
            string[] lines = Resources.Properties.Resources.CharWidths.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string str in lines)
            {
                string[] vals = str.Split('\t');
                char key = Convert.ToChar(vals[0]);
                int value = Convert.ToInt32(vals[1]);

                if (!charWidths.ContainsKey(key))
                    charWidths.Add(key, value);
            }
        }

        public static int GetStringWidth(string line)
        {
            string[] lines = line.Replace(Environment.NewLine, "\n").Split('\n');
            var widths = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                foreach (char c in lines[i])
                    widths[i] += charWidths[c];
            }

            return widths.Max();
        }

        public static Image DrawString(Bitmap baseImage, string line, Color textColor, int posX, int posY)
        {
            string newLine = line.Replace(Environment.NewLine, "\n");
            int curX = posX, curY = posY;

            BitmapData data = baseImage.LockBits(new Rectangle(0, 0, baseImage.Width, baseImage.Height), ImageLockMode.WriteOnly, baseImage.PixelFormat);
            var info = new SKImageInfo(baseImage.Width, baseImage.Height);

            using (SKSurface surface = SKSurface.Create(info, data.Scan0, baseImage.Width * 4))
            {
                //Debug
                //surface.Canvas.Clear(SKColors.AliceBlue);

                using (var paint = new SKPaint())
                {
                    paint.Typeface = typeface;
                    paint.TextSize = 15f;
                    paint.IsAntialias = true;
                    paint.Color = textColor.ToSKColor();

                    foreach (char c in newLine)
                    {
                        if (c == '\n')
                        {
                            curY += 20;
                            curX = posX;
                            continue;
                        }

                        surface.Canvas.DrawPositionedText(c.ToString(), new[] {new SKPoint(curX, curY + 15)}, paint);
                        curX += charWidths[c];
                    }
                }
            }

            baseImage.UnlockBits(data);
            return baseImage;
        }
    }
}
