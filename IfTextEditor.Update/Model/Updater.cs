using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IfTextEditor.Update.Controller;

namespace IfTextEditor.Update.Model
{
    internal class Updater
    {
        private LauncherController controller;

        private readonly Uri xmlLocation;
        private readonly UpdateContainer updates = new UpdateContainer();
        private string[] libraries;
        private string tempPath;

        internal Updater()
        {
#if DEBUG
            xmlLocation = new Uri("https://raw.githubusercontent.com/ShadySheikah/IfTextEditor/dev/update.xml");
#endif
#if RELEASE
            xmlLocation = new Uri("https://raw.githubusercontent.com/ShadySheikah/IfTextEditor/master/update.xml");
#endif
        }

        internal void SetController(LauncherController c)
        {
            controller = c;
        }

        internal bool CheckIfUpdateAvailable()
        {
            var sw = new Stopwatch();
            sw.Start();

            try
            {
                //Check if manifest is available
                var request = (HttpWebRequest) WebRequest.Create(xmlLocation);
                var response = (HttpWebResponse) request.GetResponse();
                response.Close();

                if (response.StatusCode != HttpStatusCode.OK)
                    return false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

            //Download/parse manifest
            if (!updates.Parse(xmlLocation))
                return false;

            //Get assemblies from lib folder
            libraries = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib"), "*.dll");
            Dictionary<string, Version> assemblies = libraries.Select(FileVersionInfo.GetVersionInfo).ToDictionary(info => Path.GetFileName(info.FileName), info => Version.Parse(info.FileVersion));

            //Add the executable assembly to the dictionary
            string fileName = Assembly.GetEntryAssembly().Location;
            if (fileName != null)
            {
                FileVersionInfo exeInfo = FileVersionInfo.GetVersionInfo(fileName);
                assemblies.Add(Path.GetFileName(exeInfo.FileName), Version.Parse(exeInfo.FileVersion));
            }

            foreach (UpdateContainer.UpdateInfo update in updates)
            {
                //Check if current assemblies exist
                if (!assemblies.ContainsKey(update.AssemblyName))
                    update.UpdateAvailable = true;
                //Check if assembly version lower than manifest version
                else if (update.AssemblyVer > assemblies[update.AssemblyName])
                {
                    update.UpdateAvailable = true;
                    //Check if it's an important update (more than a patch)
                    if (update.AssemblyVer.Major > assemblies[update.AssemblyName].Major || update.AssemblyVer.Minor > assemblies[update.AssemblyName].Minor)
                        update.ImportantUpdate = true;
                }
            }

            sw.Stop();
            Debug.WriteLine("UPDATE CHECK TIME: " + sw.Elapsed);

            //Return results, prompt if update needed
            return updates.Any(update => update.UpdateAvailable);
        }

        internal bool ImportantUpdatePending()
        {
            return updates.Any(update => update.ImportantUpdate);
        }

        internal async Task<bool> UpdateAssemblies(BackgroundWorker worker, DoWorkEventArgs e)
        {
            //Set up temp folder
            tempPath = Path.Combine(Path.GetTempPath(), "IfTextEditor");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            var tempInfo = new DirectoryInfo(tempPath);
            foreach (FileInfo file in tempInfo.GetFiles())
                file.Delete();

            //For each new update, download to tmp folder
            foreach (UpdateContainer.UpdateInfo update in updates)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return false;
                }

                if (!update.UpdateAvailable)
                    continue;

                controller.UpdateLabel("Updating " + update.AssemblyName);
                string filePath = Path.Combine(tempPath, update.AssemblyName);
                await DownloadFile(update.DownloadUri, Path.Combine(tempPath, filePath), worker);

                controller.UpdateLabel("Verifying " + update.AssemblyName);
                if (!VerifyDownloadIntegrity(filePath, update))
                    return false;
            }

            return true;
        }

        private async Task DownloadFile(Uri fileLocation, string filePath, BackgroundWorker worker)
        {
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (o, e) =>
                {
                    worker.ReportProgress(e.ProgressPercentage);
                };

                await client.DownloadFileTaskAsync(fileLocation, filePath);
            }
        }

        private bool VerifyDownloadIntegrity(string path, UpdateContainer.UpdateInfo info)
        {
            var sw = new Stopwatch();
            sw.Start();
            //Get the downloaded file's hash
            byte[] fileHash = MD5.Create().ComputeHash(new FileStream(path, FileMode.Open));

            //Convert to string
            var builder = new StringBuilder();
            foreach (byte b in fileHash)
                builder.Append(b.ToString("x2").ToLower());

            Debug.WriteLine("BEFORE: " + builder);
            Debug.WriteLine("AFTER: " + builder.ToString().ToUpper());
            sw.Stop();
            Debug.WriteLine("TIME TO VERIFY: " + sw.Elapsed);

            return string.Equals(builder.ToString().ToUpper(), info.Md5, StringComparison.CurrentCultureIgnoreCase);
        }

        internal void FinalizeUpdate(string[] startupArgs)
        {
            //Base directory where the new files will go
            string destPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            //If arguments were passed on program start, carry them over to the next process
            string startArgs = startupArgs.Aggregate(string.Empty, (current, s) => current + (s + " "));

            //Pieces of the command line argument
            const string cmdStart = "/C choice /C Y /N /D Y /T 4 ";
            const string cmdMove = "& Move /Y \"{0}\" \"{1}\" ";
            var cmdEnd = "& Start \"\" /D \"{0}\" \"{1}\" {2}";

            //Get paths to new 
            string[] newFiles = Directory.GetFiles(tempPath);
            var moveArgs = new string[newFiles.Length];

            //Set move command args for each new file
            for (int i = 0; i < newFiles.Length; i++)
            {
                string fileName = Path.GetFileName(newFiles[i]);
                string movePath = Path.Combine(destPath, "lib", fileName);

                //If entry assembly (this), check for possible name diff
                FileVersionInfo fileInfo = FileVersionInfo.GetVersionInfo(newFiles[i]);
                if (fileInfo.FileDescription == Assembly.GetEntryAssembly().GetName().ToString())
                {
                    fileName = Path.GetFileName(Assembly.GetEntryAssembly().Location);
                    movePath = Path.Combine(destPath, fileName);
                    cmdEnd = string.Format(cmdEnd, destPath, fileName, startArgs);
                }
                else
                {
                    string exeLocation = Assembly.GetEntryAssembly().Location;
                    cmdEnd = string.Format(cmdEnd, Path.GetDirectoryName(exeLocation), Path.GetFileName(exeLocation), startArgs);
                }

                moveArgs[i] = string.Format(cmdMove, newFiles[i], movePath);
            }

            //What we'll pass to the command line
            string compArg = cmdStart + moveArgs.Where(s => s != null).Aggregate(string.Empty, (current, s) => current + s) + cmdEnd;

            //Start the command process
            var info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.Arguments = compArg;
            Process.Start(info);
        }
    }
}
