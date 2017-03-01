using FEITS.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;

namespace FEITS.Model
{
    public class AssetGeneration
    {
        private static bool isInitialized = false;

        //Font
        private static bool[] validCharacters;
        public static bool[] ValidCharacters { get { return validCharacters; } }
        private static FontCharacter[] characters;
        private static Image[] Images = { Resources.Awakening_0, Resources.Awakening_1 };

        //Resources
        private static Dictionary<string, byte[]> faceData;
        private static List<string> resourceList = new List<string>();

        //Kamui
        private static string[] EyeStyles = { "a", "b", "c", "d", "e", "f", "g" };
        private static string[] Kamuis = { "マイユニ男1", "マイユニ女2" };

        static AssetGeneration()
        {
            
        }

        public static void Initialize(BackgroundWorker worker, DoWorkEventArgs e)
        {
            if(isInitialized)
            {
                Console.WriteLine("Assets already initialized!");
            }
            else
            {
                Console.WriteLine("Initializing assets...");

                //Set up font, generate list of valid chars
                validCharacters = new bool[0x10000];
                characters = new FontCharacter[0x10000];
                var charWidths = new Dictionary<char, int>();
                for (int i = 0; i < Resources.chars.Length / 0x10; i++)
                {
                    var fc = new FontCharacter(Resources.chars, i * 0x10);
                    validCharacters[fc.Value] = true;
                    fc.SetGlyph(Images[fc.IMG]);
                    characters[fc.Value] = fc;
                    if (!charWidths.ContainsKey(fc.Character))
                        charWidths.Add(fc.Character, fc.CropWidth);
                }

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FontData");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                List<string> lines = charWidths.Select(entry => entry.Key + "\t" + entry.Value).ToList();
                File.WriteAllLines(Path.Combine(path, "charWidths.txt"), lines, Encoding.UTF8);

                worker.ReportProgress(33);

                //Grab face data and assign to dictionary
                faceData = new Dictionary<string, byte[]>();
                string[] fids = Resources.FID.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < fids.Length; i++)
                {
                    byte[] dat = new byte[0x48];
                    Array.Copy(Resources.faces, i * 0x48, dat, 0, 0x48);
                    faceData[fids[i]] = dat;
                }

                var stageBlush = new List<string>();
                var stageSweat = new List<string>();
                var BuCropDimensions = new List<string>();
                var BuCropPos = new List<string>();
                foreach (KeyValuePair<string, byte[]> entry in faceData)
                {
                    if (entry.Key.Contains("_ST_"))
                    {
                        stageBlush.Add(entry.Key.Substring(8) + '\t' + BitConverter.ToUInt16(entry.Value, 0x38) + "," + BitConverter.ToUInt16(entry.Value, 0x3A));
                        stageSweat.Add(entry.Key.Substring(8) + '\t' + BitConverter.ToUInt16(entry.Value, 0x40) + "," + BitConverter.ToUInt16(entry.Value, 0x42));
                        continue;
                    }

                    BuCropDimensions.Add(entry.Key.Substring(8) + '\t' + BitConverter.ToUInt16(entry.Value, 0x34) + "," + BitConverter.ToUInt16(entry.Value, 0x36));
                    BuCropPos.Add(entry.Key.Substring(8) + '\t' + -BitConverter.ToUInt16(entry.Value, 0x30) + "," + -BitConverter.ToUInt16(entry.Value, 0x32));
                }

                File.WriteAllLines(Path.Combine(path, "FaceSweat.txt"), stageSweat, Encoding.UTF8);
                File.WriteAllLines(Path.Combine(path, "FaceBlush.txt"), stageBlush, Encoding.UTF8);
                File.WriteAllLines(Path.Combine(path, "CroppedDimensions.txt"), BuCropDimensions, Encoding.UTF8);
                File.WriteAllLines(Path.Combine(path, "CroppedPositions.txt"), BuCropPos, Encoding.UTF8);

                worker.ReportProgress(66);

                ResourceSet set = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
                foreach (DictionaryEntry o in set)
                {
                    resourceList.Add(o.Key as string);
                }

                worker.ReportProgress(100);
                Resources.ResourceManager.ReleaseAllResources();

                isInitialized = true;
            }
        }

