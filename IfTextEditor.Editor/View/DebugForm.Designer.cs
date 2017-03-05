namespace IfTextEditor.Editor.View
{
    partial class DebugForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TB_ParsedOutput = new System.Windows.Forms.TextBox();
            this.TB_Input = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.L_Message = new System.Windows.Forms.Label();
            this.CB_Message = new System.Windows.Forms.ComboBox();
            this.B_Parse = new System.Windows.Forms.Button();
            this.L_Page = new System.Windows.Forms.Label();
            this.NUD_Page = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Page)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TB_ParsedOutput, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TB_Input, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 561);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // TB_ParsedOutput
            // 
            this.TB_ParsedOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_ParsedOutput.Location = new System.Drawing.Point(3, 308);
            this.TB_ParsedOutput.Multiline = true;
            this.TB_ParsedOutput.Name = "TB_ParsedOutput";
            this.TB_ParsedOutput.Size = new System.Drawing.Size(478, 250);
            this.TB_ParsedOutput.TabIndex = 2;
            // 
            // TB_Input
            // 
            this.TB_Input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Input.Location = new System.Drawing.Point(3, 3);
            this.TB_Input.MaxLength = 150000;
            this.TB_Input.Multiline = true;
            this.TB_Input.Name = "TB_Input";
            this.TB_Input.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TB_Input.Size = new System.Drawing.Size(478, 249);
            this.TB_Input.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.L_Message);
            this.panel1.Controls.Add(this.CB_Message);
            this.panel1.Controls.Add(this.B_Parse);
            this.panel1.Controls.Add(this.L_Page);
            this.panel1.Controls.Add(this.NUD_Page);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 258);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 44);
            this.panel1.TabIndex = 1;
            // 
            // L_Message
            // 
            this.L_Message.AutoSize = true;
            this.L_Message.Location = new System.Drawing.Point(86, 4);
            this.L_Message.Name = "L_Message";
            this.L_Message.Size = new System.Drawing.Size(53, 13);
            this.L_Message.TabIndex = 4;
            this.L_Message.Text = "Message:";
            // 
            // CB_Message
            // 
            this.CB_Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_Message.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Message.FormattingEnabled = true;
            this.CB_Message.Location = new System.Drawing.Point(86, 21);
            this.CB_Message.Name = "CB_Message";
            this.CB_Message.Size = new System.Drawing.Size(263, 21);
            this.CB_Message.TabIndex = 3;
            this.CB_Message.SelectedIndexChanged += new System.EventHandler(this.CB_Message_SelectedIndexChanged);
            // 
            // B_Parse
            // 
            this.B_Parse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.B_Parse.Location = new System.Drawing.Point(4, 4);
            this.B_Parse.Name = "B_Parse";
            this.B_Parse.Size = new System.Drawing.Size(75, 37);
            this.B_Parse.TabIndex = 2;
            this.B_Parse.Text = "Parse";
            this.B_Parse.UseVisualStyleBackColor = true;
            this.B_Parse.Click += new System.EventHandler(this.B_Parse_Click);
            // 
            // L_Page
            // 
            this.L_Page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.L_Page.AutoSize = true;
            this.L_Page.Location = new System.Drawing.Point(355, 4);
            this.L_Page.Name = "L_Page";
            this.L_Page.Size = new System.Drawing.Size(35, 13);
            this.L_Page.TabIndex = 1;
            this.L_Page.Text = "Page:";
            // 
            // NUD_Page
            // 
            this.NUD_Page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NUD_Page.Location = new System.Drawing.Point(355, 21);
            this.NUD_Page.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_Page.Name = "NUD_Page";
            this.NUD_Page.Size = new System.Drawing.Size(120, 20);
            this.NUD_Page.TabIndex = 0;
            this.NUD_Page.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_Page.ValueChanged += new System.EventHandler(this.NUD_Page_ValueChanged);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Page)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox TB_Input;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label L_Message;
        private System.Windows.Forms.ComboBox CB_Message;
        private System.Windows.Forms.Button B_Parse;
        private System.Windows.Forms.Label L_Page;
        private System.Windows.Forms.NumericUpDown NUD_Page;
        private System.Windows.Forms.TextBox TB_ParsedOutput;
    }
}