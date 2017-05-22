using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IfTextEditor.Editor.Model
{
    public partial class FileContainer : IEnumerable<FileContainer.Message>
    {
        //Fields
        public string Name { get; set; }
        internal string Path { get; set; }
        public List<Message> Messages { get; }

        internal FileContainer()
        {
            Name = "Untitled";
            Path = string.Empty;
            Messages = new List<Message>();
        }

        internal bool PopulateContainer(string[] fileMsgs)
        {
            //Find and skip the header
            if (!fileMsgs[0].StartsWith("MESS_ARCHIVE_"))
            {
                for (int i = 0; i < fileMsgs.Length; i++)
                {
                    var newMessage = new Message();
                    if (fileMsgs[i].Contains("ID_"))
                    {
                        int tOffset = fileMsgs[i].IndexOf(':');
                        newMessage.MsgName = fileMsgs[i].Substring(0, tOffset);
                        fileMsgs[i] = fileMsgs[i].Substring(tOffset + 2);   //": "
                    }
                    else
                        newMessage.MsgName = "Imported Message" + (i > 0 ? " " + i : "");

                    newMessage.ParseMessage(fileMsgs[i]);
                    Messages.Add(newMessage);
                }
                return true;
            }

            Name = fileMsgs[0].Replace("MESS_ARCHIVE_", "").Substring(1);

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
                    newMessage.MsgName = fileMsgs[i].Substring(0, prefixIndex);

                    //Message will take it from here
                    newMessage.ParseMessage(fileMsgs[i].Substring(prefixIndex + 2));

                    //Add new Message to the list
                    Messages.Add(newMessage);
                }
            }

            return true;
        }

        public override string ToString()
        {
            var textBuilder = new StringBuilder();

            if (Name != string.Empty)
            {
                //Generate and add header
                textBuilder.Append("MESS_ARCHIVE_" + Name + Environment.NewLine);
                textBuilder.Append(Environment.NewLine + Environment.NewLine);
                textBuilder.Append("Message Name: Message" + Environment.NewLine);
                textBuilder.Append(Environment.NewLine + Environment.NewLine);
            }

            //Compile and add all messages
            for (int i = 0; i < Messages.Count; i++)
            {
                string compiledMsg = Messages[i].ToString(true);
                textBuilder.Append(compiledMsg + (i < Messages.Count - 1 ? Environment.NewLine : string.Empty));
            }

            return textBuilder.ToString();
        }

        public IEnumerator<Message> GetEnumerator()
        {
            return ((IEnumerable<Message>)Messages).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Message>)Messages).GetEnumerator();
        }
    }
}
