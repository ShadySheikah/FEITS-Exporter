using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FEITS.Model
{
    /// <summary>
    /// Container for the contents of a parsed script file.
    /// </summary>
    public class ParsedFileContainer
    {
        public string FileName;
        public string FilePath;
        private string[] Header;
        public List<MessageBlock> MessageList;

        public ParsedFileContainer()
        {
            EmptyFileData();
        }

        /// <summary>
        /// Removes any previous message data
        /// and returns the file to an empty state.
        /// </summary>
        private void EmptyFileData()
        {
            FileName = FilePath = string.Empty;
            Header = null;
            MessageList = new List<MessageBlock>();
        }

        #region Loading

        /// <summary>
        /// Reads lines from specified file
        /// and parses the information
        /// </summary>
        /// <param name="filePath">Specified file name</param>
        public bool LoadFromFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                EmptyFileData();

                //Store the file path
                FilePath = filePath;

                var fileSplitByLinebreak = File.ReadAllLines(filePath);

                return LoadConversationFromString(fileSplitByLinebreak);
            }
            EmptyFileData();
            Debug.WriteLine("Opened file was empty; treating as a new object and keeping the file path.");
            return true;
        }

        public bool LoadFromString(string messageString)
        {
            EmptyFileData();

            try
            {
                if (messageString.StartsWith("MESS_"))
                {
                    var fileSplitByLinebreak = messageString.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

                    return LoadConversationFromString(fileSplitByLinebreak);
                }
                else
                {
                    var newMessage = new MessageBlock {Prefix = "Imported Message"};
                    newMessage.ParseMessage(messageString);
                    MessageList.Add(newMessage);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool LoadConversationFromString(IReadOnlyList<string> fileLines)
        {
            //Parse for header and message blocks
            if (fileLines[0].StartsWith("MESS_"))
            {
                //Find where the header ends
                //Should be after "Message Name: Message"
                var headerEndIndex = 0;
                for (var i = 0; i < fileLines.Count; i++)
                {
                    if (!fileLines[i].Contains(":")) continue;
                    if (headerEndIndex != 0)
                    {
                        headerEndIndex = i;
                        break;
                    }
                    headerEndIndex = i;
                }

                //If we found the header, add it to Header
                if (headerEndIndex != 0)
                {
                    Header = new string[headerEndIndex];
                    for (var i = 0; i < headerEndIndex; i++)
                    {
                        Header[i] = fileLines[i];
                    }

                    //Create messages from the rest of the lines
                    for (var i = headerEndIndex; i < fileLines.Count; i++)
                    {
                        //Separate the prefix from the message itself
                        if (fileLines[i].Contains(":"))
                        {
                            var newMessage = new MessageBlock();
                            var prefixIndex = fileLines[i].IndexOf(":", StringComparison.Ordinal);
                            newMessage.Prefix = fileLines[i].Substring(0, prefixIndex);

                            //Get the message by itself
                            var message = fileLines[i].Substring(prefixIndex + 2);

                            //Make sure we didn't leave any prefix stuff behind
                            if (message.StartsWith(":"))
                            {
                                message = message.Remove(0, 2);

                                return false;
                            }
                            if (char.IsWhiteSpace(message[0]))
                            {
                                message = message.Remove(0, 1);
                            }

                            //Have the message parse the rest
                            newMessage.ParseMessage(message);

                            //Add it to our list
                            MessageList.Add(newMessage);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Message lines don't appear to be formatted correctly. Please make sure each message is preceeded with a Message Name.",
                                "Error");
                            return false;
                        }
                        //TODO: messageProgress = (int)((float)i / fileLines.Length * 100);
                    }
                }
                else
                {
                    MessageBox.Show(
                        "File header doesn't appear to be formatted correctly. Please make sure the formatting is correct.",
                        "Error");
                    return false;
                }
            }
            else
            {
                MessageBox.Show(
                    "File contents don't appear to be formatted correctly. Please make sure the formatting is correct and the file is compatible with FEITS.",
                    "Error");
                return false;
            }

            return true;
        }

        #endregion

        #region Saving

        /// <summary>
        /// Compiles the message list and
        /// writes contents with header to file.
        /// </summary>
        /// <param name="filePath">File to save as</param>
        public bool SaveToFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;

            //Update file path in case different
            FilePath = filePath;

            var compiledFileText = CompileFileText();

            if (string.IsNullOrEmpty(compiledFileText)) return false;
            File.WriteAllText(filePath, compiledFileText, new UTF8Encoding(true));
            return true;
        }

        public string CompileFileText()
        {
            //Start compiling a string to make up the new file
            var newFileText = Header.Aggregate(string.Empty,
                (current, str) => current + str + Environment.NewLine);

            return MessageList.Select(msg => msg.CompileMessage())
                              .Aggregate(newFileText,
                                  (current, s) => current + s + Environment.NewLine);
        }

        #endregion
    }
}