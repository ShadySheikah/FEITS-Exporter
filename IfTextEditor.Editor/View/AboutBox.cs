using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IfTextEditor.Editor.View
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            L_Creator.Text = $"Written by: {CreatorName}";
            L_EditorVer.Text = $"Editor ver: {EditorVersion}";
            L_ResourceVer.Text = $"Resources ver: {ResourceVersion}";
            L_UpdaterVer.Text = $"Updater ver: {UpdaterVersion}";
        }

        #region Assembly Attribute Accessors

        private string EditorVersion
        {
            get
            {
                string fileName = Assembly.GetExecutingAssembly().Location;
                return FileVersionInfo.GetVersionInfo(fileName).FileVersion;
            }
        }

        private string CreatorName
        {
            get
            {
                string fileName = Assembly.GetExecutingAssembly().Location;
                return FileVersionInfo.GetVersionInfo(fileName).CompanyName;
            }
        }

        private string ResourceVersion
        {
            get
            {
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "Editor.Resources.dll");
                return FileVersionInfo.GetVersionInfo(fileName).FileVersion;
            }
        }

        private string UpdaterVersion
        {
            get
            {
                string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "Editor.Update.dll");
                return FileVersionInfo.GetVersionInfo(fileName).FileVersion;
            }
        }
        #endregion
    }
}
