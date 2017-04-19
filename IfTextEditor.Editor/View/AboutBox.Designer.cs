namespace IfTextEditor.Editor.View
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.L_Creator = new System.Windows.Forms.Label();
            this.L_EditorVer = new System.Windows.Forms.Label();
            this.L_ResourceVer = new System.Windows.Forms.Label();
            this.L_UpdaterVer = new System.Windows.Forms.Label();
            this.TB_Description = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tableLayoutPanel.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.L_Creator, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.L_EditorVer, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.L_ResourceVer, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.L_UpdaterVer, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.TB_Description, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.okButton, 1, 5);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 6;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(417, 265);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel.SetRowSpan(this.logoPictureBox, 6);
            this.logoPictureBox.Size = new System.Drawing.Size(131, 259);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // L_Creator
            // 
            this.L_Creator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_Creator.Location = new System.Drawing.Point(143, 0);
            this.L_Creator.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.L_Creator.MaximumSize = new System.Drawing.Size(0, 17);
            this.L_Creator.Name = "L_Creator";
            this.L_Creator.Size = new System.Drawing.Size(271, 17);
            this.L_Creator.TabIndex = 19;
            this.L_Creator.Text = "Creator";
            this.L_Creator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_EditorVer
            // 
            this.L_EditorVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_EditorVer.Location = new System.Drawing.Point(143, 26);
            this.L_EditorVer.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.L_EditorVer.MaximumSize = new System.Drawing.Size(0, 17);
            this.L_EditorVer.Name = "L_EditorVer";
            this.L_EditorVer.Size = new System.Drawing.Size(271, 17);
            this.L_EditorVer.TabIndex = 0;
            this.L_EditorVer.Text = "Editor Version";
            this.L_EditorVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_ResourceVer
            // 
            this.L_ResourceVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_ResourceVer.Location = new System.Drawing.Point(143, 52);
            this.L_ResourceVer.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.L_ResourceVer.MaximumSize = new System.Drawing.Size(0, 17);
            this.L_ResourceVer.Name = "L_ResourceVer";
            this.L_ResourceVer.Size = new System.Drawing.Size(271, 17);
            this.L_ResourceVer.TabIndex = 21;
            this.L_ResourceVer.Text = "Resources Version";
            this.L_ResourceVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // L_UpdaterVer
            // 
            this.L_UpdaterVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L_UpdaterVer.Location = new System.Drawing.Point(143, 78);
            this.L_UpdaterVer.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.L_UpdaterVer.MaximumSize = new System.Drawing.Size(0, 17);
            this.L_UpdaterVer.Name = "L_UpdaterVer";
            this.L_UpdaterVer.Size = new System.Drawing.Size(271, 17);
            this.L_UpdaterVer.TabIndex = 22;
            this.L_UpdaterVer.Text = "Updater Version";
            this.L_UpdaterVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TB_Description
            // 
            this.TB_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Description.Location = new System.Drawing.Point(143, 107);
            this.TB_Description.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.TB_Description.Multiline = true;
            this.TB_Description.Name = "TB_Description";
            this.TB_Description.ReadOnly = true;
            this.TB_Description.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_Description.Size = new System.Drawing.Size(271, 126);
            this.TB_Description.TabIndex = 23;
            this.TB_Description.TabStop = false;
            this.TB_Description.Text = "Special thanks to SciresM for his work on FEITS and many other priceless tools th" +
    "at help make the 3DS modding community as vibrant as it is.";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(339, 239);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 24;
            this.okButton.Text = "&OK";
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About ITE";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label L_Creator;
        private System.Windows.Forms.Label L_EditorVer;
        private System.Windows.Forms.Label L_ResourceVer;
        private System.Windows.Forms.Label L_UpdaterVer;
        private System.Windows.Forms.TextBox TB_Description;
        private System.Windows.Forms.Button okButton;
    }
}
