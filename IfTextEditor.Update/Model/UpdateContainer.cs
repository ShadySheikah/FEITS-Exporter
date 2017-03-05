using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace IfTextEditor.Update.Model
{
    internal class UpdateContainer : IEnumerable<UpdateContainer.UpdateInfo>
    {
        public class UpdateInfo
        {
            internal string AssemblyName { get; set; }
            internal Version AssemblyVer { get; set; }
            internal Uri DownloadUri { get; set; }
            internal string Md5 { get; set; }
            internal bool UpdateAvailable { get; set; }
            internal bool ImportantUpdate { get; set; }
        }

        private List<UpdateInfo> updates;

        internal bool Parse(Uri xmlUri)
        {
            updates = new List<UpdateInfo>();

            try
            {
                var manifest = new XmlDocument();
                manifest.Load(xmlUri.AbsoluteUri);

                XmlNodeList nodeList = manifest.DocumentElement?.SelectNodes("//assembly[@name]");

                if (nodeList == null)
                    return false;

                foreach (XmlNode node in nodeList)
                {
                    var newUpdate = new UpdateInfo
                    {
                        AssemblyName = node.Name,
                        AssemblyVer = Version.Parse(node["version"].InnerText),
                        DownloadUri = new Uri(node["url"].InnerText),
                        Md5 = node["md5"].InnerText
                    };

                    updates.Add(newUpdate);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public IEnumerator<UpdateInfo> GetEnumerator()
        {
            return ((IEnumerable<UpdateInfo>)updates).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<UpdateInfo>)updates).GetEnumerator();
        }
    }
}
