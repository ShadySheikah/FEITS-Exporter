namespace IfTextEditor.Update.View
{
    partial class Import
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
            this.B_Import = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.GB_Scope = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.RB_ScopeMessage = new System.Windows.Forms.RadioButton();
            this.RB_ScopeFile = new System.Windows.Forms.RadioButton();
            this.GB_Format = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.RB_FormatPlaintext = new System.Windows.Forms.RadioButton();
            this.RB_FormatSerialized = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.L_CharCount = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TB_Input = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.GB_Scope.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.GB_Format.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // B_Import
            // 
            this.B_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Import.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.B_Import.Location = new System.Drawing.Point(377, 3);
            this.B_Import.Name = "B_Import";
            this.B_Import.Size = new System.Drawing.Size(75, 23);
            this.B_Import.TabIndex = 0;
            this.B_Import.Text = "Import";
            this.B_Import.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.B_Cancel.Location = new System.Drawing.Point(296, 3);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(75, 23);
            this.B_Cancel.TabIndex = 1;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 606);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.GB_Scope, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.GB_Format, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(317, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(12, 0, 12, 12);
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(144, 597);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // GB_Scope
            // 
            this.GB_Scope.Controls.Add(this.flowLayoutPanel2);
            this.GB_Scope.Enabled = false;
            this.GB_Scope.Location = new System.Drawing.Point(15, 3);
            this.GB_Scope.Name = "GB_Scope";
            this.GB_Scope.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.GB_Scope.Size = new System.Drawing.Size(114, 77);
            this.GB_Scope.TabIndex = 1;
            this.GB_Scope.TabStop = false;
            this.GB_Scope.Text = "Scope";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.RB_ScopeMessage);
            this.flowLayoutPanel2.Controls.Add(this.RB_ScopeFile);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 16);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(111, 58);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // RB_ScopeMessage
            // 
            this.RB_ScopeMessage.AutoSize = true;
            this.RB_ScopeMessage.Location = new System.Drawing.Point(3, 9);
            this.RB_ScopeMessage.Name = "RB_ScopeMessage";
            this.RB_ScopeMessage.Size = new System.Drawing.Size(105, 17);
            this.RB_ScopeMessage.TabIndex = 2;
            this.RB_ScopeMessage.Text = "Current Message";
            this.RB_ScopeMessage.UseVisualStyleBackColor = true;
            this.RB_ScopeMessage.CheckedChanged += new System.EventHandler(this.RB_ScopeMessage_CheckedChanged);
            // 
            // RB_ScopeFile
            // 
            this.RB_ScopeFile.AutoSize = true;
            this.RB_ScopeFile.Location = new System.Drawing.Point(3, 32);
            this.RB_ScopeFile.Name = "RB_ScopeFile";
            this.RB_ScopeFile.Size = new System.Drawing.Size(71, 17);
            this.RB_ScopeFile.TabIndex = 3;
            this.RB_ScopeFile.Text = "Entire File";
            this.RB_ScopeFile.UseVisualStyleBackColor = true;
            this.RB_ScopeFile.CheckedChanged += new System.EventHandler(this.RB_ScopeFile_CheckedChanged);
            // 
            // GB_Format
            // 
            this.GB_Format.Controls.Add(this.flowLayoutPanel1);
            this.GB_Format.Enabled = false;
            this.GB_Format.Location = new System.Drawing.Point(15, 91);
            this.GB_Format.Name = "GB_Format";
            this.GB_Format.Size = new System.Drawing.Size(114, 77);
            this.GB_Format.TabIndex = 0;
            this.GB_Format.TabStop = false;
            this.GB_Format.Text = "Format";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.RB_FormatPlaintext);
            this.flowLayoutPanel1.Controls.Add(this.RB_FormatSerialized);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(108, 58);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // RB_FormatPlaintext
            // 
            this.RB_FormatPlaintext.AutoSize = true;
            this.RB_FormatPlaintext.Location = new System.Drawing.Point(9, 9);
            this.RB_FormatPlaintext.Name = "RB_FormatPlaintext";
            this.RB_FormatPlaintext.Size = new System.Drawing.Size(65, 17);
            this.RB_FormatPlaintext.TabIndex = 2;
            this.RB_FormatPlaintext.Text = "Plaintext";
            this.RB_FormatPlaintext.UseVisualStyleBackColor = true;
            this.RB_FormatPlaintext.CheckedChanged += new System.EventHandler(this.RB_FormatPlaintext_CheckedChanged);
            // 
            // RB_FormatSerialized
            // 
            this.RB_FormatSerialized.AutoSize = true;
            this.RB_FormatSerialized.Location = new System.Drawing.Point(9, 32);
            this.RB_FormatSerialized.Name = "RB_FormatSerialized";
            this.RB_FormatSerialized.Size = new System.Drawing.Size(70, 17);
            this.RB_FormatSerialized.TabIndex = 3;
            this.RB_FormatSerialized.Text = "Serialized";
            this.RB_FormatSerialized.UseVisualStyleBackColor = true;
            this.RB_FormatSerialized.CheckedChanged += new System.EventHandler(this.RB_FormatSerialized_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.L_CharCount);
            this.panel1.Controls.Add(this.B_Import);
            this.panel1.Controls.Add(this.B_Cancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 568);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 38);
            this.panel1.TabIndex = 3;
            // 
            // L_CharCount
            // 
            this.L_CharCount.AutoSize = true;
            this.L_CharCount.Location = new System.Drawing.Point(12, 8);
            this.L_CharCount.Name = "L_CharCount";
            this.L_CharCount.Size = new System.Drawing.Size(0, 13);
            this.L_CharCount.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TB_Input);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(311, 565);
            this.panel2.TabIndex = 3;
            // 
            // TB_Input
            // 
            this.TB_Input.Location = new System.Drawing.Point(3, 3);
            this.TB_Input.MaxLength = 250000;
            this.TB_Input.Multiline = true;
            this.TB_Input.Name = "TB_Input";
            this.TB_Input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_Input.Size = new System.Drawing.Size(305, 559);
            this.TB_Input.TabIndex = 2;
            this.TB_Input.TextChanged += new System.EventHandler(this.TB_Input_TextChanged);
            // 
            // Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 606);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(320, 480);
            this.Name = "Import";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.GB_Scope.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.GB_Format.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button B_Import;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox GB_Format;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton RB_FormatSerialized;
        private System.Windows.Forms.RadioButton RB_FormatPlaintext;
        private System.Windows.Forms.GroupBox GB_Scope;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton RB_ScopeFile;
        private System.Windows.Forms.RadioButton RB_ScopeMessage;
        private System.Windows.Forms.Label L_CharCount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TB_Input;
    }
}