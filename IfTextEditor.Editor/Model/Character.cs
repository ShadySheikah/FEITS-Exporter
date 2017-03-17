using System.Drawing;
using System.Resources;

namespace IfTextEditor.Editor.Model
{
    internal class Character
    {
        //Info
        internal string Name { private get; set; }
        internal string LocalizedName { get; set; }
        internal Color HairColor { private get; set; }

        //Player-specific
        internal bool IsPlayer { private get; set; }
        internal int PlayerFace { get; set; }
        internal char PlayerEyes { get; set; }

        //Emote layer positions
        internal Point SweatPos { private get; set; }
        internal Point BlushPos { private get; set; }
        internal Point CropPos { private get; set; }

        private readonly ResourceManager manager;

        internal Character()
        {
            manager = Resources.Properties.Resources.ResourceManager;
        }

        internal Bitmap GetCharacterImage(Emote[] emotes, Gender gender, bool cropped)
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

        internal Bitmap GetHair(Gender gender, bool cropped)
        {
            string hair = (IsPlayer ? Name + (gender == Gender.Male ? "男" : "女") + PlayerFace : Name) + (cropped ? "_bu_" : "_st_") + "髪0";
            var hairImg = (Bitmap) manager.GetObject(hair);

            return hairImg != null ? CharacterData.ColorizeImage(hairImg, HairColor, SkiaSharp.SKBlendMode.Overlay) : null;
        }

        internal Bitmap CropImage(Bitmap baseImage)
        {
            var croppedImg = new Bitmap(70, 49);

            using (Graphics g = Graphics.FromImage(croppedImg))
                g.DrawImage(baseImage, CropPos);

            return croppedImg;
        }
    }
}