        public static Image DrawString(Image BaseImage, string Message, int StartX, int StartY, Color? TC = null)
        {
            Color TextColor = TC.HasValue ? TC.Value : Color.Black;
            int CurX = StartX;
            int CurY = StartY;
            Bitmap NewImage = BaseImage.Clone() as Bitmap;

            //var charWidths = new List<string>();

            using (Graphics g = Graphics.FromImage(NewImage))
            {
                g.Clear(Color.DeepSkyBlue);

                foreach (char c in Message)
                {
                    if (c == '\n')
                    {
                        CurY += 20;
                        CurX = StartX;
                    }
                    else
                    {
                        FontCharacter cur = characters[GetValue(c)];
                        g.DrawImage(cur.GetGlyph(TextColor), new Point(CurX, CurY - cur.CropHeight));
                        CurX += cur.CropWidth;

                        //charWidths.Add(string.Format(c + "\t" + cur.CropWidth));
                    }
                }
            }

            //foreach (string str in charWidths)
            //{
            //    Debug.WriteLine(str);
            //}
            return NewImage;
        }

        public static Image GetCharacterStageImage(string CName, string CEmo, Color HairColor, bool Slot1, int PGender)
        {
            //Names
            bool USER = CName == "username";
            string hairname = "_st_髪";
            string dat_id = "FSID_ST_" + CName;
            if (USER)
            {
                dat_id = "FSID_ST_" + (new[] { "マイユニ_男1", "マイユニ_女2" })[PGender] + "_顔" + EyeStyles[0].ToUpper();
                CName = EyeStyles[0] + Kamuis[PGender];
                hairname = CName.Substring(1) + hairname + 0;
            }
            else
                hairname = CName + hairname + "0";

            //Emotion parsing
            var Emos = CEmo.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string resname = CName + "_st_" + Emos[0];

            //Grab image
            Image C;
            if (resourceList.Contains(resname))
                C = Resources.ResourceManager.GetObject(resname) as Image;
            else
                C = new Bitmap(1, 1);

            //Drawing
            using (Graphics g = Graphics.FromImage(C))
            {
                for (int i = 1; i < Emos.Length; i++)
                {
                    //Secondary emotions
                    string exresname = CName + "_st_" + Emos[i];
                    if (Emos[i] == "汗" && resourceList.Contains(exresname))
                    {
                        g.DrawImage(Resources.ResourceManager.GetObject(exresname) as Image, new Point(BitConverter.ToUInt16(faceData[dat_id], 0x40), BitConverter.ToUInt16(faceData[dat_id], 0x42)));
                    }
                    else if (Emos[i] == "照" && resourceList.Contains(exresname))
                    {
                        g.DrawImage(Resources.ResourceManager.GetObject(exresname) as Image, new Point(BitConverter.ToUInt16(faceData[dat_id], 0x38), BitConverter.ToUInt16(faceData[dat_id], 0x3A)));
                    }
                }

                //Hair
                if (resourceList.Contains(hairname))
                {
                    Bitmap hair = Resources.ResourceManager.GetObject(hairname) as Bitmap;
                    g.DrawImage(ColorHair(hair, HairColor), new Point(0, 0));
                }
            }

            //Mirror
            if (Slot1)
                C.RotateFlip(RotateFlipType.RotateNoneFlipX);

            return C;
        }

