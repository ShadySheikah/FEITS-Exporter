using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfTextEditor.Editor.Model
{
    public partial class FileContainer
    {
        public partial class Message
        {
            public class Page
            {
                //Page fields
                public List<Command> Commands { get; }
                public Dictionary<int, string> ExtraComment { get; set; }
                public Dictionary<int, string> SpokenText { get; set; }

                public Page()
                {
                    
                }

                public Page(string speech)
                {
                    Commands = new List<Command>();
                    ExtraComment = new Dictionary<int, string>();
                    SpokenText = new Dictionary<int, string>();
                    string newText = speech;

                    //Parse for commands until text begins
                    for (int i = 0; i < newText.Length; i++)
                    {
                        bool commandPresent = newText[i] == '$';

                        //If $Nu is next, we've found all the commands.
                        //Loop once more for line ending
                        if (commandPresent && (newText.Substring(i).StartsWith("$Nu") || newText.Substring(i).StartsWith("$a0") || newText.Substring(i).StartsWith("$G")))
                            continue;

                        //Find a command and strip it from the text
                        string parsedText;
                        var newCmd = new Command(newText.Substring(i), out parsedText);
                        newText = parsedText;

                        //If nothing was found, don't keep it
                        if (newCmd.Type != CommandType.Empty)
                            Commands.Add(newCmd);

                        //One more time for the line ender
                        if (!commandPresent)
                            break;

                        i--;
                    }

                    string[] text = newText.Split(new[] {"\\n"}, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < text.Length; i++)
                    {
                        SpokenText.Add(i, text[i]);
                    }

                    if (SpokenText.Count < 1 && ExtraComment.Count < 1)
                    {
                        ExtraComment.Add(0, "//This page was imported without speech.");
                        ExtraComment.Add(1, "//There may be code attached to it.");
                    }
                }

                public string GetCompiledPage()
                {
                    //This will be our compiled string
                    string comp = string.Empty;
                    
                    //Commands
                    comp += Commands.Aggregate(string.Empty, (current, c) => current + c.CompileCommand());

                    //Lines
                    List<string> lines = SpokenText.Select(entry => entry.Value).ToList();
                    return comp + string.Join("\\n", lines) + GetLineEnd();
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
            }
        }
    }
}
