using System;
using System.Windows.Forms;

namespace IfTextEditor.Update.View
{
    internal partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
        }

        public int UpdatePreference { get; set; } = 0;
        public bool ImportantUpdatesOnly { get; set; } = false;

        private void RB_Auto_Auto_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            UpdatePreference = 2;
            UpdateRadioChecks(button, true);
        }

        private void RB_Auto_Notify_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            UpdatePreference = 1;
            UpdateRadioChecks(button, true);
        }

        private void RB_Auto_No_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            UpdatePreference = 0;
            UpdateRadioChecks(button, true);
        }

        //Priority
        private void RB_Priority_All_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            ImportantUpdatesOnly = false;
            UpdateRadioChecks(button, false);
        }

        private void RB_Priority_Important_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            ImportantUpdatesOnly = true;
            UpdateRadioChecks(button, false);
        }

        private void UpdateRadioChecks(RadioButton button, bool autoUpdateGroup)
        {
            GroupBox currentBox = autoUpdateGroup ? GB_AutoUpdate : GB_Priority;

            foreach (object o in currentBox.Controls)
            {
                if (o.GetType() != typeof(RadioButton))
                    return;

                if (o != button)
                    ((RadioButton)o).Checked = false;
            }
        }
    }
}
