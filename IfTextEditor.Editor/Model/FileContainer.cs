using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IfTextEditor.Editor.Model
{
    public partial class FileContainer : IEnumerable<FileContainer.Message>
    {
        //Fields
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public List<Message> Messages { get; private set; }

        public FileContainer()
        {
            FileName = FilePath = string.Empty;
            Messages = new List<Message>();
        }

        public bool PopulateContainer(string[] fileLines)
        {
            //Find and skip the header
            if (!fileLines[0].StartsWith("MESS_ARCHIVE_"))
            {
                for (int i = 0; i < fileLines.Length; i++)
                {
                    var newMessage = new Message {MessageName = "Imported Message" + (i > 0 ? i.ToString() : "")};
                    newMessage.ParseMessage(fileLines[i]);
                    Messages.Add(newMessage);
                }
                return true;
            }

            FileName = fileLines[0].Substring(13);

            int offset = -1;
            for (int i = 0; i < fileLines.Length; i++)
            {
                //Contents begin after "Message Name: Message"
                if (fileLines[i].Contains(':'))
                {
                    if (offset > -1)
                    {
                        offset = i;
                        break;
                    }

                    offset = i;
                }
            }

            for (int i = offset; i < fileLines.Length; i++)
            {
                //Set the message name/prefix
                if (fileLines[i].Contains(':'))
                {
                    var newMessage = new Message();
                    int prefixIndex = fileLines[i].IndexOf(':');
                    newMessage.MessageName = fileLines[i].Substring(0, prefixIndex);

                    //Message will take it from here
                    newMessage.ParseMessage(fileLines[i].Substring(prefixIndex + 2));

                    //Add new Message to the list
                    Messages.Add(newMessage);
                    continue;
                }

                return false;
            }

            return true;
        }

        public string CompileFileText()
        {
            var newFileText = string.Empty;

            //Generate and add header
            string h1 = "MESS_ARCHIVE_" + FileName + Environment.NewLine;
            string h2 = "Message Name: Message" + Environment.NewLine;
            newFileText += h1 + Environment.NewLine + h2 + Environment.NewLine;

            //Compile and add all messages
            for (int i = 0; i < Messages.Count; i++)
            {
                string compiledMsg = Messages[i].Compile();
                newFileText += compiledMsg + (i < Messages.Count - 1 ? Environment.NewLine : string.Empty);
            }

            return newFileText;
        }

        #region Enumerable
        public IEnumerator<Message> GetEnumerator()
        {
            return Messages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Messages.GetEnumerator();
        }
        #endregion
    }
}
