using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IfTextEditor.Editor.Model
{
    internal partial class FileContainer
    {
        internal partial class Message : IEnumerable<Message.Page>
        {
            internal string MessageName { get; set; }
            internal List<Page> Pages { get; } = new List<Page>();

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

            internal string Compile(bool includeName = true)
            {
                string compMess = string.Empty;

                if (includeName)
                    compMess = (MessageName != string.Empty ? MessageName + ": " : string.Empty);

                return Pages.Aggregate(compMess, (current, p) => current + p.GetCompiledPage());
            }

            #region Enumerable

            public IEnumerator<Page> GetEnumerator()
            {
                return ((IEnumerable<Page>)Pages).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<Page>)Pages).GetEnumerator();
            }
            #endregion
        }
    }
}
