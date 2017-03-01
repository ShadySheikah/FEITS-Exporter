namespace FEITS.View
{
    partial class FriendlyReminder
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
            this.PB_Conv1 = new System.Windows.Forms.PictureBox();
            this.PB_Conv0 = new System.Windows.Forms.PictureBox();
            this.LBL_Conv1 = new System.Windows.Forms.Label();
            this.LBL_Title = new System.Windows.Forms.Label();
            this.LBL_Conv0 = new System.Windows.Forms.Label();
            this.LBL_CharLimit = new System.Windows.Forms.Label();
            this.LBL_UnderLimit = new System.Windows.Forms.Label();
            this.B_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Conv1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Conv0)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_Conv1
            // 
            this.PB_Conv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_Conv1.Image = global::FEITS.Properties.Resources.Reminder_1;
            this.PB_Conv1.InitialImage = global::FEITS.Properties.Resources.Reminder_1;
            this.PB_Conv1.Location = new System.Drawing.Point(12, 36);
            this.PB_Conv1.Name = "PB_Conv1";
            this.PB_Conv1.Size = new System.Drawing.Size(381, 54);
            this.PB_Conv1.TabIndex = 1;
            this.PB_Conv1.TabStop = false;
            // 
            // PB_Conv0
            // 
            this.PB_Conv0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_Conv0.Image = global::FEITS.Properties.Resources.Reminder_0;
            this.PB_Conv0.InitialImage = global::FEITS.Properties.Resources.Reminder_0;
            this.PB_Conv0.Location = new System.Drawing.Point(12, 112);
            this.PB_Conv0.Name = "PB_Conv0";
            this.PB_Conv0.Size = new System.Drawing.Size(381, 53);
            this.PB_Conv0.TabIndex = 3;
            this.PB_Conv0.TabStop = false;
            // 
            // LBL_Conv1
            // 
            this.LBL_Conv1.AutoSize = true;
            this.LBL_Conv1.Location = new System.Drawing.Point(9, 93);
            this.LBL_Conv1.Name = "LBL_Conv1";
            this.LBL_Conv1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.LBL_Conv1.Size = new System.Drawing.Size(104, 16);
            this.LBL_Conv1.TabIndex = 2;
            this.LBL_Conv1.Text = "Normal conversation";
            // 
            // LBL_Title
            // 
            this.LBL_Title.AutoSize = true;
            this.LBL_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Title.Location = new System.Drawing.Point(8, 13);
            this.LBL_Title.Name = "LBL_Title";
            this.LBL_Title.Size = new System.Drawing.Size(119, 20);
            this.LBL_Title.TabIndex = 1;
            this.LBL_Title.Text = "Mind the cutoff!";
            // 
            // LBL_Conv0
            // 
            this.LBL_Conv0.AutoSize = true;
            this.LBL_Conv0.Location = new System.Drawing.Point(9, 168);
            this.LBL_Conv0.Name = "LBL_Conv0";
            this.LBL_Conv0.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.LBL_Conv0.Size = new System.Drawing.Size(112, 16);
            this.LBL_Conv0.TabIndex = 3;
            this.LBL_Conv0.Text = "Two-box conversation";
            // 
            // LBL_CharLimit
            // 
            this.LBL_CharLimit.AutoSize = true;
            this.LBL_CharLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_CharLimit.ForeColor = System.Drawing.Color.Red;
            this.LBL_CharLimit.Location = new System.Drawing.Point(8, 187);
            this.LBL_CharLimit.Margin = new System.Windows.Forms.Padding(3);
            this.LBL_CharLimit.Name = "LBL_CharLimit";
            this.LBL_CharLimit.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.LBL_CharLimit.Size = new System.Drawing.Size(386, 23);
            this.LBL_CharLimit.TabIndex = 4;
            this.LBL_CharLimit.Text = "Character limit is estimated to be around 6,000-6,200.";
            // 
            // LBL_UnderLimit
            // 
            this.LBL_UnderLimit.AutoSize = true;
            this.LBL_UnderLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_UnderLimit.Location = new System.Drawing.Point(9, 213);
            this.LBL_UnderLimit.Name = "LBL_UnderLimit";
            this.LBL_UnderLimit.Size = new System.Drawing.Size(315, 16);
            this.LBL_UnderLimit.TabIndex = 5;
            this.LBL_UnderLimit.Text = "Please make sure all messages are below this limit.";
            // 
            // B_Close
            // 
            this.B_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Close.Location = new System.Drawing.Point(313, 242);
            this.B_Close.Name = "B_Close";
            this.B_Close.Size = new System.Drawing.Size(75, 23);
            this.B_Close.TabIndex = 0;
            this.B_Close.Text = "Close";
            this.B_Close.UseVisualStyleBackColor = true;
            this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
            // 
            // FriendlyReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 281);
            this.Controls.Add(this.B_Close);
            this.Controls.Add(this.LBL_UnderLimit);
            this.Controls.Add(this.LBL_CharLimit);
            this.Controls.Add(this.LBL_Conv0);
            this.Controls.Add(this.PB_Conv0);
            this.Controls.Add(this.LBL_Conv1);
            this.Controls.Add(this.PB_Conv1);
            this.Controls.Add(this.LBL_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 320);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 320);
            this.Name = "FriendlyReminder";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Friendly Reminders";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FriendlyReminder_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.PB_Conv1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_Conv0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_Conv1;
        private System.Windows.Forms.PictureBox PB_Conv0;
        private System.Windows.Forms.Label LBL_Conv1;
        private System.Windows.Forms.Label LBL_Title;
        private System.Windows.Forms.Label LBL_Conv0;
        private System.Windows.Forms.Label LBL_CharLimit;
        private System.Windows.Forms.Label LBL_UnderLimit;
        private System.Windows.Forms.Button B_Close;
    }
}