using System.Collections.Generic;
using System.Linq;
using ScintillaNET;

namespace IfTextEditor.Editor.View.Lexers
{
    internal class YamlLexer
    {
        public const int StyleDefault = 0;
        public const int StyleKey = 1;
        public const int StyleValue = 2;
        public const int StyleString = 3;
        public const int StyleLiteral = 4;
        public const int StyleComment = 5;

        private enum State
        {
            Unknown = 0,
            Key,
            Value,
            String,
            Literal,
            Comment
        }

        public void Style(Scintilla scintilla, int startPos, int endPos)
        {
            int length = 0;
            State state = State.Unknown,
                prevState = State.Unknown;
            bool escapeSet = false,
                inQuotes = false;

        //To line start
        int line = scintilla.LineFromPosition(startPos);
            startPos = scintilla.Lines[line].Position;

            //Start styling
            scintilla.StartStyling(startPos);
            while (startPos < endPos)
            {
                var c = (char) scintilla.GetCharAt(startPos);

                bool wasInQuotes = inQuotes;
                if ((c == '\'' || c == '"') && !escapeSet)
                    inQuotes = !inQuotes;

                REPROCESS:

                switch (state)
                {
                    case State.Unknown:
                        if (c == ':' && !inQuotes)
                        {
                            length++;
                            scintilla.SetStyling(length, StyleKey);
                            length = 1;
                            state = State.Value;
                        }
                        else if (c == '-')
                        {
                            scintilla.SetStyling(length, StyleDefault);
                            length = 1;
                            state = State.Key;
                        }
                        else if (c == '#')
                        {
                            scintilla.SetStyling(length, StyleDefault);
                            length = 1;
                            state = State.Comment;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                    case State.Key:
                        if (c == ':')
                        {
                            length++;
                            scintilla.SetStyling(length, StyleKey);
                            length = 1;
                            state = State.Value;
                        }
                        else if (inQuotes && !wasInQuotes)
                        {
                            scintilla.SetStyling(length - 1, StyleKey);
                            length = 0;
                            prevState = state;
                            state = State.String;
                            goto REPROCESS;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                    case State.Value:
                        if (c == '-' && escapeSet)
                        {
                            scintilla.SetStyling(length, StyleDefault);
                            length = 0;
                            state = State.Literal;
                        }
                        else if (c == '|' &&!inQuotes)
                        {
                            escapeSet = true;
                            length++;
                            startPos++;
                            continue;
                        }
                        else if (inQuotes && !wasInQuotes)
                        {
                            scintilla.SetStyling(length - 1, StyleValue);
                            length = 0;
                            prevState = state;
                            state = State.String;
                            goto REPROCESS;
                        }
                        else if (c == '\r')
                        {
                            scintilla.SetStyling(length, StyleValue);
                            length = 0;
                            state = State.Unknown;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                    case State.String:
                        if (!inQuotes && wasInQuotes)
                        {
                            length++;
                            scintilla.SetStyling(length, StyleString);
                            length = 1;
                            state = prevState;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                    case State.Literal:
                        if (escapeSet)
                        {
                            if (c == '-' || c == '#')
                            {
                                scintilla.SetStyling(length, StyleLiteral);
                                length = 0;
                                state = State.Unknown;
                                goto REPROCESS;
                            }
                            if (c == ' ' || c == '\n')
                            {
                                length++;
                                startPos++;
                                continue;
                            }

                            escapeSet = false;
                            length++;
                        }
                        else if (c == '\r')
                        {
                            escapeSet = true;
                            length++;
                            startPos++;
                            continue;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                    case State.Comment:
                        if (c == '\r')
                        {
                            scintilla.SetStyling(length, StyleComment);
                            length = 0;
                            state = State.Unknown;
                        }
                        else
                        {
                            length++;
                        }
                        break;
                }

                if (c == '\\' && !escapeSet)
                    escapeSet = true;
                else
                    escapeSet = false;

                    startPos++;
            }
        }
    }
}
