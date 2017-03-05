using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VersionLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = string.Empty;
#if DEBUG
            path = "https://raw.githubusercontent.com/ShadySheikah/IfTextEditor/dev/IfTextEditor.Editor/dist/";
#endif
#if RELEASE
            path = "https://raw.githubusercontent.com/ShadySheikah/IfTextEditor/master/IfTextEditor.Editor/dist/";
#endif

            Dictionary<string, Version> assList = GetAssemblies();
            var updateables = new List<UpdateableAssembly>();

            foreach (KeyValuePair<string, Version> entry in assList)
            {
                try
                {
                    //Hash
                    byte[] fileHash = MD5.Create().ComputeHash(new FileStream(entry.Key, FileMode.Open));
                    var builder = new StringBuilder();
                    foreach (byte b in fileHash)
                        builder.Append(b.ToString("x2").ToLower());

                    //Get hash to string
                    string md5 = builder.ToString().ToUpper();

                    string assName = Path.GetFileName(entry.Key);

                    //If it's the executable, keep outside the lib folder
                    string newUrl = assName.EndsWith(".exe") ?
                        path + assName.Replace(" ", "%20") : path + "lib/" + assName.Replace(" ", "%20");

                    var ua = new UpdateableAssembly
                    {
                        name = assName,
                        version = entry.Value,
                        url = newUrl,
                        md5 = md5
                    };

                    updateables.Add(ua);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            CreateXml(updateables);
        }

        private static Dictionary<string, Version> GetAssemblies()
        {
            //Get assemblies from lib folder
            IEnumerable<string> lib = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*", SearchOption.AllDirectories).Where(s => s.EndsWith(".exe") || s.EndsWith(".dll"));
            foreach (string s in lib)
                Console.WriteLine("ASSEMBLY: " + s);

            return lib.Select(FileVersionInfo.GetVersionInfo).Where(info => info.FileVersion != null).ToDictionary(info => info.FileName, info => new Version(info.FileVersion));
        }

        private static void CreateXml(List<UpdateableAssembly> list)
        {
            var settings = new XmlWriterSettings {Indent = true};
            XmlWriter writer = XmlWriter.Create("update.xml", settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("IfTextEditor");

            foreach (UpdateableAssembly ua in list)
            {
                writer.WriteStartElement("assembly");
                writer.WriteAttributeString("name", ua.name);

                writer.WriteStartElement("version");
                writer.WriteString(ua.version.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("url");
                writer.WriteString(ua.url);
                writer.WriteEndElement();

                writer.WriteStartElement("md5");
                writer.WriteString(ua.md5);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }

            writer.WriteEndDocument();
            writer.Close();
        }
    }

    internal class UpdateableAssembly
    {
        public string name { get; set; }
        public Version version { get; set; }
        public string url { get; set; }
        public string md5 { get; set; }
    }
}
