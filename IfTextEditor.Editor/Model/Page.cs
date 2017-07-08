using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IfTextEditor.Editor.Model
{
    public partial class FileContainer
    {
        public partial class Message
        {
            public class Page : IEnumerable<Command>
            {
                //Page fields
                public List<Command> Commands { get; } = new List<Command>();
                public Dictionary<int, string> Comments { get; set; } = new Dictionary<int, string>();
                public List<string> SpokenLine { get; set; } = new List<string>();

                internal Page()
                {
                    
                }

                internal Page(string speech)
                {
                    Commands = new List<Command>();
                    Comments = new Dictionary<int, string>();
                    SpokenLine = new List<string>();
                    string newText = speech;

                    var starterCmdFound = false;

                    //Parse for commands until text begins
                    for (int i = 0; i < newText.Length; i++)
                    {
                        bool commandPresent = newText[i] == '$';

                        //If $Nu is next, we've found all the commands.
                        //Loop once more for line ending
                        if (commandPresent && (newText.Substring(i).StartsWith("$Nu") || newText.Substring(i).StartsWith("$a0") || newText.Substring(i).StartsWith("$G")))
                        {
                            starterCmdFound = true;
                            continue;
                        }

                        //Find a command and strip it from the text
                        string parsedText;
                        var newCmd = new Command(newText.Substring(i), out parsedText);
                        newText = parsedText;

                        if (starterCmdFound)
                            newText = '$' + parsedText;

                        //If nothing was found, don't keep it
                        if (newCmd.Type != CommandType.Empty)
                            Commands.Add(newCmd);

                        //One more time for the line ender
                        if (!commandPresent)
                            break;

                        i--;
                    }

                    string[] tempText = newText.Split(new[] {"\\n"}, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string str in tempText)
                        SpokenLine.Add(str.Trim());

                    if (SpokenLine.Count < 1 && Comments.Count < 1)
                    {
                        Comments.Add(0, "//This page was imported without speech.");
                        Comments.Add(1, "//There may be code attached to it.");
                    }
                }

                public string ToString(bool stripped = false)
                {
                    if (stripped)
                    {
                        var strippedComp = new StringBuilder();

                        string curSpeaker = string.Empty;
                        foreach (Command cmd in Commands)
                        {
                            if (cmd.Type == CommandType.Speaker)
                                curSpeaker = cmd.Parameters[0];
                        }

                        if(curSpeaker != string.Empty)
                            strippedComp.AppendLine(curSpeaker /*CharacterData.GetLocalizedName(curSpeaker)*/ + ":");

                        foreach (string line in SpokenLine)
                            strippedComp.AppendLine('\t' + line);

                        return strippedComp.ToString();
                    }

                    //This will be our compiled string
                    var comp = new StringBuilder();

                    //Commands
                    comp.Append(Commands.Aggregate(string.Empty, (current, command) => current + command.ToString()));

                    //Lines
                    return comp + string.Join("\\n", SpokenLine) + GetLineEnd();
                }

                private string GetLineEnd()
                {
                    foreach (Command c in Commands)
                    {
                        if (c.Type == CommandType.PageEnd)
                            return c.Symbol;
                    }

                    return string.Empty;
                }

                public IEnumerator<Command> GetEnumerator()
                {
                    return ((IEnumerable<Command>)Commands).GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return ((IEnumerable<Command>)Commands).GetEnumerator();
                }
            }
        }
    }
}
