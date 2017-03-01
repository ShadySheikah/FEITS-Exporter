namespace FEITS.View
{
    partial class HalfBoxTester
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
            this.PB_TextBox = new System.Windows.Forms.PictureBox();
            this.B_Export = new System.Windows.Forms.Button();
            this.TB_Line = new System.Windows.Forms.TextBox();
            this.CB_TB = new System.Windows.Forms.ComboBox();
            this.LBL_TBType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB_TextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_TextBox
            // 
            this.PB_TextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PB_TextBox.Image = global::FEITS.Properties.Resources.HalfBox;
            this.PB_TextBox.InitialImage = global::FEITS.Properties.Resources.HalfBox;
            this.PB_TextBox.Location = new System.Drawing.Point(93, 12);
            this.PB_TextBox.Name = "PB_TextBox";
            this.PB_TextBox.Size = new System.Drawing.Size(198, 56);
            this.PB_TextBox.TabIndex = 0;
            this.PB_TextBox.TabStop = false;
            // 
            // B_Export
            // 
            this.B_Export.Location = new System.Drawing.Point(12, 95);
            this.B_Export.Name = "B_Export";
            this.B_Export.Size = new System.Drawing.Size(88, 28);
            this.B_Export.TabIndex = 2;
            this.B_Export.Text = "Export Line";
            this.B_Export.UseVisualStyleBackColor = true;
            this.B_Export.Click += new System.EventHandler(this.B_Export_Click);
            // 
            // TB_Line
            // 
            this.TB_Line.AllowDrop = true;
            this.TB_Line.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Line.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Line.Location = new System.Drawing.Point(12, 129);
            this.TB_Line.MaxLength = 2147483647;
            this.TB_Line.Multiline = true;
            this.TB_Line.Name = "TB_Line";
            this.TB_Line.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_Line.Size = new System.Drawing.Size(355, 120);
            this.TB_Line.TabIndex = 1;
            this.TB_Line.TextChanged += new System.EventHandler(this.TB_Line_TextChanged);
            this.TB_Line.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_Line_KeyDown);
            // 
            // CB_TB
            // 
            this.CB_TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_TB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_TB.FormattingEnabled = true;
            this.CB_TB.Items.AddRange(new object[] {
            "Standard",
            "Nohr",
            "Hoshido"});
            this.CB_TB.Location = new System.Drawing.Point(285, 100);
            this.CB_TB.Name = "CB_TB";
            this.CB_TB.Size = new System.Drawing.Size(82, 21);
            this.CB_TB.TabIndex = 3;
            this.CB_TB.SelectedIndexChanged += new System.EventHandler(this.CB_TB_SelectedIndexChanged);
            // 
            // LBL_TBType
            // 
            this.LBL_TBType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LBL_TBType.AutoSize = true;
            this.LBL_TBType.Location = new System.Drawing.Point(204, 103);
            this.LBL_TBType.Name = "LBL_TBType";
            this.LBL_TBType.Size = new System.Drawing.Size(75, 13);
            this.LBL_TBType.TabIndex = 4;
            this.LBL_TBType.Text = "TextBox Style:";
            // 
            // HalfBoxTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 261);
            this.Controls.Add(this.LBL_TBType);
            this.Controls.Add(this.CB_TB);
            this.Controls.Add(this.TB_Line);
            this.Controls.Add(this.B_Export);
            this.Controls.Add(this.PB_TextBox);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(395, 300);
            this.Name = "HalfBoxTester";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Half-Size Text Box";
            ((System.ComponentModel.ISupportInitialize)(this.PB_TextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_TextBox;
        private System.Windows.Forms.Button B_Export;
        private System.Windows.Forms.TextBox TB_Line;
        private System.Windows.Forms.ComboBox CB_TB;
        private System.Windows.Forms.Label LBL_TBType;
    }
}