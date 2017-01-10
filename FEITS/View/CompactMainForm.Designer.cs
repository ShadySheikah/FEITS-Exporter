namespace FEITS.View
{
    partial class CompactMainForm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(CompactMainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TSL_PageNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSL_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MI_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.M_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_EditLine = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Message = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MI_SpellCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_TBStyles = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_TBStandard = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_TBNohr = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_TBHoshido = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_PlayerGender = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_PlayerMale = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_PlayerFemale = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MI_EnableBackgrounds = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_HalfBox = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_CompareMode = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MI_Reminder = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LB_MessageList = new System.Windows.Forms.ListBox();
            this.TB_PlayerName = new System.Windows.Forms.TextBox();
            this.LBL_PlayerName = new System.Windows.Forms.Label();
            this.B_PrevLine = new System.Windows.Forms.Button();
            this.TB_CurrentPage = new System.Windows.Forms.TextBox();
            this.B_NextLine = new System.Windows.Forms.Button();
            this.PB_PreviewBox = new System.Windows.Forms.PictureBox();
            this.TB_CurrentLine = new FEITS.View.SpellBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSL_PageNumber,
            this.TSL_Status});
            this.statusStrip1.Location = new System.Drawing.Point(9, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(556, 24);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TSL_PageNumber
            // 
            this.TSL_PageNumber.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.TSL_PageNumber.Name = "TSL_PageNumber";
            this.TSL_PageNumber.Size = new System.Drawing.Size(66, 19);
            this.TSL_PageNumber.Text = "Page: 0 / 0";
            // 
            // TSL_Status
            // 
            this.TSL_Status.Name = "TSL_Status";
            this.TSL_Status.Size = new System.Drawing.Size(81, 19);
            this.TSL_Status.Text = "No file loaded";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(9, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(556, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Open,
            this.MI_Save,
            this.MI_SaveAs,
            this.toolStripSeparator1,
            this.MI_Import,
            this.MI_Export,
            this.exportAllMessagesToolStripMenuItem,
            this.toolStripSeparator2,
            this.M_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // MI_Open
            // 
            this.MI_Open.Image = ((System.Drawing.Image)(resources.GetObject("MI_Open.Image")));
            this.MI_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MI_Open.Name = "MI_Open";
            this.MI_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MI_Open.Size = new System.Drawing.Size(196, 22);
            this.MI_Open.Text = "&Open";
            this.MI_Open.Click += new System.EventHandler(this.MI_Open_Click);
            // 
            // MI_Save
            // 
            this.MI_Save.Image = ((System.Drawing.Image)(resources.GetObject("MI_Save.Image")));
            this.MI_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MI_Save.Name = "MI_Save";
            this.MI_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MI_Save.Size = new System.Drawing.Size(196, 22);
            this.MI_Save.Text = "&Save";
            this.MI_Save.Click += new System.EventHandler(this.MI_Save_Click);
            // 
            // MI_SaveAs
            // 
            this.MI_SaveAs.Name = "MI_SaveAs";
            this.MI_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MI_SaveAs.Size = new System.Drawing.Size(196, 22);
            this.MI_SaveAs.Text = "Save &As";
            this.MI_SaveAs.Click += new System.EventHandler(this.MI_SaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // MI_Import
            // 
            this.MI_Import.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MI_Import.Name = "MI_Import";
            this.MI_Import.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.MI_Import.Size = new System.Drawing.Size(196, 22);
            this.MI_Import.Text = "&Import";
            this.MI_Import.Click += new System.EventHandler(this.MI_Import_Click);
            // 
            // MI_Export
            // 
            this.MI_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MI_Export.Name = "MI_Export";
            this.MI_Export.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.MI_Export.Size = new System.Drawing.Size(196, 22);
            this.MI_Export.Text = "Ex&port";
            this.MI_Export.Click += new System.EventHandler(this.MI_Export_Click);
            // 
            // exportAllMessagesToolStripMenuItem
            // 
            this.exportAllMessagesToolStripMenuItem.Name = "exportAllMessagesToolStripMenuItem";
            this.exportAllMessagesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.exportAllMessagesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.exportAllMessagesToolStripMenuItem.Text = "Expo&rt All";
            this.exportAllMessagesToolStripMenuItem.Click += new System.EventHandler(this.exportAllMessagesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // M_Exit
            // 
            this.M_Exit.Name = "M_Exit";
            this.M_Exit.Size = new System.Drawing.Size(196, 22);
            this.M_Exit.Text = "E&xit";
            this.M_Exit.Click += new System.EventHandler(this.M_Exit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_EditLine,
            this.MI_Message,
            this.toolStripSeparator4,
            this.MI_SpellCheck});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // MI_EditLine
            // 
            this.MI_EditLine.Name = "MI_EditLine";
            this.MI_EditLine.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.MI_EditLine.Size = new System.Drawing.Size(213, 22);
            this.MI_EditLine.Text = "Edit Raw &Line";
            this.MI_EditLine.Click += new System.EventHandler(this.MI_EditLine_Click);
            // 
            // MI_Message
            // 
            this.MI_Message.Name = "MI_Message";
            this.MI_Message.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.MI_Message.Size = new System.Drawing.Size(213, 22);
            this.MI_Message.Text = "Edit Raw &Message";
            this.MI_Message.Click += new System.EventHandler(this.MI_Message_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(210, 6);
            // 
            // MI_SpellCheck
            // 
            this.MI_SpellCheck.Checked = true;
            this.MI_SpellCheck.CheckOnClick = true;
            this.MI_SpellCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MI_SpellCheck.Name = "MI_SpellCheck";
            this.MI_SpellCheck.Size = new System.Drawing.Size(213, 22);
            this.MI_SpellCheck.Text = "Spell Check";
            this.MI_SpellCheck.CheckedChanged += new System.EventHandler(this.MI_SpellCheck_CheckedChanged);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_TBStyles,
            this.MI_PlayerGender,
            this.toolStripSeparator3,
            this.MI_EnableBackgrounds});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // MI_TBStyles
            // 
            this.MI_TBStyles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_TBStandard,
            this.MI_TBNohr,
            this.MI_TBHoshido});
            this.MI_TBStyles.Name = "MI_TBStyles";
            this.MI_TBStyles.Size = new System.Drawing.Size(181, 22);
            this.MI_TBStyles.Text = "Textbox Style";
            // 
            // MI_TBStandard
            // 
            this.MI_TBStandard.Checked = true;
            this.MI_TBStandard.CheckOnClick = true;
            this.MI_TBStandard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MI_TBStandard.Name = "MI_TBStandard";
            this.MI_TBStandard.Size = new System.Drawing.Size(121, 22);
            this.MI_TBStandard.Text = "Standard";
            this.MI_TBStandard.CheckedChanged += new System.EventHandler(this.MI_TBItem_CheckedChanged);
            this.MI_TBStandard.Click += new System.EventHandler(this.MI_CheckableItem_Click);
            // 
            // MI_TBNohr
            // 
            this.MI_TBNohr.CheckOnClick = true;
            this.MI_TBNohr.Name = "MI_TBNohr";
            this.MI_TBNohr.Size = new System.Drawing.Size(121, 22);
            this.MI_TBNohr.Text = "Nohr";
            this.MI_TBNohr.CheckedChanged += new System.EventHandler(this.MI_TBItem_CheckedChanged);
            this.MI_TBNohr.Click += new System.EventHandler(this.MI_CheckableItem_Click);
            // 
            // MI_TBHoshido
            // 
            this.MI_TBHoshido.CheckOnClick = true;
            this.MI_TBHoshido.Name = "MI_TBHoshido";
            this.MI_TBHoshido.Size = new System.Drawing.Size(121, 22);
            this.MI_TBHoshido.Text = "Hoshido";
            this.MI_TBHoshido.CheckedChanged += new System.EventHandler(this.MI_TBItem_CheckedChanged);
            this.MI_TBHoshido.Click += new System.EventHandler(this.MI_CheckableItem_Click);
            // 
            // MI_PlayerGender
            // 
            this.MI_PlayerGender.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_PlayerMale,
            this.MI_PlayerFemale});
            this.MI_PlayerGender.Name = "MI_PlayerGender";
            this.MI_PlayerGender.Size = new System.Drawing.Size(181, 22);
            this.MI_PlayerGender.Text = "Player Gender";
            // 
            // MI_PlayerMale
            // 
            this.MI_PlayerMale.Checked = true;
            this.MI_PlayerMale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MI_PlayerMale.Name = "MI_PlayerMale";
            this.MI_PlayerMale.Size = new System.Drawing.Size(112, 22);
            this.MI_PlayerMale.Text = "Male";
            this.MI_PlayerMale.CheckedChanged += new System.EventHandler(this.MI_PlayerGender_CheckedChanged);
            this.MI_PlayerMale.Click += new System.EventHandler(this.MI_CheckableItem_Click);
            // 
            // MI_PlayerFemale
            // 
            this.MI_PlayerFemale.Name = "MI_PlayerFemale";
            this.MI_PlayerFemale.Size = new System.Drawing.Size(112, 22);
            this.MI_PlayerFemale.Text = "Female";
            this.MI_PlayerFemale.CheckedChanged += new System.EventHandler(this.MI_PlayerGender_CheckedChanged);
            this.MI_PlayerFemale.Click += new System.EventHandler(this.MI_CheckableItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(178, 6);
            // 
            // MI_EnableBackgrounds
            // 
            this.MI_EnableBackgrounds.CheckOnClick = true;
            this.MI_EnableBackgrounds.Name = "MI_EnableBackgrounds";
            this.MI_EnableBackgrounds.Size = new System.Drawing.Size(181, 22);
            this.MI_EnableBackgrounds.Text = "Enable &Backgrounds";
            this.MI_EnableBackgrounds.CheckedChanged += new System.EventHandler(this.MI_EnableBackgrounds_CheckedChanged);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_HalfBox,
            this.MI_CompareMode});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // MI_HalfBox
            // 
            this.MI_HalfBox.Name = "MI_HalfBox";
            this.MI_HalfBox.Size = new System.Drawing.Size(173, 22);
            this.MI_HalfBox.Text = "&Half-Box Editor";
            this.MI_HalfBox.Click += new System.EventHandler(this.MI_HalfBox_Click);
            // 
            // MI_CompareMode
            // 
            this.MI_CompareMode.Name = "MI_CompareMode";
            this.MI_CompareMode.Size = new System.Drawing.Size(173, 22);
            this.MI_CompareMode.Text = "Comparison Mode";
            this.MI_CompareMode.Click += new System.EventHandler(this.MI_CompareMode_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MI_Reminder});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // MI_Reminder
            // 
            this.MI_Reminder.Name = "MI_Reminder";
            this.MI_Reminder.Size = new System.Drawing.Size(170, 22);
            this.MI_Reminder.Text = "Friendly &Reminder";
            this.MI_Reminder.Click += new System.EventHandler(this.MI_Reminder_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(9, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LB_MessageList);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TB_CurrentLine);
            this.splitContainer1.Panel2.Controls.Add(this.TB_PlayerName);
            this.splitContainer1.Panel2.Controls.Add(this.LBL_PlayerName);
            this.splitContainer1.Panel2.Controls.Add(this.B_PrevLine);
            this.splitContainer1.Panel2.Controls.Add(this.TB_CurrentPage);
            this.splitContainer1.Panel2.Controls.Add(this.B_NextLine);
            this.splitContainer1.Panel2.Controls.Add(this.PB_PreviewBox);
            this.splitContainer1.Panel2MinSize = 400;
            this.splitContainer1.Size = new System.Drawing.Size(556, 386);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 2;
            // 
            // LB_MessageList
            // 
            this.LB_MessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LB_MessageList.FormattingEnabled = true;
            this.LB_MessageList.Location = new System.Drawing.Point(0, 0);
            this.LB_MessageList.Name = "LB_MessageList";
            this.LB_MessageList.Size = new System.Drawing.Size(150, 386);
            this.LB_MessageList.TabIndex = 0;
            this.LB_MessageList.SelectedIndexChanged += new System.EventHandler(this.LB_MessageList_SelectedIndexChanged);
            this.LB_MessageList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LB_MessageList_MouseDown);
            // 
            // TB_PlayerName
            // 
            this.TB_PlayerName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TB_PlayerName.Location = new System.Drawing.Point(79, 353);
            this.TB_PlayerName.Name = "TB_PlayerName";
            this.TB_PlayerName.Size = new System.Drawing.Size(100, 20);
            this.TB_PlayerName.TabIndex = 6;
            this.TB_PlayerName.TextChanged += new System.EventHandler(this.TB_PlayerName_TextChanged);
            // 
            // LBL_PlayerName
            // 
            this.LBL_PlayerName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LBL_PlayerName.AutoSize = true;
            this.LBL_PlayerName.Location = new System.Drawing.Point(3, 356);
            this.LBL_PlayerName.Name = "LBL_PlayerName";
            this.LBL_PlayerName.Size = new System.Drawing.Size(70, 13);
            this.LBL_PlayerName.TabIndex = 5;
            this.LBL_PlayerName.Text = "Player Name:";
            this.LBL_PlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // B_PrevLine
            // 
            this.B_PrevLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.B_PrevLine.Enabled = false;
            this.B_PrevLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_PrevLine.Location = new System.Drawing.Point(259, 351);
            this.B_PrevLine.Name = "B_PrevLine";
            this.B_PrevLine.Size = new System.Drawing.Size(50, 23);
            this.B_PrevLine.TabIndex = 2;
            this.B_PrevLine.Text = "<---";
            this.B_PrevLine.UseVisualStyleBackColor = true;
            this.B_PrevLine.Click += new System.EventHandler(this.B_PrevLine_Click);
            // 
            // TB_CurrentPage
            // 
            this.TB_CurrentPage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TB_CurrentPage.Location = new System.Drawing.Point(315, 353);
            this.TB_CurrentPage.MaxLength = 3;
            this.TB_CurrentPage.Name = "TB_CurrentPage";
            this.TB_CurrentPage.Size = new System.Drawing.Size(24, 20);
            this.TB_CurrentPage.TabIndex = 4;
            this.TB_CurrentPage.Text = "0";
            this.TB_CurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TB_CurrentPage.WordWrap = false;
            this.TB_CurrentPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_CurrentPage_KeyDown);
            // 
            // B_NextLine
            // 
            this.B_NextLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.B_NextLine.Enabled = false;
            this.B_NextLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_NextLine.Location = new System.Drawing.Point(345, 351);
            this.B_NextLine.Name = "B_NextLine";
            this.B_NextLine.Size = new System.Drawing.Size(50, 23);
            this.B_NextLine.TabIndex = 3;
            this.B_NextLine.Text = "--->";
            this.B_NextLine.UseVisualStyleBackColor = true;
            this.B_NextLine.Click += new System.EventHandler(this.B_NextLine_Click);
            // 
            // PB_PreviewBox
            // 
            this.PB_PreviewBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PB_PreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PB_PreviewBox.Location = new System.Drawing.Point(-1, 0);
            this.PB_PreviewBox.MaximumSize = new System.Drawing.Size(400, 240);
            this.PB_PreviewBox.MinimumSize = new System.Drawing.Size(400, 240);
            this.PB_PreviewBox.Name = "PB_PreviewBox";
            this.PB_PreviewBox.Size = new System.Drawing.Size(400, 240);
            this.PB_PreviewBox.TabIndex = 7;
            this.PB_PreviewBox.TabStop = false;
            this.PB_PreviewBox.Click += new System.EventHandler(this.PB_PreviewBox_Click);
            this.PB_PreviewBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.PB_PreviewBox_DragDrop);
            this.PB_PreviewBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.PB_PreviewBox_DragEnter);
            // 
            // TB_CurrentLine
            // 
            this.TB_CurrentLine.AllowDrop = true;
            this.TB_CurrentLine.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TB_CurrentLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_CurrentLine.Location = new System.Drawing.Point(-1, 246);
            this.TB_CurrentLine.Multiline = true;
            this.TB_CurrentLine.Name = "TB_CurrentLine";
            this.TB_CurrentLine.Size = new System.Drawing.Size(400, 99);
            this.TB_CurrentLine.TabIndex = 12;
            this.TB_CurrentLine.ChildChanged += new System.EventHandler<System.Windows.Forms.Integration.ChildChangedEventArgs>(this.TB_CurrentLine_ChildChanged);
            this.TB_CurrentLine.Child = null;
            // 
            // CompactMainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 443);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(590, 482);
            this.Name = "CompactMainForm";
            this.Padding = new System.Windows.Forms.Padding(9, 0, 9, 9);
            this.Text = "FEITS Exporter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompactMainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompactMainForm_KeyDown_1);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PB_PreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TSL_PageNumber;
        private System.Windows.Forms.ToolStripStatusLabel TSL_Status;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_Open;
        private System.Windows.Forms.ToolStripMenuItem MI_Save;
        private System.Windows.Forms.ToolStripMenuItem MI_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MI_Import;
        private System.Windows.Forms.ToolStripMenuItem MI_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem M_Exit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_EditLine;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_HalfBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_Reminder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox LB_MessageList;
        private System.Windows.Forms.TextBox TB_PlayerName;
        private System.Windows.Forms.Label LBL_PlayerName;
        private System.Windows.Forms.Button B_PrevLine;
        private System.Windows.Forms.TextBox TB_CurrentPage;
        private System.Windows.Forms.Button B_NextLine;
        private System.Windows.Forms.PictureBox PB_PreviewBox;
        private System.Windows.Forms.ToolStripMenuItem MI_Message;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MI_TBStyles;
        private System.Windows.Forms.ToolStripMenuItem MI_TBStandard;
        private System.Windows.Forms.ToolStripMenuItem MI_TBNohr;
        private System.Windows.Forms.ToolStripMenuItem MI_TBHoshido;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MI_EnableBackgrounds;
        private System.Windows.Forms.ToolStripMenuItem exportAllMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MI_SpellCheck;
        private System.Windows.Forms.ToolStripMenuItem MI_PlayerGender;
        private System.Windows.Forms.ToolStripMenuItem MI_PlayerMale;
        private System.Windows.Forms.ToolStripMenuItem MI_PlayerFemale;
        private System.Windows.Forms.ToolStripMenuItem MI_CompareMode;
        private SpellBox TB_CurrentLine;
    }
}