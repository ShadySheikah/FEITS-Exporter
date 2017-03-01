using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IfTextEditor.Editor.Model
{
    public partial class FileContainer
    {
        public partial class Message : IEnumerable<Message.Page>
        {
            public string MessageName { get; set; }
            public List<Page> Pages { get; } = new List<Page>();

            public void ParseMessage(string message)
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

            public string Compile(bool includeName = true)
            {
                string compMess = string.Empty;

                if (includeName)
                    compMess = (MessageName != string.Empty ? MessageName + ": " : string.Empty);

                return Pages.Aggregate(compMess, (current, p) => current + p.GetCompiledPage());
            }

            #region Enumerable
            public IEnumerator<Page> GetEnumerator()
            {
                return Pages.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return Pages.GetEnumerator();
            }
            #endregion
        }
    }
}