        public static Image GetCharacterBUImage(string CName, string CEmo, Color HairColor, bool Crop, int PGender)
        {
            string hairname = "_bu_髪";
            string dat_id = "FSID_BU_" + CName;
            bool USER = CName == "username";
            if (USER)
            {
                dat_id = "FSID_BU_" + (new[] { "マイユニ_男1", "マイユニ_女2" })[PGender] + "_顔" + EyeStyles[0].ToUpper();
                CName = EyeStyles[0] + Kamuis[PGender];
                hairname = CName.Substring(1) + hairname + 0;
            }
            else
                hairname = CName + hairname + "0";
            var Emos = CEmo.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string resname = CName + "_bu_" + Emos[0];
            Image C;
            if (resourceList.Contains(resname))
                C = Resources.ResourceManager.GetObject(resname) as Image;
            else
                C = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(C))
            {
                for (int i = 1; i < Emos.Length; i++)
                {
                    string exresname = CName + "_bu_" + Emos[i];
                    if (Emos[i] == "汗" && resourceList.Contains(exresname))
                    {
                        Debug.WriteLine(dat_id + ": " + BitConverter.ToUInt16(faceData[dat_id], 0x40) + "," + BitConverter.ToUInt16(faceData[dat_id], 0x42));
                        g.DrawImage(Resources.ResourceManager.GetObject(exresname) as Image, new Point(BitConverter.ToUInt16(faceData[dat_id], 0x40), BitConverter.ToUInt16(faceData[dat_id], 0x42)));
                    }
                    else if (Emos[i] == "照" && resourceList.Contains(exresname))
                    {
                        Debug.WriteLine(dat_id + ": " + BitConverter.ToUInt16(faceData[dat_id], 0x38) + "," + BitConverter.ToUInt16(faceData[dat_id], 0x3A));
                        g.DrawImage(Resources.ResourceManager.GetObject(exresname) as Image, new Point(BitConverter.ToUInt16(faceData[dat_id], 0x38), BitConverter.ToUInt16(faceData[dat_id], 0x3A)));
                    }
                }
                if (resourceList.Contains(hairname))
                {
                    Bitmap hair = Resources.ResourceManager.GetObject(hairname) as Bitmap;
                    g.DrawImage(ColorHair(hair, HairColor), new Point(0, 0));
                }
            }
            if (Crop)
            {
                Bitmap Cropped = new Bitmap(BitConverter.ToUInt16(faceData[dat_id], 0x34), BitConverter.ToUInt16(faceData[dat_id], 0x36));
                using (Graphics g = Graphics.FromImage(Cropped))
                {
                    g.DrawImage(C, new Point(-BitConverter.ToUInt16(faceData[dat_id], 0x30), -BitConverter.ToUInt16(faceData[dat_id], 0x32)));
                }
                C = Cropped;
            }
            C.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return C;
        }

        public static Image ColorHair(Image Hair, Color C)
        {
            Bitmap bmp = Hair as Bitmap;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbaValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbaValues, 0, bytes);

            for (int i = 0; i < rgbaValues.Length; i += 4)
            {
                if (rgbaValues[i + 3] > 0)
                {
                    rgbaValues[i + 2] = BlendOverlay(C.R, rgbaValues[i + 2]);
                    rgbaValues[i + 1] = BlendOverlay(C.G, rgbaValues[i + 1]);
                    rgbaValues[i + 0] = BlendOverlay(C.B, rgbaValues[i + 0]);
                }
            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbaValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        public static byte BlendOverlay(byte Src, byte Dst)
        {
            return ((Dst < 128) ? (byte)Math.Max(Math.Min((Src / 255.0f * Dst / 255.0f) * 255.0f * 2, 255), 0) : (byte)Math.Max(Math.Min(255 - ((255 - Src) / 255.0f * (255 - Dst) / 255.0f) * 255.0f * 2, 255), 0));
        }

        public static Image Fade(Image BaseImage)
        {

            Bitmap bmp = BaseImage as Bitmap;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbaValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbaValues, 0, bytes);

            const double BLACK_A = 113.0 / 255.0;

            for (int i = 0; i < rgbaValues.Length; i += 4)
            {
                if (rgbaValues[i + 3] <= 0) continue;
                double DST_A = rgbaValues[i + 3] / 255.0;
                // double FINAL_A = BLACK_A + (DST_A) * (1.0 - BLACK_A);
                // rgbaValues[i + 3] = (byte)Math.Round((FINAL_A) * 255.0);
                rgbaValues[i + 2] = (byte)Math.Round((((rgbaValues[i + 2] / 255.0)) * (DST_A) * (1.0 - BLACK_A)) * 255.0);
                rgbaValues[i + 1] = (byte)Math.Round((((rgbaValues[i + 1] / 255.0)) * (DST_A) * (1.0 - BLACK_A)) * 255.0);
                rgbaValues[i + 0] = (byte)Math.Round((((rgbaValues[i + 0] / 255.0)) * (DST_A) * (1.0 - BLACK_A)) * 255.0);
            }
            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbaValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        public static ushort GetValue(char c)
        {
            return BitConverter.ToUInt16(Encoding.Unicode.GetBytes(string.Empty + c), 0);
        }

        public static int GetLength(string s)
        {
            return s.Select(GetValue).Select(val => Math.Max(characters[val].Width, characters[val].CropWidth)).Sum();
        }
    }
}
