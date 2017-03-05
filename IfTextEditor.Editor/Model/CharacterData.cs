using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace IfTextEditor.Editor.Model
{
    public class CharacterData
    {
        private static bool isInitialized;

        public static Dictionary<Emote, string> EmoteLookup = new Dictionary<Emote, string>
        {
            {Emote.Normal, "通常"},
            {Emote.Smile, "笑"},
            {Emote.Pained, "苦"},
            {Emote.Angry, "怒"},
            {Emote.Signature, "キメ"},
            {Emote.Sweat, "汗"},
            {Emote.Blush, "照"}
        };

        //Kamui Defaults
        private static int playerFaceType = 1;
        private static char playerEyeType = 'a';

        private static Dictionary<string, Character> characters;

        public static void Initialize()
        {
            if (isInitialized)
                return;
            isInitialized = true;

            characters = new Dictionary<string, Character>();

            //Names
            string[] localNames = Resources.Properties.Resources.CharacterNames.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string names in localNames)
            {
                string[] vals = names.Split('\t');

                var newCharacter = new Character
                {
                    Name = vals[0],
                    LocalizedName = vals[1]
                };

                if (vals[1] == "Kamui")
                {
                    newCharacter.IsPlayer = true;
                    newCharacter.PlayerFace = playerFaceType;
                    newCharacter.PlayerEyes = playerEyeType;
                    vals[0] = "username";
                }

                characters.Add(vals[0], newCharacter);
            }

            //Hair color
            string[] hairLines = Resources.Properties.Resources.HairColors.Split(new[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in hairLines)
            {
                string[] vals = line.Split('\t');
                
                if(characters.ContainsKey(vals[0]))
                    characters[vals[0]].HairColor = ColorTranslator.FromHtml("#" + vals[1]);
            }

            //Point data
            string[] sweatLines = Resources.Properties.Resources.FaceSweat.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] blushLines = Resources.Properties.Resources.FaceBlush.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] cropLines = Resources.Properties.Resources.CroppedPositions.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sweatLines.Length; i++)
            {
                string[] sweatVals = sweatLines[i].Split('\t');
                string[] blushVals = blushLines[i].Split('\t');
                string[] cropVals = cropLines[i].Split('\t');

                if (sweatVals[0].StartsWith("マイユニ"))
                {
                    Character kamui = characters["username"];
                    if (sweatVals[0].Contains("男" + kamui.PlayerFace) && sweatVals[0].EndsWith(kamui.PlayerEyes.ToString().ToUpper()))
                    {
                        string[] kSweatPoints = sweatVals[1].Split(',');
                        string[] kBlushPoints = blushVals[1].Split(',');
                        string[] kCropPoints = cropVals[1].Split(',');

                        kamui.SweatPos = new Point(Convert.ToInt32(kSweatPoints[0]), Convert.ToInt32(kSweatPoints[1]));
                        kamui.BlushPos = new Point(Convert.ToInt32(kBlushPoints[0]), Convert.ToInt32(kBlushPoints[1]));
                        kamui.CropPos = new Point(Convert.ToInt32(kCropPoints[0]), Convert.ToInt32(kCropPoints[1]));
                        continue;
                    }
                }

                if (characters.ContainsKey(sweatVals[0]))
                {
                    string[] sweatPoints = sweatVals[1].Split(',');
                    string[] blushPoints = blushVals[1].Split(',');
                    string[] cropPoints = cropVals[1].Split(',');

                    characters[sweatVals[0]].SweatPos = new Point(Convert.ToInt32(sweatPoints[0]), Convert.ToInt32(sweatPoints[1]));
                    characters[sweatVals[0]].BlushPos = new Point(Convert.ToInt32(blushPoints[0]), Convert.ToInt32(blushPoints[1]));
                    characters[sweatVals[0]].CropPos = new Point(Convert.ToInt32(cropPoints[0]), Convert.ToInt32(cropPoints[1]));
                }
            }
        }

        public static void UpdatePlayerData(Gender gender, int faceType = 1, char eyeType = 'a')
        {
            Character player = characters["username"];
            player.PlayerFace = faceType;
            player.PlayerEyes = eyeType;

            string[] sweatLines = Resources.Properties.Resources.FaceSweat.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] blushLines = Resources.Properties.Resources.FaceBlush.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string[] cropLines = Resources.Properties.Resources.CroppedPositions.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < sweatLines.Length; i++)
            {
                if (sweatLines[i].StartsWith("マイユニ"))
                {
                    string[] sweatVals = sweatLines[i].Split('\t');
                    string[] blushVals = blushLines[i].Split('\t');
                    string[] cropVals = cropLines[i].Split('\t');

                    if (sweatVals[0].Contains((gender == Gender.Male ? "男" : "女") + player.PlayerFace) && sweatVals[0].EndsWith(player.PlayerEyes.ToString().ToUpper()))
                    {
                        string[] sweatPoints = sweatVals[1].Split(',');
                        player.SweatPos = new Point(Convert.ToInt32(sweatPoints[0]), Convert.ToInt32(sweatPoints[1]));

                        string[] blushPoints = blushVals[1].Split(',');
                        player.BlushPos = new Point(Convert.ToInt32(blushPoints[0]), Convert.ToInt32(blushPoints[1]));

                        string[] cropPoints = cropVals[1].Split(',');
                        player.CropPos = new Point(Convert.ToInt32(cropPoints[0]), Convert.ToInt32(cropPoints[1]));
                    }
                }
            }
        }

        public static Bitmap DrawCharacterImage(string name, Emote[] emotes, bool leftSide, Gender gender)
        {
            //Character
            Character character = characters[name];
            Bitmap charImage = character.GetCharacterImage(emotes, gender, false);

            //Hair
            Bitmap hairImage = character.GetHair(gender, false);
            if (hairImage != null)
                using (Graphics g = Graphics.FromImage(charImage))
                    g.DrawImage(hairImage, new Point(0, 0));

            //Mirror
            if (leftSide)
                charImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            return charImage;
        }

        public static Bitmap DrawCharacterCloseUpImage(string name, Emote[] emotes, Gender gender)
        {
            //Character
            Character character = characters[name];
            Bitmap charImage = character.GetCharacterImage(emotes, gender, true);

            //Hair
            Bitmap hairImage = character.GetHair(gender, true);
            if (hairImage != null)
                using (Graphics g = Graphics.FromImage(charImage))
                    g.DrawImage(hairImage, new Point(0, 0));

            //Crop
            charImage = character.CropImage(charImage);

            //Mirror
            charImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

            return charImage;
        }

        public static Bitmap ColorizeImage(Bitmap baseImage, Color newColor, SKBlendMode blendMode, byte alpha = 255)
        {
            BitmapData data = baseImage.LockBits(new Rectangle(0, 0, baseImage.Width, baseImage.Height), ImageLockMode.WriteOnly, baseImage.PixelFormat);
            var info = new SKImageInfo(baseImage.Width, baseImage.Height);

            using (SKSurface surface = SKSurface.Create(info, data.Scan0, baseImage.Width * 4))
            using (SKImage image = SKImage.FromPixels(info, data.Scan0, baseImage.Width * 4))
            using (var paint = new SKPaint())
            {
                SKColor color = newColor.ToSKColor();
                paint.Color = color;
                paint.BlendMode = SKBlendMode.SrcIn;
                surface.Canvas.DrawPaint(paint);

                using (SKImage img2 = surface.Snapshot())
                {
                    surface.Canvas.Clear();
                    paint.BlendMode = blendMode;
                    paint.Color = new SKColor(color.Red, color.Green, color.Blue, alpha);

                    surface.Canvas.DrawImage(image, SKRect.Create(image.Width, image.Height));
                    surface.Canvas.DrawImage(img2, SKRect.Create(img2.Width, img2.Height), paint);
                }
            }

            baseImage.UnlockBits(data);
            return baseImage;
        }

        public static string GetLocalizedName(string name)
        {
            return characters.ContainsKey(name) ? characters[name].LocalizedName : null;
        }
    }

    public enum Emote
    {
        Normal,
        Smile,
        Pained,
        Angry,
        Signature,
        Sweat,
        Blush
    }

    public enum Gender
    {
        Male,
        Female
    }
}
