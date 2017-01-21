using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace FEITS.Model
{
    public class FontCharacter
    {
        public FontCharacterData Data { get; }
        private Image Glyph { get; set; }

        public FontCharacter(byte[] data, int ofs = 0)
        {
            Data = new FontCharacterData(data, ofs);
        }

        public void SetGlyph(Image img)
        {
            var crop = new Bitmap(Data.Width, Data.Height);
            using (var g = Graphics.FromImage(crop))
            {
                g.DrawImage((Bitmap) img,
                    new Rectangle(0, 0, Data.Width, Data.Height),
                    new Rectangle(Data.XOffset, Data.YOffset, Data.Width, Data.Height),
                    GraphicsUnit.Pixel);
            }
            Glyph = crop;
        }

        public Image GetGlyph(Color newColor)
        {
            var coloredGlyph = (Bitmap) Glyph;

            var rect = new Rectangle(0, 0, coloredGlyph.Width, coloredGlyph.Height);
            var bmpData = coloredGlyph.LockBits(rect, ImageLockMode.ReadWrite, coloredGlyph.PixelFormat);

            var ptr = bmpData.Scan0;

            var bytes = Math.Abs(bmpData.Stride)*coloredGlyph.Height;
            var rgbaValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbaValues, 0, bytes);

            for (var i = 0; i < rgbaValues.Length; i += 4)
            {
                if (rgbaValues[i + 3] <= 0) // Skip if CurrentColor.A <= 0
                    continue;
                rgbaValues[i + 2] = newColor.R;
                rgbaValues[i + 1] = newColor.G;
                rgbaValues[i + 0] = newColor.B;
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbaValues, 0, ptr, bytes);

            // Unlock the bits.
            coloredGlyph.UnlockBits(bmpData);
            return coloredGlyph;
        }
    }
}