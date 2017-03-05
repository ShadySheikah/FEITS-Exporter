namespace IfTextEditor.Update.View
{
    internal partial class Preferences
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
            this.TL_Base = new System.Windows.Forms.TableLayoutPanel();
            this.GB_AutoUpdate = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.RB_Auto_Auto = new System.Windows.Forms.RadioButton();
            this.RB_Auto_Notify = new System.Windows.Forms.RadioButton();
            this.RB_Auto_No = new System.Windows.Forms.RadioButton();
            this.GB_Priority = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.RB_Priority_All = new System.Windows.Forms.RadioButton();
            this.RB_Priority_Important = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_SaveChanges = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TL_Base.SuspendLayout();
            this.GB_AutoUpdate.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.GB_Priority.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TL_Base
            // 
            this.TL_Base.ColumnCount = 2;
            this.TL_Base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TL_Base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TL_Base.Controls.Add(this.GB_AutoUpdate, 0, 1);
            this.TL_Base.Controls.Add(this.GB_Priority, 1, 1);
            this.TL_Base.Controls.Add(this.flowLayoutPanel1, 1, 2);
            this.TL_Base.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.TL_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TL_Base.Location = new System.Drawing.Point(0, 0);
            this.TL_Base.Name = "TL_Base";
            this.TL_Base.RowCount = 3;
            this.TL_Base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TL_Base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TL_Base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TL_Base.Size = new System.Drawing.Size(344, 261);
            this.TL_Base.TabIndex = 0;
            // 
            // GB_AutoUpdate
            // 
            this.GB_AutoUpdate.Controls.Add(this.tableLayoutPanel2);
            this.GB_AutoUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GB_AutoUpdate.Location = new System.Drawing.Point(3, 118);
            this.GB_AutoUpdate.Name = "GB_AutoUpdate";
            this.GB_AutoUpdate.Size = new System.Drawing.Size(166, 109);
            this.GB_AutoUpdate.TabIndex = 9;
            this.GB_AutoUpdate.TabStop = false;
            this.GB_AutoUpdate.Text = "Auto Update";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.RB_Auto_Auto, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.RB_Auto_Notify, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.RB_Auto_No, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(160, 90);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // RB_Auto_Auto
            // 
            this.RB_Auto_Auto.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RB_Auto_Auto.AutoSize = true;
            this.RB_Auto_Auto.Location = new System.Drawing.Point(3, 6);
            this.RB_Auto_Auto.Name = "RB_Auto_Auto";
            this.RB_Auto_Auto.Size = new System.Drawing.Size(137, 17);
            this.RB_Auto_Auto.TabIndex = 0;
            this.RB_Auto_Auto.Text = "Download automatically";
            this.RB_Auto_Auto.UseVisualStyleBackColor = true;
            this.RB_Auto_Auto.CheckedChanged += new System.EventHandler(this.RB_Auto_Auto_CheckedChanged);
            // 
            // RB_Auto_Notify
            // 
            this.RB_Auto_Notify.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RB_Auto_Notify.AutoSize = true;
            this.RB_Auto_Notify.Location = new System.Drawing.Point(3, 36);
            this.RB_Auto_Notify.Name = "RB_Auto_Notify";
            this.RB_Auto_Notify.Size = new System.Drawing.Size(134, 17);
            this.RB_Auto_Notify.TabIndex = 1;
            this.RB_Auto_Notify.Text = "Notify before download";
            this.RB_Auto_Notify.UseVisualStyleBackColor = true;
            this.RB_Auto_Notify.CheckedChanged += new System.EventHandler(this.RB_Auto_Notify_CheckedChanged);
            // 
            // RB_Auto_No
            // 
            this.RB_Auto_No.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RB_Auto_No.AutoSize = true;
            this.RB_Auto_No.Location = new System.Drawing.Point(3, 66);
            this.RB_Auto_No.Name = "RB_Auto_No";
            this.RB_Auto_No.Size = new System.Drawing.Size(93, 17);
            this.RB_Auto_No.TabIndex = 2;
            this.RB_Auto_No.Text = "Do not update";
            this.RB_Auto_No.UseVisualStyleBackColor = true;
            this.RB_Auto_No.CheckedChanged += new System.EventHandler(this.RB_Auto_No_CheckedChanged);
            // 
            // GB_Priority
            // 
            this.GB_Priority.Controls.Add(this.tableLayoutPanel3);
            this.GB_Priority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GB_Priority.Location = new System.Drawing.Point(175, 118);
            this.GB_Priority.Name = "GB_Priority";
            this.GB_Priority.Size = new System.Drawing.Size(166, 109);
            this.GB_Priority.TabIndex = 8;
            this.GB_Priority.TabStop = false;
            this.GB_Priority.Text = "Priority";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.RB_Priority_All, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.RB_Priority_Important, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(160, 90);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // RB_Priority_All
            // 
            this.RB_Priority_All.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RB_Priority_All.AutoSize = true;
            this.RB_Priority_All.Location = new System.Drawing.Point(3, 14);
            this.RB_Priority_All.Name = "RB_Priority_All";
            this.RB_Priority_All.Size = new System.Drawing.Size(77, 17);
            this.RB_Priority_All.TabIndex = 0;
            this.RB_Priority_All.Text = "All updates";
            this.RB_Priority_All.UseVisualStyleBackColor = true;
            this.RB_Priority_All.CheckedChanged += new System.EventHandler(this.RB_Priority_All_CheckedChanged);
            // 
            // RB_Priority_Important
            // 
            this.RB_Priority_Important.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.RB_Priority_Important.AutoSize = true;
            this.RB_Priority_Important.Location = new System.Drawing.Point(3, 59);
            this.RB_Priority_Important.Name = "RB_Priority_Important";
            this.RB_Priority_Important.Size = new System.Drawing.Size(132, 17);
            this.RB_Priority_Important.TabIndex = 1;
            this.RB_Priority_Important.Text = "Important updates only";
            this.RB_Priority_Important.UseVisualStyleBackColor = true;
            this.RB_Priority_Important.CheckedChanged += new System.EventHandler(this.RB_Priority_Important_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.B_Cancel);
            this.flowLayoutPanel1.Controls.Add(this.B_SaveChanges);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(175, 233);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(166, 25);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(88, 3);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 1;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // B_SaveChanges
            // 
            this.B_SaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_SaveChanges.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_SaveChanges.Location = new System.Drawing.Point(7, 3);
            this.B_SaveChanges.Name = "B_SaveChanges";
            this.B_SaveChanges.Size = new System.Drawing.Size(75, 23);
            this.B_SaveChanges.TabIndex = 0;
            this.B_SaveChanges.Text = "Save";
            this.B_SaveChanges.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.TL_Base.SetColumnSpan(this.flowLayoutPanel2, 2);
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(338, 109);
            this.flowLayoutPanel2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auto Update";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(328, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose whether or not to be notified before downloading an update,\r\nor turn off a" +
    "uto-update completely.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Priority";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(310, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "If Text Editor is a work in process and will be frequently updated.\r\nYou can choo" +
    "se whether or not to opt out of minor updates.";
            // 
            // Preferences
            // 
            this.AcceptButton = this.B_SaveChanges;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.B_Cancel;
            this.ClientSize = new System.Drawing.Size(344, 261);
            this.Controls.Add(this.TL_Base);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 300);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 300);
            this.Name = "Preferences";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto Update Preferences";
            this.TL_Base.ResumeLayout(false);
            this.GB_AutoUpdate.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.GB_Priority.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TL_Base;
        private System.Windows.Forms.GroupBox GB_AutoUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton RB_Auto_Auto;
        private System.Windows.Forms.RadioButton RB_Auto_Notify;
        private System.Windows.Forms.RadioButton RB_Auto_No;
        private System.Windows.Forms.GroupBox GB_Priority;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton RB_Priority_All;
        private System.Windows.Forms.RadioButton RB_Priority_Important;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_SaveChanges;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}