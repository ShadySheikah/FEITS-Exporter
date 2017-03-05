using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace IfTextEditor.Editor.Model
{
    public class Character
    {
        //Info
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public Color HairColor { get; set; }

        //Player-specific
        public bool IsPlayer { get; set; }
        public int PlayerFace { get; set; }
        public char PlayerEyes { get; set; }

        //Emote layer positions
        public Point SweatPos { get; set; }
        public Point BlushPos { get; set; }
        public Point CropPos { get; set; }

        private readonly ResourceManager manager;

        public Character()
        {
            manager = Resources.Properties.Resources.ResourceManager;
        }

        public Bitmap GetCharacterImage(Emote[] emotes, Gender gender, bool cropped)
        {
            //Resource file name
            string baseName = (IsPlayer ? PlayerEyes + Name + (gender == Gender.Male ? "男" : "女") + PlayerFace : Name) + (cropped ? "_bu_" : "_st_");

            //Grab image
            Bitmap image = (Bitmap) manager.GetObject(baseName + CharacterData.EmoteLookup[emotes[0]]) ?? new Bitmap(1, 1);

            using (Graphics g = Graphics.FromImage(image))
            {
                //Secondary emotion
                if (emotes.Length > 1)
                {
                    string secEmo = CharacterData.EmoteLookup[emotes[1]];
                    switch (emotes[1])
                    {
                        case Emote.Sweat:
                            var sweat = (Bitmap) manager.GetObject(baseName + secEmo);
                            g.DrawImage(sweat, SweatPos);
                            break;
                        case Emote.Blush:
                            var blush = (Bitmap) manager.GetObject(baseName + secEmo);
                            g.DrawImage(blush, BlushPos);
                            break;
                    }
                }
            }

            return image;
        }

        public Bitmap GetHair(Gender gender, bool cropped)
        {
            string hair = (IsPlayer ? Name + (gender == Gender.Male ? "男" : "女") + PlayerFace : Name) + (cropped ? "_bu_" : "_st_") + "髪0";
            var hairImg = (Bitmap) manager.GetObject(hair);

            return hairImg != null ? CharacterData.ColorizeImage(hairImg, HairColor, SkiaSharp.SKBlendMode.Overlay) : null;
        }

        public Bitmap CropImage(Bitmap baseImage)
        {
            var croppedImg = new Bitmap(70, 49);

            using (Graphics g = Graphics.FromImage(croppedImg))
                g.DrawImage(baseImage, CropPos);

            return croppedImg;
        }
    }
}
