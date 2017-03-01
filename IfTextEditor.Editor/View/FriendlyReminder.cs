using FEITS.Controller;
using System;
using System.Windows.Forms;

namespace FEITS.View
{
    public partial class FriendlyReminder : Form
    {
        private MainController cont;

        public FriendlyReminder()
        {
            InitializeComponent();
        }

        public void SetController(MainController controller)
        {
            cont = controller;
        }

        private void B_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FriendlyReminder_FormClosed(object sender, FormClosedEventArgs e)
        {
            cont.ReminderOpen = false;
        }
    }
}
