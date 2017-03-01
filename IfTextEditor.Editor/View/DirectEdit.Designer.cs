namespace FEITS.View
{
    partial class DirectEdit
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
            this.TB_LineEdit = new System.Windows.Forms.TextBox();
            this.LBL_Warning = new System.Windows.Forms.Label();
            this.B_SwapGenders = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // B_LoadScript
            // 
            this.B_LoadScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_LoadScript.Location = new System.Drawing.Point(236, 164);
            this.B_LoadScript.Name = "B_LoadScript";
            this.B_LoadScript.Size = new System.Drawing.Size(80, 25);
            this.B_LoadScript.TabIndex = 2;
            this.B_LoadScript.Text = "Save";
            this.B_LoadScript.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(322, 164);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(80, 25);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // TB_LineEdit
            // 
            this.TB_LineEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_LineEdit.Location = new System.Drawing.Point(12, 12);
            this.TB_LineEdit.MaxLength = 2147483647;
            this.TB_LineEdit.Multiline = true;
            this.TB_LineEdit.Name = "TB_LineEdit";
            this.TB_LineEdit.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_LineEdit.Size = new System.Drawing.Size(390, 117);
            this.TB_LineEdit.TabIndex = 1;
            this.TB_LineEdit.TextChanged += new System.EventHandler(this.TB_LineEdit_TextChanged);
            this.TB_LineEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_LineEdit_KeyDown);
            // 
            // LBL_Warning
            // 
            this.LBL_Warning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_Warning.ForeColor = System.Drawing.Color.Red;
            this.LBL_Warning.Location = new System.Drawing.Point(12, 138);
            this.LBL_Warning.Name = "LBL_Warning";
            this.LBL_Warning.Size = new System.Drawing.Size(218, 54);
            this.LBL_Warning.TabIndex = 4;
            this.LBL_Warning.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // B_SwapGenders
            // 
            this.B_SwapGenders.Location = new System.Drawing.Point(236, 135);
            this.B_SwapGenders.Name = "B_SwapGenders";
            this.B_SwapGenders.Size = new System.Drawing.Size(166, 23);
            this.B_SwapGenders.TabIndex = 6;
            this.B_SwapGenders.Text = "Swap Genders";
            this.B_SwapGenders.UseVisualStyleBackColor = true;
            this.B_SwapGenders.Click += new System.EventHandler(this.B_SwapGenders_Click);
            // 
            // DirectEdit
            // 
            this.AcceptButton = this.B_LoadScript;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(414, 201);
            this.Controls.Add(this.B_SwapGenders);
            this.Controls.Add(this.LBL_Warning);
            this.Controls.Add(this.TB_LineEdit);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_LoadScript);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 150);
            this.Name = "DirectEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Raw Edit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button B_LoadScript;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.TextBox TB_LineEdit;
        private System.Windows.Forms.Label LBL_Warning;
        private System.Windows.Forms.Button B_SwapGenders;
    }
}