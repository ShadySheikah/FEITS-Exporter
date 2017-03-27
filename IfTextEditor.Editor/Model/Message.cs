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

            internal string ToString(bool includeName)
            {
                string compMess = string.Empty;

                if (includeName)
                    compMess = (MsgName != string.Empty ? MsgName + ": " : string.Empty);

                return Pages.Aggregate(compMess, (current, p) => current + p.ToString());
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
