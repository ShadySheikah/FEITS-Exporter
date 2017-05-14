using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IfTextEditor.Editor.Model
{
    public class Command : IEnumerable<string>
    {
        public string Symbol { get; } = string.Empty;
        public string[] Parameters { get; }
        public CommandType Type { get; }

        internal Command(string symbol, string[] parameters, CommandType type)
        {
            if (symbol == string.Empty)
            {
                switch (type)
                {
                    case CommandType.Portrait:
                        Symbol = "$Wm";
                        break;
                    case CommandType.Speaker:
                        Symbol = "$Ws";
                        break;
                    case CommandType.Emotion:
                        Symbol = "$E";
                        break;
                    case CommandType.CharExit:
                        Symbol = "$Wd";
                        break;
                    case CommandType.NamePerms:
                        Symbol = "$a";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            else
            {
                Symbol = symbol;
            }

            Parameters = parameters;
            Type = type;
        }

        internal Command(string content, out string updatedText)
        {
            if (content.StartsWith("$"))
            {
                var knownCommands = new Dictionary<string, int>
                {
                    {"$Wa", 0}, {"$a", 0}, {"$t0", 0}, {"$t1", 0}, {"$Wd", 0}, {"$Wc", 0}, {"$N0", 0}, {"$N1", 0}, {"$p", 0}, {"$Wv", 0}, {"$t2", 0}, {"$t3", 0},
                    {"$E", 1}, {"$Ws", 1}, {"$Sbs", 1}, {"$Svp", 1}, {"$Sre", 1}, {"$Fw", 1}, {"$VF", 1}, {"$Ssp", 1}, {"$Fo", 1}, {"$VNMPID", 1}, {"$Fi", 1}, {"$b", 1}, {"$w", 1}, {"$l", 1}, {"$Tc", 1}, {"$Td", 1},
                    {"$Wm", 2}, {"$Sbv", 2}, {"$Sbp", 2}, {"$Sls", 2}, {"$Slp", 2}, {"$Srp", 2}
                };

                //Iterate through dictionary to find symbol and param count
                foreach (string key in knownCommands.Keys)
                {
                    if (!content.StartsWith(key, StringComparison.Ordinal))
                        continue;

                    Symbol = key;
                    Parameters = new string[knownCommands[key]];
                    break;
                }

                //If no key matched, set empty params to avoid errors
                if (Symbol == string.Empty)
                {
                    Debug.WriteLine("UNKNOWN COMMAND: " + content.Substring(0, 10));
                    Parameters = new string[0];
                }

                #region Set parameters

                switch (Parameters.Length)
                {
                    case 1:
                        int index = content.IndexOf('|');
                        Parameters[0] = content.Substring(Symbol.Length, index - Symbol.Length);
                        updatedText = content.Substring(index + 1);
                        break;
                    case 2:
                        index = content.IndexOf('|');
                        int index2 = content.IndexOf('|', index + 1);
                        Parameters[0] = content.Substring(Symbol.Length, index - Symbol.Length);

                        if (Symbol == "$Wm")
                        {
                            Parameters[1] = content.Substring(index + 1, 1);
                            updatedText = content.Substring(index + 2);
                        }
                        else
                        {
                            Parameters[1] = content.Substring(index + 1, index2 - (index + 1));
                            updatedText = content.Substring(index2 + 1);
                        }
                        break;
                    default:
                        updatedText = content.Substring(Symbol.Length);
                        break;
                }

                #endregion

                #region Set command type for easy lookup
                switch (Symbol)
                {
                    case "$E":
                        Type = CommandType.Emotion;
                        break;
                    case "$Ws":
                        Type = CommandType.Speaker;
                        break;
                    case "$Wm":
                        Type = CommandType.Portrait;
                        break;
                    case "$t0":
                        Type = CommandType.Format;
                        break;
                    case "$t1":
                        Type = CommandType.Format;
                        break;
                    case "$Wd":
                        Type = CommandType.CharExit;
                        break;
                    case "$a":
                        Type = CommandType.NamePerms;
                        break;
                    case "":
                        Debug.WriteLine("NEW COMMAND: " + Symbol);
                        Type = CommandType.UNKNOWN_TYPE;
                        break;
                    default:
                        Type = CommandType.Other;
                        break;
                }
                #endregion
            }
            else
            {
                string[] pageEnds = { "$k$p", "$k\\n", "$k" };

                foreach (string end in pageEnds)
                {
                    if (!content.Contains(end))
                        continue;

                    Symbol = end;
                    Parameters = new string[0];
                    Type = CommandType.PageEnd;
                    updatedText = content.Substring(0, content.Length - end.Length);
                    return;
                }

                Type = CommandType.Empty;
                updatedText = content;
            }
        }

        public override string ToString()
        {
            if (Symbol == string.Empty || Type == CommandType.PageEnd)
                return string.Empty;

            var cmdLine = new StringBuilder();
            cmdLine.Append(Symbol);

            if (Parameters.Length > 0)
            {
                for (int i = 0; i < Parameters.Length; i++)
                {
                    if (Symbol == "$Wm" && i == 1)
                        cmdLine.Append(Parameters[i]);
                    else
                        cmdLine.Append(Parameters[i] + '|');
                }
            }

            return cmdLine.ToString();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)Parameters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)Parameters).GetEnumerator();
        }
    }

    public enum CommandType
    {
        // ReSharper disable once InconsistentNaming
        UNKNOWN_TYPE,
        Other,
        Format,
        Portrait,
        Speaker,
        Emotion,
        CharExit,
        NamePerms,
        PageEnd,
        Empty
    }
}
