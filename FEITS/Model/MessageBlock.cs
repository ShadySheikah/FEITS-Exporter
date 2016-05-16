﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FEITS.Model
{
    /// <summary>
    /// Stores a line of dialogue with
    /// its prefix separated from the messages.
    /// </summary>
    public class MessageBlock
    {
        private string prefix = string.Empty;
        public string Prefix { get { return prefix; } set { prefix = value; } }
        private List<MessageLine> messageLines = new List<MessageLine>();
        public List<MessageLine> MessageLines { get { return messageLines; } set { messageLines = value; } }

        #region Parsing
        /// <summary>
        /// Returns a list of parsed message lines.
        /// </summary>
        /// <param name="message">The message to parse</param>
        public void ParseMessage(string message)
        {
            message = message.Replace("\\n", "\n").Replace("$k\n", "$k\\n");

            //Split the lines based on the line-end markers
            var delimiters = new List<string> { "$k$p", "$k\\n" };
            string pattern = "(?<=" + string.Join("|", delimiters.Select(d => Regex.Escape(d)).ToArray()) + ")";
            string[] splits = Regex.Split(message, pattern);

            List<string> lines = new List<string>();
            lines.AddRange(splits.Where(s => (!string.IsNullOrWhiteSpace(Parse(s)) && !string.IsNullOrEmpty(Parse(s)))));

            foreach(string str in lines)
            {
                MessageLine newLine = new MessageLine();
                newLine.RawLine = str;
                messageLines.Add(newLine);
            }
        }

        private string Parse(string line)
        {
            for(int i = 0; i < line.Length; i++)
            {
                if(line[i] == '$')
                {
                    Tuple<string, Command> res = ParseCommand(line, i);
                    line = res.Item1;
                    switch(res.Item2.cmd)
                    {
                        case "$E":
                        case "$Ws":
                        case "$Wm":
                        case "$Wd":
                        case "$a":
                        case "$t0":
                        case "$t1":
                        case "$Nu":
                            i += 2;
                            break;
                        case "$Wa":
                        case "$Wc":
                        default:
                            break;
                    }

                    i--;
                }
            }

            line = line.Replace("\\n", "\n").Replace("$k\n", "$k\\n");
            return line;
        }

        public static Tuple<string, Command> ParseCommand(string line, int offset)
        {
            string trunc = line.Substring(offset);
            string[] NoParams = { "$Wa", "$Wc", "$a", "$Nu", "$N0", "$N1", "$k\\n", "$k", "$t0", "$t1", "$p" };
            string[] SingleParams = { "$E", "$Sbs", "$Svp", "$Sre", "$Fw", "$Ws", "$VF", "$Ssp", "$Fo", "$VNMPID", "$Fi", "$b", "$Wd", "$w", "$l" };
            string[] DoubleParams = { "$Wm", "$Sbv", "$Sbp", "$Sls", "$Slp" };
            Command newCmd = new Command();

            foreach(string delim in NoParams)
            {
                if(trunc.StartsWith(delim))
                {
                    newCmd.cmd = delim;
                    newCmd.numParams = 0;
                    newCmd.Params = new string[newCmd.numParams];
                    line = line.Substring(0, offset) + line.Substring(offset + delim.Length);
                    break;
                }
            }

            foreach(string delim in SingleParams)
            {
                if(trunc.StartsWith(delim))
                {
                    Console.WriteLine(delim);
                    Console.WriteLine(trunc);
                    newCmd.cmd = delim;
                    newCmd.numParams = 1;
                    newCmd.Params = new string[newCmd.numParams];
                    int index = line.IndexOf("|", offset);
                    newCmd.Params[0] = line.Substring(offset + delim.Length, index - (offset + delim.Length));
                    line = line.Substring(0, offset) + line.Substring(index + 1);
                }
            }

            foreach(string delim in DoubleParams)
            {
                if(trunc.StartsWith(delim))
                {
                    newCmd.cmd = delim;
                    newCmd.numParams = 2;
                    newCmd.Params = new string[newCmd.numParams];
                    int index = line.IndexOf("|", offset);
                    int index2 = line.IndexOf("|", index + 1);

                    if(delim == "$Wm")
                    {
                        newCmd.Params[0] = line.Substring(offset + delim.Length, index - (offset + delim.Length));
                        newCmd.Params[1] = line.Substring(index + 1, 1);
                        line = line.Substring(0, offset) + line.Substring(index + 2);
                    }
                    else
                    {
                        newCmd.Params[0] = line.Substring(offset + delim.Length, index - (offset + delim.Length));
                        newCmd.Params[1] = line.Substring(index + 1, index2 - (index + 1));
                        line = line.Substring(0, offset) + line.Substring(index2 + 1);
                    }
                }
            }

            Tuple<string, Command> ret = new Tuple<string, Command>(line, newCmd);
            return ret;
        }
        #endregion

        public string CompileMessage()
        {
            //Recompile the message with the prefix and all lines
            string combinedLines = (prefix != string.Empty ? prefix + ": " : string.Empty);
            foreach(MessageLine line in messageLines)
            {
                line.UpdateRawWithNewDialogue();
                combinedLines += line.RawLine;
            }

            combinedLines = combinedLines.Replace("\n", "\\n");
            return combinedLines;
        }
    }
}
