using IfTextEditor.Update.Controller;
using System;
using System.Windows.Forms;

namespace IfTextEditor.Update.View
{
    internal partial class LaunchView : Form, Controller.Interface.ILaunchView
    {
        private LauncherController controller;

        public string StatusDesc
        {
            get { return L_UpdateStatus.Text; }
            set { L_UpdateStatus.Text = value; }
        }

        public int Progress
        {
            get { return PB_UpdateProgress.Value; }
            set { PB_UpdateProgress.Value = value; }
        }

        public bool CanCancel
        {
            get { return B_Cancel.Enabled; }
            set { B_Cancel.Enabled = value; }
        }

        public bool IgnoreUpdatePreferences { get; set; }

        public LaunchView()
        {
            InitializeComponent();
        }

        public void SetController(LauncherController c)
        {
            controller = c;
        }

        private void B_Cancel_Click(object sender, EventArgs e)
        {
            controller.CancelUpdate();
        }

        private void LaunchView_Shown(object sender, EventArgs e)
        {
            controller.StartUpdating();
        }
    }
}
