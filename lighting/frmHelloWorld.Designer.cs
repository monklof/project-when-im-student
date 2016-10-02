namespace HelloWorld
{
	partial class frmHelloWorld
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelloWorld));
            this.imgProfile = new System.Windows.Forms.PictureBox();
            this.lblScreenName = new System.Windows.Forms.Label();
            this.txtStatusBody = new System.Windows.Forms.TextBox();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.btnInsertPicture = new System.Windows.Forms.Button();
            this.btnPublish = new System.Windows.Forms.Button();
            this.lblCharCount = new System.Windows.Forms.Label();
            this.wbStatuses = new System.Windows.Forms.WebBrowser();
            this.lblDesc = new System.Windows.Forms.Label();
            this.groupBox_serialSet = new System.Windows.Forms.GroupBox();
            this.panel_state = new System.Windows.Forms.Panel();
            this.button_serialOpen = new System.Windows.Forms.Button();
            this.comboBox_Bault = new System.Windows.Forms.ComboBox();
            this.comboBox_stopBit = new System.Windows.Forms.ComboBox();
            this.comboBox_statusBit = new System.Windows.Forms.ComboBox();
            this.comboBox_checkBit = new System.Windows.Forms.ComboBox();
            this.comboBox_serialName = new System.Windows.Forms.ComboBox();
            this.label_stopBit = new System.Windows.Forms.Label();
            this.label_statusBit = new System.Windows.Forms.Label();
            this.label_checkBit = new System.Windows.Forms.Label();
            this.label_Bault = new System.Windows.Forms.Label();
            this.label_serialName = new System.Windows.Forms.Label();
            this.groupBox_sendContent = new System.Windows.Forms.GroupBox();
            this.textBox_sendContent = new System.Windows.Forms.TextBox();
            this.groupBox_sendType = new System.Windows.Forms.GroupBox();
            this.radioButton_receiveInChar = new System.Windows.Forms.RadioButton();
            this.radioButton_receiveInHex = new System.Windows.Forms.RadioButton();
            this.groupBox_receiveBuffer = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl01 = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox_receiveBuffer = new System.Windows.Forms.TextBox();
            this.groupBox_oprandSet = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox_sendRound = new System.Windows.Forms.CheckBox();
            this.button_save = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.button_clearReceive = new System.Windows.Forms.Button();
            this.button_clearSend = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btn_refresh = new System.Windows.Forms.Button();
            this.numericUpDown_Interval = new System.Windows.Forms.NumericUpDown();
            this.btn_receiveTui = new System.Windows.Forms.Button();
            this.groupBox_interaction = new System.Windows.Forms.GroupBox();
            this.textBox_EndCh = new System.Windows.Forms.TextBox();
            this.textBox_BeginCh = new System.Windows.Forms.TextBox();
            this.numericUpDown_Ctrl = new System.Windows.Forms.NumericUpDown();
            this.btn_SendCtrl = new System.Windows.Forms.Button();
            this.label_EndCh = new System.Windows.Forms.Label();
            this.label_BeginCh = new System.Windows.Forms.Label();
            this.label_Ctrl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMI_View = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_View_SystemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_View_WBLog = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_View_SysSet = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_View_Convert = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_RefreshMyWB = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgProfile)).BeginInit();
            this.groupBox_serialSet.SuspendLayout();
            this.groupBox_sendContent.SuspendLayout();
            this.groupBox_sendType.SuspendLayout();
            this.groupBox_receiveBuffer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox_oprandSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).BeginInit();
            this.groupBox_interaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ctrl)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgProfile
            // 
            this.imgProfile.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.imgProfile, "imgProfile");
            this.imgProfile.Name = "imgProfile";
            this.imgProfile.TabStop = false;
            // 
            // lblScreenName
            // 
            resources.ApplyResources(this.lblScreenName, "lblScreenName");
            this.lblScreenName.BackColor = System.Drawing.Color.Transparent;
            this.lblScreenName.Name = "lblScreenName";
            // 
            // txtStatusBody
            // 
            resources.ApplyResources(this.txtStatusBody, "txtStatusBody");
            this.txtStatusBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatusBody.Name = "txtStatusBody";
            this.txtStatusBody.TextChanged += new System.EventHandler(this.txtStatusBody_TextChanged);
            // 
            // lblUserStatus
            // 
            resources.ApplyResources(this.lblUserStatus, "lblUserStatus");
            this.lblUserStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblUserStatus.Name = "lblUserStatus";
            // 
            // btnInsertPicture
            // 
            resources.ApplyResources(this.btnInsertPicture, "btnInsertPicture");
            this.btnInsertPicture.Name = "btnInsertPicture";
            this.btnInsertPicture.TabStop = false;
            this.btnInsertPicture.UseVisualStyleBackColor = true;
            this.btnInsertPicture.Click += new System.EventHandler(this.btnInsertPicture_Click);
            // 
            // btnPublish
            // 
            resources.ApplyResources(this.btnPublish, "btnPublish");
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.UseVisualStyleBackColor = true;
            this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
            // 
            // lblCharCount
            // 
            resources.ApplyResources(this.lblCharCount, "lblCharCount");
            this.lblCharCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCharCount.ForeColor = System.Drawing.Color.White;
            this.lblCharCount.Name = "lblCharCount";
            // 
            // wbStatuses
            // 
            this.wbStatuses.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.wbStatuses, "wbStatuses");
            this.wbStatuses.IsWebBrowserContextMenuEnabled = false;
            this.wbStatuses.Name = "wbStatuses";
            this.wbStatuses.Url = new System.Uri("", System.UriKind.Relative);
            this.wbStatuses.WebBrowserShortcutsEnabled = false;
            // 
            // lblDesc
            // 
            resources.ApplyResources(this.lblDesc, "lblDesc");
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblDesc.Name = "lblDesc";
            // 
            // groupBox_serialSet
            // 
            resources.ApplyResources(this.groupBox_serialSet, "groupBox_serialSet");
            this.groupBox_serialSet.Controls.Add(this.panel_state);
            this.groupBox_serialSet.Controls.Add(this.button_serialOpen);
            this.groupBox_serialSet.Controls.Add(this.comboBox_Bault);
            this.groupBox_serialSet.Controls.Add(this.comboBox_stopBit);
            this.groupBox_serialSet.Controls.Add(this.comboBox_statusBit);
            this.groupBox_serialSet.Controls.Add(this.comboBox_checkBit);
            this.groupBox_serialSet.Controls.Add(this.comboBox_serialName);
            this.groupBox_serialSet.Controls.Add(this.label_stopBit);
            this.groupBox_serialSet.Controls.Add(this.label_statusBit);
            this.groupBox_serialSet.Controls.Add(this.label_checkBit);
            this.groupBox_serialSet.Controls.Add(this.label_Bault);
            this.groupBox_serialSet.Controls.Add(this.label_serialName);
            this.groupBox_serialSet.Name = "groupBox_serialSet";
            this.groupBox_serialSet.TabStop = false;
            // 
            // panel_state
            // 
            this.panel_state.BackColor = System.Drawing.Color.Red;
            this.panel_state.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.panel_state, "panel_state");
            this.panel_state.Name = "panel_state";
            // 
            // button_serialOpen
            // 
            resources.ApplyResources(this.button_serialOpen, "button_serialOpen");
            this.button_serialOpen.Name = "button_serialOpen";
            this.button_serialOpen.UseVisualStyleBackColor = true;
            this.button_serialOpen.Click += new System.EventHandler(this.button_serialOpen_Click);
            // 
            // comboBox_Bault
            // 
            this.comboBox_Bault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bault.FormattingEnabled = true;
            this.comboBox_Bault.Items.AddRange(new object[] {
            resources.GetString("comboBox_Bault.Items"),
            resources.GetString("comboBox_Bault.Items1"),
            resources.GetString("comboBox_Bault.Items2"),
            resources.GetString("comboBox_Bault.Items3"),
            resources.GetString("comboBox_Bault.Items4"),
            resources.GetString("comboBox_Bault.Items5"),
            resources.GetString("comboBox_Bault.Items6"),
            resources.GetString("comboBox_Bault.Items7"),
            resources.GetString("comboBox_Bault.Items8"),
            resources.GetString("comboBox_Bault.Items9"),
            resources.GetString("comboBox_Bault.Items10"),
            resources.GetString("comboBox_Bault.Items11"),
            resources.GetString("comboBox_Bault.Items12"),
            resources.GetString("comboBox_Bault.Items13"),
            resources.GetString("comboBox_Bault.Items14"),
            resources.GetString("comboBox_Bault.Items15")});
            resources.ApplyResources(this.comboBox_Bault, "comboBox_Bault");
            this.comboBox_Bault.Name = "comboBox_Bault";
            this.comboBox_Bault.SelectedIndexChanged += new System.EventHandler(this.comboBox_Bault_SelectedIndexChanged);
            // 
            // comboBox_stopBit
            // 
            this.comboBox_stopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_stopBit.FormattingEnabled = true;
            this.comboBox_stopBit.Items.AddRange(new object[] {
            resources.GetString("comboBox_stopBit.Items"),
            resources.GetString("comboBox_stopBit.Items1"),
            resources.GetString("comboBox_stopBit.Items2")});
            resources.ApplyResources(this.comboBox_stopBit, "comboBox_stopBit");
            this.comboBox_stopBit.Name = "comboBox_stopBit";
            this.comboBox_stopBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_stopBit_SelectedIndexChanged);
            // 
            // comboBox_statusBit
            // 
            this.comboBox_statusBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_statusBit.FormattingEnabled = true;
            this.comboBox_statusBit.Items.AddRange(new object[] {
            resources.GetString("comboBox_statusBit.Items"),
            resources.GetString("comboBox_statusBit.Items1"),
            resources.GetString("comboBox_statusBit.Items2"),
            resources.GetString("comboBox_statusBit.Items3")});
            resources.ApplyResources(this.comboBox_statusBit, "comboBox_statusBit");
            this.comboBox_statusBit.Name = "comboBox_statusBit";
            this.comboBox_statusBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_statusBit_SelectedIndexChanged);
            // 
            // comboBox_checkBit
            // 
            this.comboBox_checkBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_checkBit.FormattingEnabled = true;
            this.comboBox_checkBit.Items.AddRange(new object[] {
            resources.GetString("comboBox_checkBit.Items"),
            resources.GetString("comboBox_checkBit.Items1"),
            resources.GetString("comboBox_checkBit.Items2"),
            resources.GetString("comboBox_checkBit.Items3"),
            resources.GetString("comboBox_checkBit.Items4")});
            resources.ApplyResources(this.comboBox_checkBit, "comboBox_checkBit");
            this.comboBox_checkBit.Name = "comboBox_checkBit";
            this.comboBox_checkBit.SelectedIndexChanged += new System.EventHandler(this.comboBox_checkBit_SelectedIndexChanged);
            // 
            // comboBox_serialName
            // 
            this.comboBox_serialName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_serialName.FormattingEnabled = true;
            this.comboBox_serialName.Items.AddRange(new object[] {
            resources.GetString("comboBox_serialName.Items"),
            resources.GetString("comboBox_serialName.Items1"),
            resources.GetString("comboBox_serialName.Items2"),
            resources.GetString("comboBox_serialName.Items3"),
            resources.GetString("comboBox_serialName.Items4"),
            resources.GetString("comboBox_serialName.Items5"),
            resources.GetString("comboBox_serialName.Items6"),
            resources.GetString("comboBox_serialName.Items7"),
            resources.GetString("comboBox_serialName.Items8"),
            resources.GetString("comboBox_serialName.Items9"),
            resources.GetString("comboBox_serialName.Items10"),
            resources.GetString("comboBox_serialName.Items11"),
            resources.GetString("comboBox_serialName.Items12"),
            resources.GetString("comboBox_serialName.Items13"),
            resources.GetString("comboBox_serialName.Items14"),
            resources.GetString("comboBox_serialName.Items15")});
            resources.ApplyResources(this.comboBox_serialName, "comboBox_serialName");
            this.comboBox_serialName.Name = "comboBox_serialName";
            this.comboBox_serialName.SelectedIndexChanged += new System.EventHandler(this.comboBox_serialName_SelectedIndexChanged);
            // 
            // label_stopBit
            // 
            resources.ApplyResources(this.label_stopBit, "label_stopBit");
            this.label_stopBit.BackColor = System.Drawing.Color.PowderBlue;
            this.label_stopBit.Name = "label_stopBit";
            // 
            // label_statusBit
            // 
            resources.ApplyResources(this.label_statusBit, "label_statusBit");
            this.label_statusBit.BackColor = System.Drawing.Color.PowderBlue;
            this.label_statusBit.Name = "label_statusBit";
            // 
            // label_checkBit
            // 
            resources.ApplyResources(this.label_checkBit, "label_checkBit");
            this.label_checkBit.BackColor = System.Drawing.Color.PowderBlue;
            this.label_checkBit.Name = "label_checkBit";
            // 
            // label_Bault
            // 
            resources.ApplyResources(this.label_Bault, "label_Bault");
            this.label_Bault.BackColor = System.Drawing.Color.PowderBlue;
            this.label_Bault.Name = "label_Bault";
            // 
            // label_serialName
            // 
            resources.ApplyResources(this.label_serialName, "label_serialName");
            this.label_serialName.BackColor = System.Drawing.Color.PowderBlue;
            this.label_serialName.Name = "label_serialName";
            // 
            // groupBox_sendContent
            // 
            resources.ApplyResources(this.groupBox_sendContent, "groupBox_sendContent");
            this.groupBox_sendContent.Controls.Add(this.textBox_sendContent);
            this.groupBox_sendContent.Name = "groupBox_sendContent";
            this.groupBox_sendContent.TabStop = false;
            // 
            // textBox_sendContent
            // 
            resources.ApplyResources(this.textBox_sendContent, "textBox_sendContent");
            this.textBox_sendContent.Name = "textBox_sendContent";
            this.textBox_sendContent.TextChanged += new System.EventHandler(this.textBox_sendContent_TextChanged);
            // 
            // groupBox_sendType
            // 
            resources.ApplyResources(this.groupBox_sendType, "groupBox_sendType");
            this.groupBox_sendType.Controls.Add(this.radioButton_receiveInChar);
            this.groupBox_sendType.Controls.Add(this.radioButton_receiveInHex);
            this.groupBox_sendType.Name = "groupBox_sendType";
            this.groupBox_sendType.TabStop = false;
            // 
            // radioButton_receiveInChar
            // 
            resources.ApplyResources(this.radioButton_receiveInChar, "radioButton_receiveInChar");
            this.radioButton_receiveInChar.BackColor = System.Drawing.Color.PowderBlue;
            this.radioButton_receiveInChar.Name = "radioButton_receiveInChar";
            this.radioButton_receiveInChar.UseVisualStyleBackColor = false;
            this.radioButton_receiveInChar.CheckedChanged += new System.EventHandler(this.radioButton_receiveInChar_CheckedChanged);
            // 
            // radioButton_receiveInHex
            // 
            resources.ApplyResources(this.radioButton_receiveInHex, "radioButton_receiveInHex");
            this.radioButton_receiveInHex.BackColor = System.Drawing.Color.PowderBlue;
            this.radioButton_receiveInHex.Checked = true;
            this.radioButton_receiveInHex.Name = "radioButton_receiveInHex";
            this.radioButton_receiveInHex.TabStop = true;
            this.radioButton_receiveInHex.UseVisualStyleBackColor = false;
            this.radioButton_receiveInHex.CheckedChanged += new System.EventHandler(this.radioButton_receiveInHex_CheckedChanged);
            // 
            // groupBox_receiveBuffer
            // 
            resources.ApplyResources(this.groupBox_receiveBuffer, "groupBox_receiveBuffer");
            this.groupBox_receiveBuffer.Controls.Add(this.statusStrip1);
            this.groupBox_receiveBuffer.Controls.Add(this.textBox_receiveBuffer);
            this.groupBox_receiveBuffer.Name = "groupBox_receiveBuffer";
            this.groupBox_receiveBuffer.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.PowderBlue;
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl01});
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.SizingGrip = false;
            // 
            // tssl01
            // 
            this.tssl01.Name = "tssl01";
            resources.ApplyResources(this.tssl01, "tssl01");
            // 
            // textBox_receiveBuffer
            // 
            this.textBox_receiveBuffer.BackColor = System.Drawing.Color.Black;
            this.textBox_receiveBuffer.ForeColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.textBox_receiveBuffer, "textBox_receiveBuffer");
            this.textBox_receiveBuffer.Name = "textBox_receiveBuffer";
            this.textBox_receiveBuffer.ReadOnly = true;
            // 
            // groupBox_oprandSet
            // 
            resources.ApplyResources(this.groupBox_oprandSet, "groupBox_oprandSet");
            this.groupBox_oprandSet.Controls.Add(this.numericUpDown1);
            this.groupBox_oprandSet.Controls.Add(this.checkBox_sendRound);
            this.groupBox_oprandSet.Controls.Add(this.button_save);
            this.groupBox_oprandSet.Controls.Add(this.button_send);
            this.groupBox_oprandSet.Controls.Add(this.button_clearReceive);
            this.groupBox_oprandSet.Controls.Add(this.button_clearSend);
            this.groupBox_oprandSet.Name = "groupBox_oprandSet";
            this.groupBox_oprandSet.TabStop = false;
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // checkBox_sendRound
            // 
            resources.ApplyResources(this.checkBox_sendRound, "checkBox_sendRound");
            this.checkBox_sendRound.BackColor = System.Drawing.Color.PowderBlue;
            this.checkBox_sendRound.Name = "checkBox_sendRound";
            this.checkBox_sendRound.UseVisualStyleBackColor = false;
            this.checkBox_sendRound.CheckedChanged += new System.EventHandler(this.checkBox_sendRound_CheckedChanged);
            // 
            // button_save
            // 
            resources.ApplyResources(this.button_save, "button_save");
            this.button_save.Name = "button_save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_send
            // 
            resources.ApplyResources(this.button_send, "button_send");
            this.button_send.Name = "button_send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // button_clearReceive
            // 
            resources.ApplyResources(this.button_clearReceive, "button_clearReceive");
            this.button_clearReceive.Name = "button_clearReceive";
            this.button_clearReceive.UseVisualStyleBackColor = true;
            this.button_clearReceive.Click += new System.EventHandler(this.button_clearReceive_Click);
            // 
            // button_clearSend
            // 
            resources.ApplyResources(this.button_clearSend, "button_clearSend");
            this.button_clearSend.Name = "button_clearSend";
            this.button_clearSend.UseVisualStyleBackColor = true;
            this.button_clearSend.Click += new System.EventHandler(this.button_clearSend_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.DataReceived);
            // 
            // btn_refresh
            // 
            resources.ApplyResources(this.btn_refresh, "btn_refresh");
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // numericUpDown_Interval
            // 
            this.numericUpDown_Interval.DecimalPlaces = 1;
            this.numericUpDown_Interval.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDown_Interval, "numericUpDown_Interval");
            this.numericUpDown_Interval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDown_Interval.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_Interval.Name = "numericUpDown_Interval";
            this.numericUpDown_Interval.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_Interval.ValueChanged += new System.EventHandler(this.numericUpDown_Interval_ValueChanged);
            // 
            // btn_receiveTui
            // 
            resources.ApplyResources(this.btn_receiveTui, "btn_receiveTui");
            this.btn_receiveTui.Name = "btn_receiveTui";
            this.btn_receiveTui.UseVisualStyleBackColor = true;
            this.btn_receiveTui.Click += new System.EventHandler(this.btn_OpenTui_Click);
            // 
            // groupBox_interaction
            // 
            resources.ApplyResources(this.groupBox_interaction, "groupBox_interaction");
            this.groupBox_interaction.Controls.Add(this.textBox_EndCh);
            this.groupBox_interaction.Controls.Add(this.textBox_BeginCh);
            this.groupBox_interaction.Controls.Add(this.numericUpDown_Ctrl);
            this.groupBox_interaction.Controls.Add(this.numericUpDown_Interval);
            this.groupBox_interaction.Controls.Add(this.btn_SendCtrl);
            this.groupBox_interaction.Controls.Add(this.label_EndCh);
            this.groupBox_interaction.Controls.Add(this.label_BeginCh);
            this.groupBox_interaction.Controls.Add(this.label_Ctrl);
            this.groupBox_interaction.Controls.Add(this.label1);
            this.groupBox_interaction.Controls.Add(this.btn_receiveTui);
            this.groupBox_interaction.Name = "groupBox_interaction";
            this.groupBox_interaction.TabStop = false;
            // 
            // textBox_EndCh
            // 
            resources.ApplyResources(this.textBox_EndCh, "textBox_EndCh");
            this.textBox_EndCh.Name = "textBox_EndCh";
            this.textBox_EndCh.TextChanged += new System.EventHandler(this.textBox_EndCh_TextChanged);
            // 
            // textBox_BeginCh
            // 
            resources.ApplyResources(this.textBox_BeginCh, "textBox_BeginCh");
            this.textBox_BeginCh.Name = "textBox_BeginCh";
            this.textBox_BeginCh.TextChanged += new System.EventHandler(this.textBox_BeginCh_TextChanged);
            // 
            // numericUpDown_Ctrl
            // 
            this.numericUpDown_Ctrl.DecimalPlaces = 1;
            this.numericUpDown_Ctrl.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            resources.ApplyResources(this.numericUpDown_Ctrl, "numericUpDown_Ctrl");
            this.numericUpDown_Ctrl.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numericUpDown_Ctrl.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_Ctrl.Name = "numericUpDown_Ctrl";
            this.numericUpDown_Ctrl.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDown_Ctrl.ValueChanged += new System.EventHandler(this.numericUpDown_Ctrl_ValueChanged);
            // 
            // btn_SendCtrl
            // 
            resources.ApplyResources(this.btn_SendCtrl, "btn_SendCtrl");
            this.btn_SendCtrl.Name = "btn_SendCtrl";
            this.btn_SendCtrl.UseVisualStyleBackColor = true;
            this.btn_SendCtrl.Click += new System.EventHandler(this.btn_SendCtrl_Click);
            // 
            // label_EndCh
            // 
            resources.ApplyResources(this.label_EndCh, "label_EndCh");
            this.label_EndCh.BackColor = System.Drawing.Color.PowderBlue;
            this.label_EndCh.Name = "label_EndCh";
            // 
            // label_BeginCh
            // 
            resources.ApplyResources(this.label_BeginCh, "label_BeginCh");
            this.label_BeginCh.BackColor = System.Drawing.Color.PowderBlue;
            this.label_BeginCh.Name = "label_BeginCh";
            // 
            // label_Ctrl
            // 
            resources.ApplyResources(this.label_Ctrl, "label_Ctrl");
            this.label_Ctrl.BackColor = System.Drawing.Color.Transparent;
            this.label_Ctrl.Name = "label_Ctrl";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SlateGray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_View,
            this.TSMI_Help});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // TSMI_View
            // 
            this.TSMI_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_View_SystemLog,
            this.TSMI_View_WBLog,
            this.TSMI_View_SysSet,
            this.TSMI_View_Convert});
            this.TSMI_View.Name = "TSMI_View";
            resources.ApplyResources(this.TSMI_View, "TSMI_View");
            // 
            // TSMI_View_SystemLog
            // 
            this.TSMI_View_SystemLog.Name = "TSMI_View_SystemLog";
            resources.ApplyResources(this.TSMI_View_SystemLog, "TSMI_View_SystemLog");
            this.TSMI_View_SystemLog.Click += new System.EventHandler(this.TSMI_View_SystemLog_Click);
            // 
            // TSMI_View_WBLog
            // 
            this.TSMI_View_WBLog.Name = "TSMI_View_WBLog";
            resources.ApplyResources(this.TSMI_View_WBLog, "TSMI_View_WBLog");
            this.TSMI_View_WBLog.Click += new System.EventHandler(this.TSMI_View_WBLog_Click);
            // 
            // TSMI_View_SysSet
            // 
            this.TSMI_View_SysSet.Name = "TSMI_View_SysSet";
            resources.ApplyResources(this.TSMI_View_SysSet, "TSMI_View_SysSet");
            this.TSMI_View_SysSet.Click += new System.EventHandler(this.TSMI_View_SysSet_Click);
            // 
            // TSMI_View_Convert
            // 
            this.TSMI_View_Convert.Name = "TSMI_View_Convert";
            resources.ApplyResources(this.TSMI_View_Convert, "TSMI_View_Convert");
            this.TSMI_View_Convert.Click += new System.EventHandler(this.TSMI_View_Convert_Click);
            // 
            // TSMI_Help
            // 
            this.TSMI_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_Help_About});
            this.TSMI_Help.Name = "TSMI_Help";
            resources.ApplyResources(this.TSMI_Help, "TSMI_Help");
            // 
            // TSMI_Help_About
            // 
            this.TSMI_Help_About.Name = "TSMI_Help_About";
            resources.ApplyResources(this.TSMI_Help_About, "TSMI_Help_About");
            this.TSMI_Help_About.Click += new System.EventHandler(this.TSMI_Help_About_Click);
            // 
            // btn_RefreshMyWB
            // 
            resources.ApplyResources(this.btn_RefreshMyWB, "btn_RefreshMyWB");
            this.btn_RefreshMyWB.Name = "btn_RefreshMyWB";
            this.btn_RefreshMyWB.UseVisualStyleBackColor = true;
            this.btn_RefreshMyWB.Click += new System.EventHandler(this.btn_RefreshMyWB_Click);
            // 
            // frmHelloWorld
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btn_RefreshMyWB);
            this.Controls.Add(this.groupBox_interaction);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.groupBox_oprandSet);
            this.Controls.Add(this.groupBox_receiveBuffer);
            this.Controls.Add(this.groupBox_sendType);
            this.Controls.Add(this.groupBox_sendContent);
            this.Controls.Add(this.groupBox_serialSet);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.wbStatuses);
            this.Controls.Add(this.lblCharCount);
            this.Controls.Add(this.btnPublish);
            this.Controls.Add(this.btnInsertPicture);
            this.Controls.Add(this.lblUserStatus);
            this.Controls.Add(this.txtStatusBody);
            this.Controls.Add(this.lblScreenName);
            this.Controls.Add(this.imgProfile);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmHelloWorld";
            this.Load += new System.EventHandler(this.HelloWorld_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgProfile)).EndInit();
            this.groupBox_serialSet.ResumeLayout(false);
            this.groupBox_serialSet.PerformLayout();
            this.groupBox_sendContent.ResumeLayout(false);
            this.groupBox_sendContent.PerformLayout();
            this.groupBox_sendType.ResumeLayout(false);
            this.groupBox_sendType.PerformLayout();
            this.groupBox_receiveBuffer.ResumeLayout(false);
            this.groupBox_receiveBuffer.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox_oprandSet.ResumeLayout(false);
            this.groupBox_oprandSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Interval)).EndInit();
            this.groupBox_interaction.ResumeLayout(false);
            this.groupBox_interaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ctrl)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox imgProfile;
		private System.Windows.Forms.Label lblScreenName;
		private System.Windows.Forms.TextBox txtStatusBody;
		private System.Windows.Forms.Label lblUserStatus;
		private System.Windows.Forms.Button btnInsertPicture;
		private System.Windows.Forms.Button btnPublish;
		private System.Windows.Forms.Label lblCharCount;
		private System.Windows.Forms.WebBrowser wbStatuses;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.GroupBox groupBox_serialSet;
        public System.Windows.Forms.Button button_serialOpen;
        private System.Windows.Forms.Label label_stopBit;
        private System.Windows.Forms.Label label_statusBit;
        private System.Windows.Forms.Label label_checkBit;
        private System.Windows.Forms.Label label_Bault;
        private System.Windows.Forms.Label label_serialName;
        public System.Windows.Forms.TextBox textBox_sendContent;
        public System.Windows.Forms.RadioButton radioButton_receiveInChar;
        public System.Windows.Forms.RadioButton radioButton_receiveInHex;
        private System.Windows.Forms.GroupBox groupBox_receiveBuffer;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.CheckBox checkBox_sendRound;
        public System.Windows.Forms.Button button_send;
        public System.Windows.Forms.ToolStripStatusLabel tssl01;
        public System.Windows.Forms.Timer timer1;
        public System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btn_refresh;
        public System.Windows.Forms.NumericUpDown numericUpDown_Interval;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox comboBox_serialName;
        internal System.Windows.Forms.ComboBox comboBox_Bault;
        internal System.Windows.Forms.ComboBox comboBox_stopBit;
        internal System.Windows.Forms.ComboBox comboBox_statusBit;
        internal System.Windows.Forms.ComboBox comboBox_checkBit;
        internal System.Windows.Forms.GroupBox groupBox_sendContent;
        internal System.Windows.Forms.GroupBox groupBox_sendType;
        internal System.Windows.Forms.GroupBox groupBox_oprandSet;
        internal System.Windows.Forms.Button button_save;
        internal System.Windows.Forms.Button button_clearReceive;
        internal System.Windows.Forms.Button button_clearSend;
        internal System.Windows.Forms.Button btn_receiveTui;
        internal System.Windows.Forms.GroupBox groupBox_interaction;
        internal System.Windows.Forms.Panel panel_state;
        internal System.Windows.Forms.StatusStrip statusStrip1;
        internal System.Windows.Forms.TextBox textBox_receiveBuffer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_View;
        private System.Windows.Forms.ToolStripMenuItem TSMI_View_SystemLog;
        private System.Windows.Forms.ToolStripMenuItem TSMI_View_WBLog;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Help;
        private System.Windows.Forms.ToolStripMenuItem TSMI_Help_About;
        private System.Windows.Forms.Label label_EndCh;
        private System.Windows.Forms.Label label_BeginCh;
        private System.Windows.Forms.TextBox textBox_EndCh;
        private System.Windows.Forms.TextBox textBox_BeginCh;
        internal System.Windows.Forms.Button btn_SendCtrl;
        private System.Windows.Forms.Label label_Ctrl;
        public System.Windows.Forms.NumericUpDown numericUpDown_Ctrl;
        private System.Windows.Forms.Button btn_RefreshMyWB;
        private System.Windows.Forms.ToolStripMenuItem TSMI_View_SysSet;
        private System.Windows.Forms.ToolStripMenuItem TSMI_View_Convert;

        public System.IO.Ports.SerialDataReceivedEventHandler DavaReceived { get; set; }
    }
}