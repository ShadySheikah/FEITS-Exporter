using FEITS.Properties;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;

namespace FEITS.Model
{
    public class ConversationModel
    {
        //File container
        public ParsedFileContainer File { get; set; } = new ParsedFileContainer();
        private int messageIndex;
        public int MessageIndex
        {
            get { return messageIndex; }
            set { messageIndex = value;  lineIndex = 0; ResetParameters(); }
        }
        private int lineIndex;
        public int LineIndex { get { return lineIndex; } set { lineIndex = value; } }

        //Settings
        public string PlayerName = "Kamui";
        public int PlayerGender = 0;
        private bool enableBackgrounds;
        public bool EnableBackgrounds
        {
            get { return enableBackgrounds; }
            set
            {
                //Changing the value should set the background image to default
                enableBackgrounds = value;
                BackgroundImage = Resources.SupportBG;
            }
        }
        public Image BackgroundImage;
        public Image[] TextBoxes = { Resources.TextBox, Resources.TextBox_Nohr, Resources.TextBox_Hoshido };
        public int TextboxIndex;

        //Character names with their translations
        private Dictionary<string, string> names;

        //
        private Color colorA = Color.FromArgb(0x5B, 0x58, 0x55);
        private Color colorB = Color.FromArgb(0x5B, 0x58, 0x55);

        //Line commands
        private bool hasPerms;
        public bool HasPerms { get { return hasPerms; } }
        private bool setType;
        public bool SetType { get { return setType; } }
        private string charActive = string.Empty;
        public string CharActive { get { return charActive; } }
        private string charA = string.Empty;
        public string CharA { get { return charA; } }
        private string charB = string.Empty;
        public string CharB { get { return charB; } }
        private int charSide = -1;
        public int CharSide { get { return charSide; } }
        private const string defaultEmotion = "通常,";
        private string emotionA = defaultEmotion;
        public string EmotionA { get { return emotionA; } }
        private string emotionB = defaultEmotion;
        public string EmotionB { get { return emotionB; } }
        private ConversationTypes ConversationType = ConversationTypes.Type1;

        public ConversationModel()
        {
            //Grab names from PID and sort them into a dictionary
            names = new Dictionary<string, string>();
            string[] pids = Resources.PID.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in pids)
            {
                var p = str.Split(new[] { '\t' });
                names[p[0]] = p[1];
            }
        }

        public void ResetParameters()
        {
            hasPerms = setType = false;
            charActive = charA = charB = string.Empty;
            emotionA = emotionB = defaultEmotion;
            charSide = -1;
            ConversationType = ConversationTypes.Type1;
        }

        public void GetCommandsUpUntilIndex()
        {
            ResetParameters();

            for(int i = 0; i < lineIndex; i++)
            {
                GetParsedCommands(File.MessageList[messageIndex].MessageLines[i]);
            }
        }

        public string GetParsedCommands(MessageLine line)
        {
            if(line.SpokenText != string.Empty)
            {
                line.UpdateRawWithNewDialogue();
                line.RawLine = line.RawLine.Replace("\\n", "\n").Replace("$k\n", "$k\\n");
            }

            line.SpokenText = line.RawLine;

            for(int i = 0; i < line.SpokenText.Length; i++)
            {
                if(line.SpokenText[i] == '$')
                {
                    Tuple<string, Command> res = MessageBlock.ParseCommand(line.SpokenText, i);
                    line.SpokenText = res.Item1;

                    if (res.Item2.numParams > 0)
                        if (res.Item2.Params[0] == "ベロア")
                            res.Item2.Params[0] = "べロア"; // Velour Fix

                    switch(res.Item2.cmd)
                    {
                        case "$E":
                            if (charActive != string.Empty && charActive == charB)
                            {
                                if (res.Item2.Params[0] != ",")
                                    emotionB = res.Item2.Params[0];
                                else
                                    emotionB = defaultEmotion;
                            }
                            else
                            {
                                if (res.Item2.Params[0] != ",")
                                    emotionA = res.Item2.Params[0];
                                else
                                    emotionA = defaultEmotion;
                            }
                            break;
                        case "$Ws":
                            charActive = res.Item2.Params[0];
                            break;
                        case "$Wm":
                            charSide = Convert.ToInt32(res.Item2.Params[1]);

                            if(ConversationType == ConversationTypes.Type1)
                            {
                                if(charSide == 3)
                                {
                                    charA = res.Item2.Params[0];
                                    emotionA = defaultEmotion;
                                }
                                else if(charSide == 7)
                                {
                                    charB = res.Item2.Params[0];
                                    emotionB = defaultEmotion;
                                }
                            }
                            else
                            {
                                if(charSide == 0| charSide == 2)
                                {
                                    charA = res.Item2.Params[0];
                                    emotionA = defaultEmotion;
                                }
                                else if(charSide == 6)
                                {
                                    charB = res.Item2.Params[0];
                                    emotionB = defaultEmotion;
                                }
                            }
                            break;
                        case "$Wd":
                            if(charActive == charB)
                            {
                                charActive = charA;
                                charB = string.Empty;
                            }
                            else
                            {
                                charA = string.Empty;
                            }
                            break;
                        case "$a":
                            hasPerms = true;
                            break;
                        case "$t0":
                            if (!setType)
                                ConversationType = ConversationTypes.Type0;
                            setType = true;
                            break;
                        case "$t1":
                            if (!setType)
                                ConversationType = ConversationTypes.Type1;
                            setType = true;
                            break;
                        case "$Nu":
                            line.SpokenText = line.SpokenText.Substring(0, i) + "$Nu" + line.SpokenText.Substring(i);
                            i += 2;
                            break;
                        case "$Wa":
                            break;
                        case "$Wc":
                            break;
                        default:
                            break;
                    }
                    i--;
                }
            }

            if(string.IsNullOrWhiteSpace(line.SpokenText))
            {
                line.SpokenText = string.Empty;
            }

            line.SpeechIndex = line.RawLine.LastIndexOf(line.SpokenText);
            line.RawLine = line.RawLine.Replace("\\n", "\n").Replace("$k\n", "$k\\n");
            line.SpokenText.Replace("\\n", "\n").Replace("$k\n", "$k\\n");
            line.SpokenText = line.SpokenText.Replace("\n", Environment.NewLine);

            return line.SpokenText;
        }

