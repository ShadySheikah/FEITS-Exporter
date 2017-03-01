using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace FEITS.Model
{
    public class FontCharacter
    {
        public ushort Value;
        public ushort IMG;
        public ushort XOfs;
        public ushort YOfs;
        public byte Width;
        public byte Height;
        public byte Padding1;
        public int CropHeight;
        public int CropWidth;
        public byte[] Padding2; // Size = 3;

        public char Character;

        public Image Glyph;

        public FontCharacter() { }

        public FontCharacter(byte[] Data) : this(Data, 0) { }

        public FontCharacter(byte[] Data, int ofs)
        {
            Value = BitConverter.ToUInt16(Data, ofs + 0);
            IMG = BitConverter.ToUInt16(Data, ofs + 2);
            XOfs = BitConverter.ToUInt16(Data, ofs + 4);
            YOfs = BitConverter.ToUInt16(Data, ofs + 6);
            Width = Data[ofs + 8];
            Height = Data[ofs + 9];
            Padding1 = Data[ofs + 0xA];
            CropHeight = Data[ofs + 0xB];
            if (CropHeight > 0x7F)
                CropHeight -= 256;
            CropWidth = Data[ofs + 0xC];
            if (CropWidth > 0x7F)
                CropWidth -= 256;
            Padding2 = new byte[0x3];
            Array.Copy(Data, ofs + 0xD, Padding2, 0, 3);

            Character = Encoding.Unicode.GetString(Data, ofs + 0, 2)[0];
        }

        public void PrintToFile(int index)
        {
            var lines = new List<string>
            {
                "Character\t" + Character,
                "Value\t" + Value,
                "IMG\t" + IMG,
                "XOfs\t" + XOfs,
                "YOfs\t" + YOfs,
                "Width\t" + Width,
                "Height\t" + Height,
                "Padding1\t" + Padding1,
                "CropHeight\t" + CropHeight,
                "CropWidth\t" + CropWidth
            };
            lines.AddRange(Padding2.Select(b => "Padding2\t" + b));

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Font");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllLines(Path.Combine(path, "FONT_CHARACTER_" + index + ".txt"), lines, Encoding.UTF8);
        }

        public void SetGlyph(Image img)
        {
            Bitmap crop = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(crop))
            {
                g.DrawImage(img as Bitmap, new Rectangle(0, 0, Width, Height), new Rectangle(XOfs, YOfs, Width, Height), GraphicsUnit.Pixel);
            }
            Glyph = crop;
        }

        public Image GetGlyph(Color NewColor)
        {
            Bitmap ColoredGlyph = Glyph as Bitmap;

            Rectangle rect = new Rectangle(0, 0, ColoredGlyph.Width, ColoredGlyph.Height);
            BitmapData bmpData = ColoredGlyph.LockBits(rect, ImageLockMode.ReadWrite, ColoredGlyph.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * ColoredGlyph.Height;
            byte[] rgbaValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbaValues, 0, bytes);

            for (int i = 0; i < rgbaValues.Length; i += 4)
            {
                if (rgbaValues[i + 3] > 0) // If CurrentColor.A > 0
                {
                    rgbaValues[i + 2] = NewColor.R;
                    rgbaValues[i + 1] = NewColor.G;
                    rgbaValues[i + 0] = NewColor.B;
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbaValues, 0, ptr, bytes);

            // Unlock the bits.
            ColoredGlyph.UnlockBits(bmpData);
            return ColoredGlyph;
        }
    }
}
