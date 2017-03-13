using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace IfTextEditor.Editor.Model
{
    public class ConversationModel
    {
        #region Variables
        //File Container
        public FileContainer FileCont { get; set; } = new FileContainer();
        private int messageIndex;
        public int MessageIndex
        {
            get { return messageIndex; }
            set
            {
                messageIndex = value;
                PageIndex = 0;
                ResetCommands();
            }
        }
        public int PageIndex { get; set; }

        //Conversation properties
        public string PlayerName { get; set; } = "Kamui";
        private Gender playerGender;
        public Gender PlayerGender
        {
            get { return playerGender; }
            set
            {
                CharacterData.UpdatePlayerData(value);
                playerGender = value;
            }
        }

        private bool hasPerms;
        private string activeChar = string.Empty;
        private string charA = string.Empty;
        private string charB = string.Empty;
        private static readonly Emote[] DefaultEmotion = {Emote.Normal};
        private Emote[] charAEmotions = DefaultEmotion;
        private Emote[] charBEmotions = DefaultEmotion;
        private enum ConversationFormat { Type1, Type0 }
        private ConversationFormat format;

        public Image BackgroundImage { get; set; } = Resources.Properties.Resources.SupportBG;
        private bool backgroundEnabled = true;
        public bool BackgroundEnabled
        {
            get { return backgroundEnabled; }
            set
            {
                backgroundEnabled = value;
                BackgroundImage = Resources.Properties.Resources.SupportBG;
            }
        }

        public int TextboxIndex { get; set; }
        private static readonly Image[] Textboxes =
        {
            Resources.Properties.Resources.TextBox,
            Resources.Properties.Resources.TextBox_Nohr,
            Resources.Properties.Resources.TextBox_Hoshido
        };
        #endregion

        private void ResetCommands()
        {
            hasPerms = false;
            activeChar = charA = charB = string.Empty;
            charAEmotions = charBEmotions = DefaultEmotion;
            format = ConversationFormat.Type1;
        }

        #region Open/Close

        public bool LoadFromFile(string path)
        {
            //if (!Directory.Exists(path))
            //    return false;

            FileCont = new FileContainer {FilePath = path};

            string[] splitFile = File.ReadAllLines(path, Encoding.UTF8);
            return FileCont.PopulateContainer(splitFile);
        }

        public bool LoadFromString(string content)
        {
            try
            {
                if (content.StartsWith("MESS_ARCHIVE_"))
                {
                    string[] splitContent = content.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                    return FileCont.PopulateContainer(splitContent);
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public bool SaveToFile(string path)
        {
            if (path == string.Empty)
                return false;

            FileCont.FilePath = path;

            string compiledFileText = FileCont.CompileFileText();
            if (compiledFileText == string.Empty)
                return false;

            File.WriteAllText(FileCont.FilePath, compiledFileText);
            return true;
        }
        #endregion

        #region Commands

        public void IterateCommandsToIndex(int pageIndex)
        {
            ResetCommands();

            for (int i = 0; i < pageIndex; i++)
            {
                UpdatePageCommands(i);
            }
        }

        public void UpdatePageCommands(int pageIndex)
        {
            UpdateCommands(FileCont.Messages[messageIndex].Pages[pageIndex].Commands);
        }

        private void UpdateCommands(IEnumerable<Command> commands)
        {
            foreach (Command c in commands)
            {
                switch (c.Type)
                {
                    case CommandType.Emotion:
                        string[] emotions = c.Parameters[0].Split(',');
                        var emotes = new Emote[emotions.Length];
                        for (int i = 0; i < emotes.Length; i++)
                            emotes[i] = CharacterData.EmoteLookup.FirstOrDefault(x => x.Value == emotions[i]).Key;

                        if (activeChar != string.Empty && activeChar == charB)
                            charBEmotions = emotes;
                        else
                            charAEmotions = emotes;
                        break;
                    case CommandType.Speaker:
                        activeChar = c.Parameters[0];
                        break;
                    case CommandType.Portrait:
                        int newPosition = Convert.ToInt32(c.Parameters[1]);

                        if (format == ConversationFormat.Type1)
                        {
                            if (newPosition == 3)
                            {
                                charA = c.Parameters[0];
                                charAEmotions = DefaultEmotion;
                            }
                            else if (newPosition == 7)
                            {
                                charB = c.Parameters[0];
                                charBEmotions = DefaultEmotion;
                            }

                            break;
                        }

                        if (newPosition == 0 || newPosition == 2)
                        {
                            charA = c.Parameters[0];
                            charAEmotions = DefaultEmotion;
                        }
                        else if (newPosition == 6)
                        {
                            charB = c.Parameters[0];
                            charBEmotions = DefaultEmotion;
                        }

                        break;
                    case CommandType.CharExit:
                        if (activeChar == charB)
                        {
                            activeChar = charA;
                            charB = string.Empty;
                            break;
                        }

                        charA = string.Empty;
                        break;
                    case CommandType.NamePerms:
                        hasPerms = true;
                        break;
                    case CommandType.Format:
                        format = c.Symbol == "$t0" ? ConversationFormat.Type0 : ConversationFormat.Type1;
                        break;
                }
            }
        }

        private string ParseInlineCommands(string speech)
        {
            string newSpeech = hasPerms ? speech.Replace("$Nu", PlayerName) : speech.Replace("$Nu", "{Needs Perms}");
            var cmds = new List<Command>();
            while (newSpeech.Contains('$'))
            {
                int index = newSpeech.IndexOf("$", StringComparison.Ordinal);

                //Handle gender-specific phrases
                if (newSpeech.Substring(index).StartsWith("$G"))
                {
                    int end = newSpeech.Substring(index).IndexOf('|');
                    if (end > 0)
                    {
                        string[] parameters = newSpeech.Substring(index + 2, end - 2).Split(',');

                        foreach (string str in parameters)
                            Debug.WriteLine(str);

                        newSpeech = newSpeech.Substring(0, index) + (playerGender == Gender.Male ? parameters[0] : parameters[1]) + newSpeech.Substring(index + end + 1);

                        continue;
                    }
                }

                string substring;
                var cmd = new Command(newSpeech.Substring(index), out substring);
                if (cmd.Type == CommandType.UNKNOWN_TYPE && newSpeech.Count(f => f == '$') <= 1)
                    break;

                newSpeech = newSpeech.Substring(0, index) + substring;
                cmds.Add(cmd);
            }

            UpdateCommands(cmds);
            return newSpeech;
        }
        #endregion

        #region Rendering

        public Image RenderPreview(int page, PreviewFormat pFormat)
        {
            if (format == ConversationFormat.Type0)
                pFormat = PreviewFormat.TopBottom;

            if(FileCont.Messages.Count > 0 && page < FileCont.Messages[messageIndex].Pages.Count)
                return RenderSpecifiedPreview(page, pFormat);

            return null;
        }

        private Bitmap RenderSpecifiedPreview(int index, PreviewFormat pFormat)
        {
            //Grab and set up the line text
            string topLine, bottomLine;
            string text = topLine = bottomLine = string.Empty;

            if (pFormat == PreviewFormat.TopBottom)
                ResetCommands();

            for (int i = pFormat == PreviewFormat.TopBottom ? 0 : index; i <= index; i++)
            {
                UpdatePageCommands(i);

                List<string> lines = FileCont.Messages[messageIndex].Pages[i].SpokenText.Select(entry => entry.Value).ToList();
                text = ParseInlineCommands(string.Join(Environment.NewLine, lines));

                if (pFormat == PreviewFormat.TopBottom && activeChar == charA)
                    topLine = text;
                else if (pFormat == PreviewFormat.TopBottom)
                    bottomLine = text;
            }

            //Halfbox preview
            if (pFormat == PreviewFormat.HalfBox)
            {
                Bitmap[] hb = {Resources.Properties.Resources.HalfBox, Resources.Properties.Resources.HalfBox_Nohr, Resources.Properties.Resources.HalfBox_Hoshido};
                var halfbox = (Bitmap) hb[TextboxIndex].Clone();

                using (Graphics g = Graphics.FromImage(halfbox))
                {
                    var halfText = (Bitmap) PreviewFont.DrawString(new Bitmap(165, 50), text, Color.FromArgb(68, 8, 0), 0, 22);
                    g.DrawImage(halfText, new Point(10, 0));
                    g.DrawImage(Resources.Properties.Resources.KeyPress, new Point(168, 32));
                }

                return halfbox;
            }

            //Preview setup
            var baseImage = new Bitmap(400, 240);

            using (Graphics g = Graphics.FromImage(baseImage))
            {
                //Draw a background
                if (pFormat == PreviewFormat.Skinship)
                    g.DrawImage(Resources.Properties.Resources.AmieBG, new Point(0, 0));
                else if (backgroundEnabled)
                    g.DrawImage(BackgroundImage, new Point(0, 0));

                //Draw characters
                if (pFormat == PreviewFormat.Normal && charA != string.Empty)
                {
                    Bitmap imageA = CharacterData.DrawCharacterImage(charA, charAEmotions, true, PlayerGender);
                    g.DrawImage(activeChar == charA ? imageA : CharacterData.ColorizeImage(imageA, Color.Black, SkiaSharp.SKBlendMode.Multiply, 128), new Point(-28, baseImage.Height - imageA.Height + 14));
                }
                if (pFormat == PreviewFormat.Normal && charB != string.Empty)
                {
                    Bitmap imageB = CharacterData.DrawCharacterImage(charB, charBEmotions, false, PlayerGender);
                    g.DrawImage(activeChar == charB ? imageB : CharacterData.ColorizeImage(imageB, Color.Black, SkiaSharp.SKBlendMode.Multiply, 128), new Point(baseImage.Width - imageB.Width + 28, baseImage.Height - imageB.Height + 14));
                }

                //Draw textbox
                switch (pFormat)
                {
                    case PreviewFormat.Normal:
                        //Text box
                        var normTextbox = (Bitmap) Textboxes[TextboxIndex].Clone();    //Grab the textbox
                        var textBounds = new Bitmap(310, 50);       //Text bounding box
                        textBounds = (Bitmap) PreviewFont.DrawString(textBounds, text, Color.FromArgb(68, 8, 0), 0, 7);   //Draw string to bounding box
                        //Draw text to textbox
                        using (Graphics gr = Graphics.FromImage(normTextbox))
                            gr.DrawImage(textBounds, new Point(29, 0));
                        //Textbox to base image
                        g.DrawImage(normTextbox, new Point(10, baseImage.Height - normTextbox.Height + 2));

                        //Name box
                        if (activeChar != string.Empty)
                        {
                            Bitmap namebox = DrawNameBox(activeChar);
                            g.DrawImage(namebox, activeChar == charB ? new Point(baseImage.Width - namebox.Width - 6, baseImage.Height - normTextbox.Height - 14) : new Point(7, baseImage.Height - normTextbox.Height - 14));
                        }

                        //Arrow
                        if (index < FileCont.Messages[messageIndex].Pages.Count - 1)
                            g.DrawImage(Resources.Properties.Resources.KeyPress, new Point(baseImage.Width - 33, baseImage.Height - normTextbox.Height + 32));
                        break;
                    case PreviewFormat.TopBottom:
                        //Top textbox
                        if (topLine != string.Empty && charA != string.Empty)
                        {
                            var topTextbox = (Bitmap) Textboxes[TextboxIndex].Clone();
                            using (Graphics gr = Graphics.FromImage(topTextbox))
                            {
                                //Character and text
                                gr.DrawImage(CharacterData.DrawCharacterCloseUpImage(charA, charAEmotions, playerGender), new Point(2, 3));
                                gr.DrawImage(PreviewFont.DrawString(new Bitmap(282, 50), topLine, Color.FromArgb(68, 8, 0), 0, 7), new Point(76, 2));

                                //Arrow
                                if (index < FileCont.Messages[messageIndex].Pages.Count - 1 && activeChar == charA)
                                    gr.DrawImage(Resources.Properties.Resources.KeyPress, new Point(topTextbox.Width - 22, 32));
                            }

                            //Add to base
                            g.DrawImage(topTextbox, new Point(10, -2));

                            //Name box
                            Bitmap topNamebox = DrawNameBox(charA);
                            g.DrawImage(topNamebox, new Point(7, topTextbox.Height - (topNamebox.Height - 15)));
                        }

                        //Bottom textbox
                        if (bottomLine != string.Empty && charB != string.Empty)
                        {
                            var botTextbox = (Bitmap) Textboxes[TextboxIndex].Clone();
                            using (Graphics gr = Graphics.FromImage(botTextbox))
                            {
                                //Character and text
                                gr.DrawImage(CharacterData.DrawCharacterCloseUpImage(charB, charBEmotions, playerGender), new Point(2,3));
                                gr.DrawImage(PreviewFont.DrawString(new Bitmap(282, 50), bottomLine, Color.FromArgb(68, 8, 0), 0, 7), new Point(76, 2));

                                //Arrow
                                if (index < FileCont.Messages[messageIndex].Pages.Count - 1 && activeChar == charB)
                                    gr.DrawImage(Resources.Properties.Resources.KeyPress, new Point(botTextbox.Width - 22, 32));
                            }

                            //Add to base
                            g.DrawImage(botTextbox, new Point(10, baseImage.Height - botTextbox.Height - 2));

                            //Name box on top
                            Bitmap botNameBox = DrawNameBox(charB);
                            g.DrawImage(botNameBox, new Point(7, baseImage.Height - botTextbox.Height - 18));
                        }
                        break;
                    case PreviewFormat.Skinship:
                        //Textbox
                        var amieTextbox = (Bitmap) Resources.Properties.Resources.AmieTextbox.Clone();
                        string[] lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                        var lineWidths = new int[lines.Length];
                        for(int i = 0; i < lineWidths.Length; i++)
                            lineWidths[i] = PreviewFont.GetStringWidth(lines[i]);

                        var amieTextImage = new Bitmap(lineWidths.Max(), lines.Length * 25);
                        for(int i = 0; i < lines.Length; i++)
                            amieTextImage = (Bitmap) PreviewFont.DrawString(amieTextImage, lines[i], Color.FromArgb(68, 8, 0), amieTextImage.Width / 2 - lineWidths[i] / 2, amieTextImage.Height / lines.Length - 25 / 2);

                        using (Graphics gr = Graphics.FromImage(amieTextbox))
                            gr.DrawImage(amieTextImage, new Point(amieTextbox.Width / 2 - amieTextImage.Width / 2, amieTextbox.Height / 2 - amieTextImage.Height / 2));

                        //TODO: Figure out where the textbox should be
                        break;
                }
            }

                return baseImage;
        }

        private Bitmap DrawNameBox(string charName)
        {
            //Get character name and width
            string name = charName == "username" ? PlayerName : CharacterData.GetLocalizedName(activeChar) ?? activeChar;
            int nameWidth = PreviewFont.GetStringWidth(name);

            //Text to namebox
            return (Bitmap) PreviewFont.DrawString(Resources.Properties.Resources.NameBox, name, Color.FromArgb(253, 234, 177), Resources.Properties.Resources.NameBox.Width / 2 - nameWidth / 2, 1);
        }
#endregion
    }

    public enum PreviewFormat
    {
        Normal,
        HalfBox,
        Skinship,
        TopBottom
    }
}