        #region BoxRender
        public Image RenderPreviewBox(string line)
        {
            return ConversationType == ConversationTypes.Type1 ? RenderTypeOne(line) : RenderTypeZero();
        }

        private Image RenderTypeOne(string line)
        {
            //Probably shouldn't be hard-coded
            Bitmap box = new Bitmap(400, 240);
            Bitmap tb = TextBoxes[TextboxIndex].Clone() as Bitmap;

            //Generate text image from string
            if (line.Contains("$Nu") && hasPerms)
            {
                line = line.Replace("$Nu", PlayerName);
            }

            line = line.Replace(Environment.NewLine, "\n");

            //Draw the line's text
            Bitmap text = AssetGeneration.DrawString(new Bitmap(310, 50), line, 0, 22, Color.FromArgb(68, 8, 0)) as Bitmap;

            using (Graphics g = Graphics.FromImage(tb))
            {
                g.DrawImage(text, new Point(29, 0));
            }

            //Name box
            string name = names.ContainsKey(charActive) ? names[charActive] : (charActive == "username" ? PlayerName : charActive);
            int nameLength = AssetGeneration.GetLength(name);
            Bitmap nb = AssetGeneration.DrawString(Resources.NameBox, name, Resources.NameBox.Width / 2 - nameLength / 2, 16, Color.FromArgb(253, 234, 177)) as Bitmap;  //Center name in NameBox

            using (Graphics g = Graphics.FromImage(box))
            {
                //Background
                if (enableBackgrounds)
                {
                    g.DrawImage(BackgroundImage, new Point(0, 0));
                }

                //Character A
                if (charA != string.Empty)
                {
                    Image ca = AssetGeneration.GetCharacterStageImage(charA, emotionA, colorA, true, PlayerGender);
                    g.DrawImage((charActive == charA) ? ca : AssetGeneration.Fade(ca), new Point(-28, box.Height - ca.Height + 14));
                }

                //Character B
                if (charB != string.Empty)
                {
                    Image cb = AssetGeneration.GetCharacterStageImage(charB, emotionB, colorB, false, PlayerGender);
                    g.DrawImage((charActive == charB) ? cb : AssetGeneration.Fade(cb), new Point(box.Width - cb.Width + 28, box.Height - cb.Height + 14));
                }

                //Textbox
                g.DrawImage(tb, new Point(10, box.Height - tb.Height + 2));

                //Name box
                if (charActive != string.Empty)
                {
                    g.DrawImage(nb, charActive == charB ? new Point(box.Width - nb.Width - 6, box.Height - tb.Height - 14) : new Point(7, box.Height - tb.Height - 14));
                }

                //Arrow
                if (lineIndex > File.MessageList[messageIndex].MessageLines.Count - 1)
                {
                    g.DrawImage(Resources.KeyPress, new Point(box.Width - 33, box.Height - tb.Height + 32));
                }
            }

            return box;
        }

