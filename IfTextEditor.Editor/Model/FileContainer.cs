using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IfTextEditor.Editor.Model
{
    internal partial class FileContainer : IEnumerable<FileContainer.Message>
    {
        //Fields
        internal string FileName { get; set; }
        internal string FilePath { get; set; }
        internal List<Message> Messages { get; }

        internal FileContainer()
        {
            FileName = FilePath = string.Empty;
            Messages = new List<Message>();
        }

        internal bool PopulateContainer(string[] fileMsgs)
        {
            //Find and skip the header
            if (!fileMsgs[0].StartsWith("MESS_ARCHIVE_"))
            {
                for (int i = 0; i < fileMsgs.Length; i++)
                {
                    var newMessage = new Message {MessageName = "Imported Message" + (i > 0 ? " " +  i : "")};
                    newMessage.ParseMessage(fileMsgs[i]);
                    Messages.Add(newMessage);
                }
                return true;
            }

            FileName = fileMsgs[0].Substring(13);

            int offset = -1;
            for (int i = 0; i < fileMsgs.Length; i++)
            {
                //Contents begin after "Message Name: Message"
                if (!fileMsgs[i].Contains(':'))
                    continue;

                if (offset > -1)
                {
                    offset = i;
                    break;
                }

                offset = i;
            }

            for (int i = offset; i < fileMsgs.Length; i++)
            {
                //Set the message name/prefix
                if (fileMsgs[i].Contains(':'))
                {
                    var newMessage = new Message();
                    int prefixIndex = fileMsgs[i].IndexOf(':');
                    newMessage.MessageName = fileMsgs[i].Substring(0, prefixIndex);

                    //Message will take it from here
                    newMessage.ParseMessage(fileMsgs[i].Substring(prefixIndex + 2));

                    //Add new Message to the list
                    Messages.Add(newMessage);
                }
            }

            return true;
        }

        internal string CompileFileText()
        {
            var newFileText = string.Empty;

            if (FileName != string.Empty)
            {
                //Generate and add header
                string h1 = "MESS_ARCHIVE_" + FileName + Environment.NewLine;
                string h2 = "Message Name: Message" + Environment.NewLine;
                newFileText += h1 + Environment.NewLine + h2 + Environment.NewLine;
            }

            //Compile and add all messages
            for (int i = 0; i < Messages.Count; i++)
            {
                string compiledMsg = Messages[i].Compile();
                newFileText += compiledMsg + (i < Messages.Count - 1 ? Environment.NewLine : string.Empty);
            }

            return newFileText;
        }

        #region Enumerator

        public IEnumerator<Message> GetEnumerator()
        {
            return ((IEnumerable<Message>)Messages).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Message>)Messages).GetEnumerator();
        }
        #endregion
    }
}
