using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IfTextEditor.Editor.Model
{
    public partial class FileContainer
    {
        public partial class Message : IEnumerable<FileContainer.Message.Page>
        {
            public string MsgName { get; set; }
            public List<Page> Pages { get; } = new List<Page>();

            internal void ParseMessage(string message)
            {
                //Split messages by line-end markers
                var delimiters = new List<string> { "$k$p", "$k\\n" };
                string pattern = "(?<=" + string.Join("|", delimiters.Select(Regex.Escape).ToArray()) + ")";
                string[] splits = Regex.Split(message, pattern);

                foreach (string str in splits)
                {
                    Pages.Add(new Page(str));
                }
            }

            internal string ToString(bool includeName, bool stripped = false)
            {
                if (stripped)
                {
                    string strippedMess = string.Empty;

                    if (includeName)
                        strippedMess = (MsgName != string.Empty ? MsgName + Environment.NewLine : string.Empty);

                    return Pages.Aggregate(strippedMess, (current, page) => current + page.ToString(true));
                }

                string compMess = string.Empty;

                if (includeName)
                    compMess = (MsgName != string.Empty ? MsgName + ": " : string.Empty);

                return Pages.Aggregate(compMess, (current, page) => current + page.ToString());
            }

            public IEnumerator<Page> GetEnumerator()
            {
                return ((IEnumerable<Page>)Pages).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<Page>)Pages).GetEnumerator();
            }
        }
    }
}
