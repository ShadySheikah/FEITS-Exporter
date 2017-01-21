using System;

namespace FEITS.Model
{
    /// <summary>
    /// Contains a single line of dialogue and its commands.
    /// </summary>
    //TODO(Robin): Immutable type?
    public class MessageLine
    {
        public string RawLine { get; set; } = string.Empty;
        public string SpokenText { get; set; } = string.Empty;
        public int SpeechIndex { private get; set; } = -1;

        public ConversationTypes ConversationType = ConversationTypes.Type1;

        public void UpdateRawWithNewDialogue()
        {
            if (SpokenText != string.Empty)
            {
                SpokenText = SpokenText.Replace('’', '\'').Replace('~', '～').Replace("  ", " ");

                //Find where the end of the old dialogue text should be
                int lineIndex;
                if (RawLine.Contains("$Nu"))
                {
                    lineIndex = RawLine.IndexOf("$",
                        RawLine.IndexOf("$Nu", StringComparison.Ordinal) + 2,
                        StringComparison.Ordinal);
                }
                else
                {
                    lineIndex = RawLine.IndexOf("$", SpeechIndex, StringComparison.Ordinal);
                }

                var oldDialogue = lineIndex > SpeechIndex
                    ? RawLine.Substring(SpeechIndex, lineIndex - SpeechIndex)
                    : RawLine.Substring(SpeechIndex);

                RawLine = RawLine.Substring(0, SpeechIndex) + SpokenText +
                          RawLine.Substring(SpeechIndex + oldDialogue.Length);
            }

            RawLine = RawLine.Replace(Environment.NewLine, "\\n");
        }
    }
}