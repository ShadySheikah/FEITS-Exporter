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
        private BackgroundWorker bw;

        internal LauncherController()
        {
            
        }

        internal LauncherController(Interface.ILaunchView v, Updater m)
        {
            view = v;
            updateModel = m;
            view.SetController(this);
            updateModel.SetController(this);

            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
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
            Settings.Default.Reset();

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
            return Settings.Default.UpdatePreference != 1 || updateModel.ImportantUpdatePending();
        }

        internal void StartUpdating()
        {
            if (!bw.IsBusy)
            {
                bw.RunWorkerAsync();
            }
        }

        public void CancelUpdate()
        {
            bw.CancelAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = (BackgroundWorker)sender;
            e.Result = updateModel.UpdateAssemblies(worker, e);
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            view.Progress = e.ProgressPercentage;
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Make sure update succeeded
            if (e.Error != null)
                MessageBox.Show(e.Error.Message, Resources.UpdateDownloadErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (e.Cancelled)
                return;
            else if ((bool)e.Result == false)
                MessageBox.Show(Resources.UpdateFailed, Resources.UpdateDownloadErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                //First arg is either blank or the application path, remove it
                List<string> args = Environment.GetCommandLineArgs().ToList();
                args.RemoveAt(0);
                string[] newArgs = args.ToArray();

                //Start file transition and exit
                updateModel.FinalizeUpdate(newArgs);
                Application.Exit();
            }

            ((Form) view).DialogResult = DialogResult.OK;
        }

        internal void UpdateLabel(string text)
        {
            view.StatusDesc = text;
        }
    }
}