        private Image RenderTypeZero()
        {
            string topLine = string.Empty, bottomLine = string.Empty;
            ResetParameters();

            for (int i = 0; i <= lineIndex; i++)
            {
                string line = GetParsedCommands(File.MessageList[messageIndex].MessageLines[i]);

                if (line.Contains("$Nu") && hasPerms)
                {
                    line = line.Replace("$Nu", PlayerName);
                }

                line = line.Replace(Environment.NewLine, "\n");

                if (charActive == charA)
                    topLine = line;
                else
                    bottomLine = line;
            }

            //Hard coded dimensions
            Bitmap box = new Bitmap(400, 240);
            Bitmap topBox = new Bitmap(1, 1), bottomBox = new Bitmap(1, 1);
            if (topLine != string.Empty && charA != string.Empty)
            {
                topBox = (TextBoxes[TextboxIndex].Clone()) as Bitmap;
                using (Graphics g = Graphics.FromImage(topBox))
                {
                    g.DrawImage(AssetGeneration.GetCharacterBUImage(charA, emotionA, colorA, true, PlayerGender), new Point(2, 3));
                    g.DrawImage(AssetGeneration.DrawString(new Bitmap(260, 50), topLine, 0, 22, Color.FromArgb(68, 8, 0)), new Point(76, 0));
                }
            }

            if (bottomLine != string.Empty && charB != string.Empty)
            {
                bottomBox = (TextBoxes[TextboxIndex].Clone()) as Bitmap;
                using (Graphics g = Graphics.FromImage(bottomBox))
                {
                    g.DrawImage(AssetGeneration.GetCharacterBUImage(charB, emotionB, colorB, true, PlayerGender), new Point(2, 3));
                    g.DrawImage(AssetGeneration.DrawString(new Bitmap(282, 50), bottomLine, 0, 22, Color.FromArgb(68, 8, 0)), new Point(76, 0));
                }
            }

            using (Graphics g = Graphics.FromImage(box))
            {
                if (lineIndex < File.MessageList[messageIndex].MessageLines.Count - 1)
                {
                    using (Graphics g2 = Graphics.FromImage(charActive == charA ? topBox : bottomBox))
                    {
                        g2.DrawImage(Resources.KeyPress, new Point(TextBoxes[TextboxIndex].Width - 30, 32));
                    }
                }

                if (enableBackgrounds)
                    g.DrawImage(BackgroundImage, new Point(0, 0));
                g.DrawImage(topBox, new Point(10, 3));
                g.DrawImage(bottomBox, new Point(10, box.Height - bottomBox.Height + 2));

                if (topLine != string.Empty && charA != string.Empty)
                {
                    string topName = names.ContainsKey(charA) ? names[charA] : (charA == "username" ? PlayerName : charA);
                    int nameLen = AssetGeneration.GetLength(topName);
                    Bitmap topNameBox = AssetGeneration.DrawString(Resources.NameBox, topName, Resources.NameBox.Width / 2 - nameLen / 2, 16, Color.FromArgb(253, 234, 177)) as Bitmap; //Center name in NameBox
                    g.DrawImage(topNameBox, new Point(7, topBox.Height - (topNameBox.Height - 20)));
                }

                if (bottomLine != string.Empty && charB != string.Empty)
                {
                    string bottomName = names.ContainsKey(charB) ? names[charB] : (charB == "username" ? PlayerName : charB);
                    int nameLen = AssetGeneration.GetLength(bottomName);
                    Bitmap bottomNameBox = AssetGeneration.DrawString(Resources.NameBox, bottomName, Resources.NameBox.Width / 2 - nameLen / 2, 16, Color.FromArgb(253, 234, 177)) as Bitmap;
                    g.DrawImage(bottomNameBox, new Point(7, box.Height - bottomBox.Height - 14));
                }
            }
            return box;
        }
        #endregion

        public Image RenderConversation()
        {
            foreach(MessageLine line in File.MessageList[messageIndex].MessageLines)
                line.UpdateRawWithNewDialogue();

            List<Image> images = new List<Image>();
            int index = lineIndex;
            ResetParameters();
            for(int i = 0; i < File.MessageList[messageIndex].MessageLines.Count; i++)
            {
                GetParsedCommands(File.MessageList[messageIndex].MessageLines[i]);
                string parsed = File.MessageList[messageIndex].MessageLines[i].SpokenText;
                if(!string.IsNullOrWhiteSpace(parsed) && !string.IsNullOrEmpty(parsed))
                {
                    images.Add(RenderPreviewBox(parsed));
                }
            }
            lineIndex = index;
            Bitmap bmp = new Bitmap(images.Max(i => i.Width), images.Sum(i => i.Height));
            using (Graphics g = Graphics.FromImage(bmp))
            {
                int h = 0;
                foreach(Image img in images)
                {
                    g.DrawImage(img, new Point(0, h));
                    h += img.Height;
                }
            }

            return bmp;
        }
    }
}
