using FEITS.Properties;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace FEITS.Model
{
    public class ConversationModel
    {
        //File container
        public ParsedFileContainer File { get; } = new ParsedFileContainer();
        private int messageIndex;

        public int MessageIndex
        {
            get { return messageIndex; }
            set
            {
                messageIndex = value;
                LineIndex = 0;
                ResetParameters();
            }
        }

        public int LineIndex { get; set; }

        //Settings
        public string PlayerName = "Kamui";

        //TODO(Robin): Why does this exist?
        public PlayerGender PlayerGender = PlayerGender.Male;

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

        public Image BackgroundImage { private get; set; }

        private static readonly Image[] TextBoxes =
        {
            Resources.TextBox, Resources.TextBox_Nohr,
            Resources.TextBox_Hoshido
        };

        public int TextboxIndex { get; set; }

        //Character names with their translations
        private readonly IReadOnlyDictionary<string, string> names;

        //
        private static readonly Color DefaultColor = Color.FromArgb(0x5B, 0x58, 0x55);
        private readonly Color colorA = DefaultColor;
        private readonly Color colorB = DefaultColor;

        //Line commands
        private bool hasPerms;
        private bool setType;
        private string charActive = string.Empty;
        private string charA = string.Empty;

        private string charB = string.Empty;

        //TODO: Enum
        private int CharSide { get; set; } = -1;


        //TODO(Robin): Enum
        private const string DefaultEmotion = "通常,";
        private string emotionA = DefaultEmotion;
        private string emotionB = DefaultEmotion;

        private ConversationTypes conversationType = ConversationTypes.Type1;

        public ConversationModel()
        {
            //Grab names from PID and sort them into a dictionary
            var names = new Dictionary<string, string>();
            var pids = Resources.PID.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var str in pids)
            {
                var p = str.Split('\t');
                names[p[0]] = p[1];
            }
            this.names = new ReadOnlyDictionary<string, string>(names);
        }

        private void ResetParameters()
        {
            hasPerms = setType = false;
            charActive = charA = charB = string.Empty;
            emotionA = emotionB = DefaultEmotion;
            CharSide = -1;
            conversationType = ConversationTypes.Type1;
        }

        public void GetCommandsUpUntilIndex()
        {
            ResetParameters();

            for (var i = 0; i < LineIndex; i++)
            {
                GetParsedCommands(File.MessageList[messageIndex].MessageLines[i]);
            }
        }

        public string GetParsedCommands(MessageLine line)
        {
            //TODO(Robin): Move to CommandTokens?
            Func<string, bool, string> purify = (s, replaceEnvironment) =>
            {
                s = s.Replace("\\n", "\n")
                     .Replace("$k\n", "$k\\n");
                if (replaceEnvironment)
                    s = s.Replace("\n", Environment.NewLine);
                return s;
            };

            if (line.SpokenText != string.Empty)
            {
                line.UpdateRawWithNewDialogue();
                line.RawLine = purify(line.RawLine, false);
            }

            line.SpokenText = line.RawLine;

            for (var i = 0; i < line.SpokenText.Length; i++)
            {
                if (line.SpokenText[i] != '$') continue;

                var res = MessageBlock.ParseCommand(line.SpokenText, i);
                line.SpokenText = res.Item1;

                var command = res.Item2;
                var @params = command.Params;
                if (command.numParams > 0)
                    if (@params[0] == "ベロア")
                        @params[0] = "べロア"; // Velour Fix

                switch (command.CommandWithPrefix)
                {
                    case CommandTokens.E:
                        var emotion = @params[0] != "," ? @params[0] : DefaultEmotion;
                        if (string.IsNullOrEmpty(charActive) || charActive != charB)
                            emotionA = emotion;
                        else
                        {
                            Debug.Assert(charActive == charB, $"{charActive} == {charB}");
                            emotionB = emotion;
                        }
                        break;
                    case CommandTokens.Ws:
                        charActive = @params[0];
                        break;
                    case CommandTokens.Wm:
                        CharSide = Convert.ToInt32(@params[1]);

                        //NOTE(Robin): Prepare an exception for multiple possible fail states below
                        var unexpectedCharSideException = new ArgumentOutOfRangeException(nameof(CharSide), CharSide,
                            "Unexpected character side parameter.");

                        switch (conversationType)
                        {
                            case ConversationTypes.Type0:
                            {
                                switch (CharSide)
                                {
                                    case 0:
                                    case 2:
                                        charA = @params[0];
                                        emotionA = DefaultEmotion;
                                        break;
                                    case 6:
                                        charB = @params[0];
                                        emotionB = DefaultEmotion;
                                        break;
                                    default:
                                        throw unexpectedCharSideException;
                                }
                                break;
                            }
                            case ConversationTypes.Type1:
                            {
                                switch (CharSide)
                                {
                                    case 3:
                                        charA = @params[0];
                                        emotionA = DefaultEmotion;
                                        break;
                                    case 7:
                                        charB = @params[0];
                                        emotionB = DefaultEmotion;
                                        break;
                                    default:
                                        throw unexpectedCharSideException;
                                }
                                break;
                            }
                            default:
                                throw new ArgumentOutOfRangeException(nameof(conversationType), conversationType,
                                    "Unexpected conversation type.");
                        }
                        break;
                    case CommandTokens.Wd:
                        if (charActive == charB)
                        {
                            charActive = charA;
                            charB = string.Empty;
                        }
                        else
                        {
                            Debug.Assert(charActive == charA, $"{nameof(charActive)} == {nameof(charA)}");
                            charA = string.Empty;
                        }
                        break;
                    case CommandTokens.a:
                        hasPerms = true;
                        break;
                    case CommandTokens.t0:
                        if (!setType)
                            conversationType = ConversationTypes.Type0;
                        setType = true;
                        break;
                    case CommandTokens.t1:
                        if (!setType)
                            conversationType = ConversationTypes.Type1;
                        setType = true;
                        break;
                    case CommandTokens.Nu:
                        line.SpokenText = $"{line.SpokenText.Substring(0, i)}$Nu{line.SpokenText.Substring(i)}";
                        i += 2;
                        break;
                    default:
                        Debug.WriteLine($"Unhandled command: {command.CommandWithPrefix}");
                        if (!CommandTokens.IsValid(command.CommandWithPrefix))
                            throw new ArgumentOutOfRangeException(nameof(command.CommandWithPrefix),
                                command.CommandWithPrefix,
                                "Unexpected command.");
                        break;
                }
                i--;
            }

            if (string.IsNullOrWhiteSpace(line.SpokenText))
            {
                line.SpokenText = string.Empty;
            }

            line.SpeechIndex = line.RawLine.LastIndexOf(line.SpokenText, StringComparison.Ordinal);


            line.RawLine = purify(line.RawLine, false);
            line.SpokenText = purify(line.SpokenText, true);

            return line.SpokenText;
        }

        #region BoxRender

        public Image RenderPreviewBox(string line)
        {
            return conversationType == ConversationTypes.Type1 ? RenderTypeOne(line) : RenderTypeZero();
        }

        private Image RenderTypeOne(string line)
        {
            //Probably shouldn't be hard-coded
            var box = new Bitmap(400, 240);
            var tb = (Bitmap) TextBoxes[TextboxIndex].Clone();

            //Generate text image from string
            if (line.Contains("$Nu") && hasPerms)
            {
                line = line.Replace("$Nu", PlayerName);
            }

            line = line.Replace(Environment.NewLine, "\n");

            //Draw the line's text
            var text = (Bitmap) AssetGeneration.DrawString(new Bitmap(310, 50), line, 0, 22, Color.FromArgb(68, 8, 0));

            using (var g = Graphics.FromImage(tb))
            {
                g.DrawImage(text, new Point(29, 0));
            }

            //Name box
            var name = names.ContainsKey(charActive)
                ? names[charActive]
                : (charActive == "username" ? PlayerName : charActive);
            var nameLength = AssetGeneration.GetLength(name);
            var nb =
                (Bitmap)
                    AssetGeneration.DrawString(Resources.NameBox, name, Resources.NameBox.Width/2 - nameLength/2, 16,
                        Color.FromArgb(253, 234, 177)); //Center name in NameBox

            using (var g = Graphics.FromImage(box))
            {
                if (enableBackgrounds)
                {
                    g.DrawImage(BackgroundImage, new Point(0, 0));
                }

                if (charA != string.Empty)
                {
                    var ca = AssetGeneration.GetCharacterStageImage(charA, emotionA, colorA, true, PlayerGender);
                    g.DrawImage((charActive == charA) ? ca : AssetGeneration.Fade(ca),
                        new Point(-28, box.Height - ca.Height + 14));
                }

                if (charB != string.Empty)
                {
                    var cb = AssetGeneration.GetCharacterStageImage(charB, emotionB, colorB, false, PlayerGender);
                    g.DrawImage((charActive == charB) ? cb : AssetGeneration.Fade(cb),
                        new Point(box.Width - cb.Width + 28, box.Height - cb.Height + 14));
                }

                g.DrawImage(tb, new Point(10, box.Height - tb.Height + 2));

                if (charActive != string.Empty)
                {
                    g.DrawImage(nb,
                        charActive == charB
                            ? new Point(box.Width - nb.Width - 6, box.Height - tb.Height - 14)
                            : new Point(7, box.Height - tb.Height - 14));
                }

                if (LineIndex > File.MessageList[messageIndex].MessageLines.Count - 1)
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

            for (var i = 0; i <= LineIndex; i++)
            {
                var line = GetParsedCommands(File.MessageList[messageIndex].MessageLines[i]);

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
            var box = new Bitmap(400, 240);
            Bitmap topBox = new Bitmap(1, 1),
                   bottomBox = new Bitmap(1, 1);
            if (topLine != string.Empty && charA != string.Empty)
            {
                topBox = (Bitmap) (TextBoxes[TextboxIndex].Clone());
                using (var g = Graphics.FromImage(topBox))
                {
                    g.DrawImage(AssetGeneration.GetCharacterBuImage(charA, emotionA, colorA, true, PlayerGender),
                        new Point(2, 3));
                    g.DrawImage(
                        AssetGeneration.DrawString(new Bitmap(260, 50), topLine, 0, 22, Color.FromArgb(68, 8, 0)),
                        new Point(76, 0));
                }
            }

            if (bottomLine != string.Empty && charB != string.Empty)
            {
                bottomBox = (Bitmap) TextBoxes[TextboxIndex].Clone();
                using (var g = Graphics.FromImage(bottomBox))
                {
                    g.DrawImage(AssetGeneration.GetCharacterBuImage(charB, emotionB, colorB, true, PlayerGender),
                        new Point(2, 3));
                    g.DrawImage(
                        AssetGeneration.DrawString(new Bitmap(282, 50), bottomLine, 0, 22, Color.FromArgb(68, 8, 0)),
                        new Point(76, 0));
                }
            }

            using (var g = Graphics.FromImage(box))
            {
                if (LineIndex < File.MessageList[messageIndex].MessageLines.Count - 1)
                {
                    using (var g2 = Graphics.FromImage(charActive == charA ? topBox : bottomBox))
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
                    var topName = names.ContainsKey(charA) ? names[charA] : (charA == "username" ? PlayerName : charA);
                    var nameLen = AssetGeneration.GetLength(topName);
                    var topNameBox =
                        (Bitmap)
                            AssetGeneration.DrawString(Resources.NameBox, topName, Resources.NameBox.Width/2 - nameLen/2,
                                16, Color.FromArgb(253, 234, 177)); //Center name in NameBox
                    g.DrawImage(topNameBox, new Point(7, topBox.Height - (topNameBox.Height - 20)));
                }

                if (string.IsNullOrEmpty(bottomLine) || string.IsNullOrEmpty(charB))
                {
                    return box;
                }
                {
                    var bottomName = names.ContainsKey(charB)
                        ? names[charB]
                        : (charB == "username" ? PlayerName : charB);
                    var nameLen = AssetGeneration.GetLength(bottomName);
                    var bottomNameBox =
                        (Bitmap)
                            AssetGeneration.DrawString(Resources.NameBox, bottomName,
                                Resources.NameBox.Width/2 - nameLen/2, 16, Color.FromArgb(253, 234, 177));
                    g.DrawImage(bottomNameBox, new Point(7, box.Height - bottomBox.Height - 14));
                }
            }
            return box;
        }

        #endregion

        public Image RenderConversation()
        {
            foreach (var line in File.MessageList[messageIndex].MessageLines)
                line.UpdateRawWithNewDialogue();

            var images = new List<Image>();
            var index = LineIndex;
            ResetParameters();
            foreach (var t in File.MessageList[messageIndex].MessageLines)
            {
                GetParsedCommands(t);
                var parsed = t.SpokenText;
                if (!string.IsNullOrWhiteSpace(parsed))
                {
                    images.Add(RenderPreviewBox(parsed));
                }
            }
            LineIndex = index;
            var bmp = new Bitmap(images.Max(i => i.Width), images.Sum(i => i.Height));
            using (var g = Graphics.FromImage(bmp))
            {
                var h = 0;
                foreach (var img in images)
                {
                    g.DrawImage(img, new Point(0, h));
                    h += img.Height;
                }
            }

            return bmp;
        }

        public PlayerGender GetPlayerGenderFromMessageList()
        {
            const string femalePrefix = "PCF";
            return File.MessageList[MessageIndex].Prefix.Contains(femalePrefix)
                ? PlayerGender.Female
                : PlayerGender.Male;
        }
    }
}