using System;
using System.Collections.Generic;
using IfTextEditor.Update.Model;
using IfTextEditor.Update.Properties;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace IfTextEditor.Update.Controller
{
    internal class LauncherController
    {
        private readonly Interface.ILaunchView view;
        private readonly Updater updateModel;

        internal LauncherController()
        {
            
        }

        internal LauncherController(Interface.ILaunchView v, Updater m)
        {
            view = v;
            updateModel = m;
            view.SetController(this);
            updateModel.SetController(this);
        }

        internal void SetAutoUpdatePreferences()
        {
            //0 = Don't update; 1 = Update when important; 2 = Update any
            var settingsView = new View.Preferences();
            if (settingsView.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.UpdatePreference = settingsView.UpdatePreference;
                Settings.Default.ImportantUpdatesOnly = settingsView.ImportantUpdatesOnly;
            }

            Settings.Default.Save();
        }

        internal bool StartAutoUpdate()
        {
            //Settings.Default.Reset();

            //If first time, set auto-update preference
            if (Settings.Default.FirstTime)
            {
                MessageBox.Show(Resources.FirstTimerAutoUpdate, Resources.FirstTimerAutoUpdateTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetAutoUpdatePreferences();
                Settings.Default.FirstTime = false;
                Settings.Default.Save();
            }

            //If auto-updates disabled, stop here
            return Settings.Default.UpdatePreference > 0 && CheckForUpdates();
        }

        internal bool CheckForUpdates()
        {
            //If no update, stop
            if (!updateModel.CheckIfUpdateAvailable())
                return false;

            //If we only want important updates and there are none, stop
            return !Settings.Default.ImportantUpdatesOnly || updateModel.ImportantUpdatePending();
        }

        internal async void StartUpdating()
        {
            bool result = await updateModel.UpdateAssemblies();
            if (result)
            {
                //First arg is either blank or the application path, remove it
                List<string> args = Environment.GetCommandLineArgs().ToList();
                args.RemoveAt(0);
                string[] newArgs = args.ToArray();

                //Start file transition and exit
                updateModel.FinalizeUpdate(newArgs);
                Application.Exit();
            }

            MessageBox.Show(Resources.UpdateFailed, Resources.UpdateDownloadErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ((Form)view).DialogResult = DialogResult.OK;
        }

        public void CancelUpdate()
        {
            updateModel.CancelDownload();
        }

        internal void UpdateLabel(string text)
        {
            view.StatusDesc = text;
        }

        internal void UpdateProgress(int percentage)
        {
            view.Progress = percentage;
        }
    }
}
