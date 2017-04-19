using System;
using System.Windows.Forms;
using IfTextEditor.Editor.Model;
using IfTextEditor.Editor.Model.YamlTypeConverters;
using YamlDotNet.Serialization;

namespace IfTextEditor.Update.View
{
    public partial class Import : Form
    {
        internal string ImportedText { get; private set; } = string.Empty;

        private FileContainer fileCont;
        private int msgIndex;

        public Import()
        {
            InitializeComponent();
        }

        internal void SetForExport(FileContainer exportedFile, int curMessage)
        {
            fileCont = exportedFile;
            msgIndex = curMessage;

            GB_Scope.Enabled = true;
            RB_ScopeMessage.Checked = true;

            SetTextboxValue();
            TB_Input.ReadOnly = true;
            TB_Input.SelectAll();

            Text = "Export";
            B_Import.Text = "OK";
            AcceptButton = B_Import;
            B_Cancel.Visible = false;
        }

        private void SetTextboxValue()
        {
            Serializer ySerializer = new SerializerBuilder().WithTypeConverter(new ContainerTypeConverter()).Build();

            if (RB_ScopeFile.Checked)
                TB_Input.Text = RB_FormatPlaintext.Checked ? fileCont.ToString() : ySerializer.Serialize(fileCont);
            else
                TB_Input.Text = fileCont.Messages[msgIndex].ToString(false);
        }

        private void UpdateRadioChecks(RadioButton button, bool scope)
        {
            GroupBox currentBox = scope ? GB_Scope : GB_Format;

            foreach (object o in currentBox.Controls)
            {
                if (o.GetType() != typeof(RadioButton))
                    return;

                if (o != button)
                    ((RadioButton)o).Checked = false;
            }
        }

        #region Events

        private void TB_Input_TextChanged(object sender, EventArgs e)
        {
            ImportedText = TB_Input.Text;
            L_CharCount.Text = GB_Scope.Enabled ? (RB_FormatPlaintext.Checked ? "Character count: " + TB_Input.Text.Length : "Character count not available for serialized text.") : string.Empty;
        }

        private void RB_ScopeMessage_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            //Serialized isn't available for messages
            GB_Format.Enabled = false;
            if (RB_FormatSerialized.Checked || !RB_FormatPlaintext.Checked)
                RB_FormatPlaintext.Checked = true;
            else
                UpdateRadioChecks(button, true);

            SetTextboxValue();
        }

        private void RB_ScopeFile_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            GB_Format.Enabled = true;
            UpdateRadioChecks(button, true);
            SetTextboxValue();
        }

        private void RB_FormatPlaintext_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            UpdateRadioChecks(button, false);
            SetTextboxValue();
        }

        private void RB_FormatSerialized_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (!button.Checked)
                return;

            UpdateRadioChecks(button, false);
            SetTextboxValue();
        }
        #endregion
    }
}
