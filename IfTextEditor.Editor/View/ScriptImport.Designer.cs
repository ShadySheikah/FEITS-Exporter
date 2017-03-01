namespace FEITS.View
{
    public partial class ScriptImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.B_LoadScript = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.TB_ScriptInput = new System.Windows.Forms.TextBox();
            this.LBL_Warning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // B_LoadScript
            // 
            this.B_LoadScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_LoadScript.Location = new System.Drawing.Point(236, 224);
            this.B_LoadScript.Name = "B_LoadScript";
            this.B_LoadScript.Size = new System.Drawing.Size(80, 25);
            this.B_LoadScript.TabIndex = 2;
            this.B_LoadScript.Text = "Import";
            this.B_LoadScript.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(322, 224);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(80, 25);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // TB_ScriptInput
            // 
            this.TB_ScriptInput.AllowDrop = true;
            this.TB_ScriptInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ScriptInput.Location = new System.Drawing.Point(12, 12);
            this.TB_ScriptInput.MaxLength = 2147483647;
            this.TB_ScriptInput.Multiline = true;
            this.TB_ScriptInput.Name = "TB_ScriptInput";
            this.TB_ScriptInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_ScriptInput.Size = new System.Drawing.Size(390, 206);
            this.TB_ScriptInput.TabIndex = 1;
            this.TB_ScriptInput.TextChanged += new System.EventHandler(this.TB_ScriptInput_TextChanged);
            this.TB_ScriptInput.DragDrop += new System.Windows.Forms.DragEventHandler(this.TB_ScriptInput_DragDrop);
            this.TB_ScriptInput.DragEnter += new System.Windows.Forms.DragEventHandler(this.TB_ScriptInput_DragEnter);
            this.TB_ScriptInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_ScriptInput_KeyDown);
            // 
            // LBL_Warning
            // 
            this.LBL_Warning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_Warning.ForeColor = System.Drawing.Color.Red;
            this.LBL_Warning.Location = new System.Drawing.Point(12, 226);
            this.LBL_Warning.Name = "LBL_Warning";
            this.LBL_Warning.Size = new System.Drawing.Size(218, 26);
            this.LBL_Warning.TabIndex = 4;
            // 
            // ScriptImport
            // 
            this.AcceptButton = this.B_LoadScript;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(414, 261);
            this.Controls.Add(this.LBL_Warning);
            this.Controls.Add(this.TB_ScriptInput);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_LoadScript);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 300);
            this.Name = "ScriptImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Script Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button B_LoadScript;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.TextBox TB_ScriptInput;
        private System.Windows.Forms.Label LBL_Warning;
    }
}