namespace FEITS.View
{
    partial class ScriptExport
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
            this.B_Close = new System.Windows.Forms.Button();
            this.TB_ScriptInput = new System.Windows.Forms.TextBox();
            this.LBL_CharInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // B_Close
            // 
            this.B_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Close.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Close.Location = new System.Drawing.Point(322, 227);
            this.B_Close.Name = "B_Close";
            this.B_Close.Size = new System.Drawing.Size(80, 25);
            this.B_Close.TabIndex = 2;
            this.B_Close.Text = "Close";
            this.B_Close.UseVisualStyleBackColor = true;
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
            this.TB_ScriptInput.ReadOnly = true;
            this.TB_ScriptInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_ScriptInput.Size = new System.Drawing.Size(390, 206);
            this.TB_ScriptInput.TabIndex = 1;
            this.TB_ScriptInput.TextChanged += new System.EventHandler(this.TB_ScriptInput_TextChanged);
            this.TB_ScriptInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_ScriptInput_KeyDown);
            // 
            // LBL_CharInfo
            // 
            this.LBL_CharInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_CharInfo.Location = new System.Drawing.Point(12, 227);
            this.LBL_CharInfo.Name = "LBL_CharInfo";
            this.LBL_CharInfo.Size = new System.Drawing.Size(304, 25);
            this.LBL_CharInfo.TabIndex = 3;
            this.LBL_CharInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScriptExport
            // 
            this.AcceptButton = this.B_Close;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.B_Close;
            this.ClientSize = new System.Drawing.Size(414, 261);
            this.Controls.Add(this.LBL_CharInfo);
            this.Controls.Add(this.TB_ScriptInput);
            this.Controls.Add(this.B_Close);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 300);
            this.Name = "ScriptExport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Script Export";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button B_Close;
        private System.Windows.Forms.TextBox TB_ScriptInput;
        private System.Windows.Forms.Label LBL_CharInfo;
    }
}