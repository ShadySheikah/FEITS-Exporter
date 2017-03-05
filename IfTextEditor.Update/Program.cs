using System;
using System.Windows.Forms;
using IfTextEditor.Update.View;
using IfTextEditor.Update.Model;
using IfTextEditor.Update.Controller;
using IfTextEditor.Update.Properties;

namespace IfTextEditor.Update
{
    public static class Program
    {
        public static void RunAutoUpdate()
        {
            var model = new Updater();
            var view = new LaunchView();
            var controller = new LauncherController(view, model);

            if (!controller.StartAutoUpdate())
                return;

            if (Settings.Default.UpdatePreference == 1)
                if(MessageBox.Show(Resources.UpdateAvailableInform, Resources.UpdateAvailableTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    return;

            view.ShowDialog();
        }

        public static void CheckUpdatesAtRuntime()
        {
            var model = new Updater();
            var view = new LaunchView();
            var controller = new LauncherController(view, model);

            if (!controller.CheckForUpdates())
            {
                MessageBox.Show(Resources.NoUpdatesFound, Resources.NoUpdatesFoundTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(Resources.UpdateAvailableInform, Resources.UpdateAvailableTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                return;

            view.Show();
        }

        public static void SetUpdatePreferences()
        {
            var controller = new LauncherController();
            controller.SetAutoUpdatePreferences();
        }
    }
}
