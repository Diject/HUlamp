namespace HUlamp
{
    partial class MainForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Отметить все");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.comportBox = new System.Windows.Forms.ComboBox();
            this.comportConnectB = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.comportReloadButton = new System.Windows.Forms.Button();
            this.commandGroup = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxParam9 = new System.Windows.Forms.TextBox();
            this.textboxParam8 = new System.Windows.Forms.TextBox();
            this.textboxParam7 = new System.Windows.Forms.TextBox();
            this.textboxParam6 = new System.Windows.Forms.TextBox();
            this.textboxParam5 = new System.Windows.Forms.TextBox();
            this.textboxParam4 = new System.Windows.Forms.TextBox();
            this.textboxParam3 = new System.Windows.Forms.TextBox();
            this.textboxParam2 = new System.Windows.Forms.TextBox();
            this.textboxParam1 = new System.Windows.Forms.TextBox();
            this.commandSentB = new System.Windows.Forms.Button();
            this.commandList = new System.Windows.Forms.ComboBox();
            this.receiveTextBox = new System.Windows.Forms.RichTextBox();
            this.comportDisconnectB = new System.Windows.Forms.Button();
            this.VisualizationOnButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFastSettings = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nsChangeColorB = new System.Windows.Forms.Button();
            this.nsColorPanel = new System.Windows.Forms.Panel();
            this.nsSetModeB = new System.Windows.Forms.Button();
            this.nsModeRB5 = new System.Windows.Forms.RadioButton();
            this.nsModeRB4 = new System.Windows.Forms.RadioButton();
            this.nsModeRB3 = new System.Windows.Forms.RadioButton();
            this.nsModeRB2 = new System.Windows.Forms.RadioButton();
            this.nsModeRB1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.nsVisualizationVolumeTB = new System.Windows.Forms.TrackBar();
            this.nsVisualizationAutoVol = new System.Windows.Forms.CheckBox();
            this.nsVisualizationStartOn = new System.Windows.Forms.CheckBox();
            this.nsVisualizationOnCB = new System.Windows.Forms.CheckBox();
            this.tabCommands = new System.Windows.Forms.TabPage();
            this.commandInfoTextBox = new System.Windows.Forms.RichTextBox();
            this.tabVisualization = new System.Windows.Forms.TabPage();
            this.statLabD3 = new System.Windows.Forms.Label();
            this.statLabD2 = new System.Windows.Forms.Label();
            this.statLabD1 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxVolumeUpdateTim = new System.Windows.Forms.TextBox();
            this.statLab12 = new System.Windows.Forms.Label();
            this.statLab11 = new System.Windows.Forms.Label();
            this.statLab10 = new System.Windows.Forms.Label();
            this.statLab9 = new System.Windows.Forms.Label();
            this.statLab8 = new System.Windows.Forms.Label();
            this.statLab4 = new System.Windows.Forms.Label();
            this.statLab7 = new System.Windows.Forms.Label();
            this.statLab6 = new System.Windows.Forms.Label();
            this.statLab5 = new System.Windows.Forms.Label();
            this.statLab3 = new System.Windows.Forms.Label();
            this.statLab2 = new System.Windows.Forms.Label();
            this.statLab1 = new System.Windows.Forms.Label();
            this.statisticCheckBox = new System.Windows.Forms.CheckBox();
            this.autoEnableVisCB = new System.Windows.Forms.CheckBox();
            this.bufferTrackBarLabel = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxDataSendTim = new System.Windows.Forms.TextBox();
            this.textBoxDataProcessTim = new System.Windows.Forms.TextBox();
            this.textBoxFallV = new System.Windows.Forms.TextBox();
            this.textBoxRiseV = new System.Windows.Forms.TextBox();
            this.groupBoxVolume = new System.Windows.Forms.GroupBox();
            this.autoVolumeResB = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.textBoxVolPow = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxAutoVolStep = new System.Windows.Forms.TextBox();
            this.textBoxAutoVolCounter = new System.Windows.Forms.TextBox();
            this.textBoxAutoVolTarget = new System.Windows.Forms.TextBox();
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.autoVolumeCheckBox = new System.Windows.Forms.CheckBox();
            this.brightnessTrackBarLabel = new System.Windows.Forms.Label();
            this.textBoxColV = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxColB = new System.Windows.Forms.TextBox();
            this.textBoxColG = new System.Windows.Forms.TextBox();
            this.textBoxColR = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxFallB = new System.Windows.Forms.TextBox();
            this.textBoxFallG = new System.Windows.Forms.TextBox();
            this.textBoxFallR = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxRiseB = new System.Windows.Forms.TextBox();
            this.textBoxRiseG = new System.Windows.Forms.TextBox();
            this.textBoxRiseR = new System.Windows.Forms.TextBox();
            this.bufferTrackBar = new System.Windows.Forms.TrackBar();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxFreqH = new System.Windows.Forms.TextBox();
            this.textBoxFreqM = new System.Windows.Forms.TextBox();
            this.textBoxFreqL = new System.Windows.Forms.TextBox();
            this.textBoxVolC = new System.Windows.Forms.TextBox();
            this.volumeComboBox = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxFollowerB = new System.Windows.Forms.TextBox();
            this.textBoxFollowerG = new System.Windows.Forms.TextBox();
            this.textBoxFollowerR = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxLeaderB = new System.Windows.Forms.TextBox();
            this.textBoxLeaderG = new System.Windows.Forms.TextBox();
            this.textBoxLeaderR = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.VisualizationButtonApply = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonDefaultReset = new System.Windows.Forms.Button();
            this.textBoxHFC = new System.Windows.Forms.TextBox();
            this.textBoxMFC = new System.Windows.Forms.TextBox();
            this.textBoxLFC = new System.Windows.Forms.TextBox();
            this.hightFreqComboBox = new System.Windows.Forms.ComboBox();
            this.midFreqComboBox = new System.Windows.Forms.ComboBox();
            this.lowFreqComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabTerminal = new System.Windows.Forms.TabPage();
            this.tabCustomMode = new System.Windows.Forms.TabPage();
            this.label30 = new System.Windows.Forms.Label();
            this.cmBrtMultextBox = new System.Windows.Forms.TextBox();
            this.cmSaveCB = new System.Windows.Forms.CheckBox();
            this.cmCustomModeOnOffB = new System.Windows.Forms.Button();
            this.cmLoadToDeviceB = new System.Windows.Forms.Button();
            this.cmChangeB = new System.Windows.Forms.Button();
            this.cmDeleteSelectedB = new System.Windows.Forms.Button();
            this.cmAddB = new System.Windows.Forms.Button();
            this.cmTreeView = new System.Windows.Forms.TreeView();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.autoConnectCB = new System.Windows.Forms.CheckBox();
            this.timerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.formMinimizeCB = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmMenuItemInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMenuItemDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMenuItemChange = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.retryTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nsBacklightStateCB = new System.Windows.Forms.CheckBox();
            this.nsTransmitterStateCB = new System.Windows.Forms.CheckBox();
            this.nsBacklightTB = new System.Windows.Forms.TrackBar();
            this.label33 = new System.Windows.Forms.Label();
            this.nsBacklightSetB = new System.Windows.Forms.Button();
            this.nsDataUpdate = new System.Windows.Forms.Button();
            this.nsDeviceRebootB = new System.Windows.Forms.Button();
            this.commandGroup.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabFastSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nsVisualizationVolumeTB)).BeginInit();
            this.tabCommands.SuspendLayout();
            this.tabVisualization.SuspendLayout();
            this.groupBoxVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bufferTrackBar)).BeginInit();
            this.tabTerminal.SuspendLayout();
            this.tabCustomMode.SuspendLayout();
            this.cmListContextMenu.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nsBacklightTB)).BeginInit();
            this.SuspendLayout();
            // 
            // comportBox
            // 
            this.comportBox.FormattingEnabled = true;
            this.comportBox.Location = new System.Drawing.Point(12, 12);
            this.comportBox.Name = "comportBox";
            this.comportBox.Size = new System.Drawing.Size(121, 21);
            this.comportBox.TabIndex = 0;
            // 
            // comportConnectB
            // 
            this.comportConnectB.Location = new System.Drawing.Point(169, 12);
            this.comportConnectB.Name = "comportConnectB";
            this.comportConnectB.Size = new System.Drawing.Size(93, 23);
            this.comportConnectB.TabIndex = 2;
            this.comportConnectB.Text = "Подключиться";
            this.comportConnectB.UseVisualStyleBackColor = true;
            this.comportConnectB.Click += new System.EventHandler(this.ComportConnectB_Click);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 4000000;
            this.serialPort.PortName = "COM4";
            this.serialPort.WriteBufferSize = 4096;
            this.serialPort.WriteTimeout = 5;
            this.serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort_DataReceived);
            // 
            // comportReloadButton
            // 
            this.comportReloadButton.Location = new System.Drawing.Point(139, 11);
            this.comportReloadButton.Name = "comportReloadButton";
            this.comportReloadButton.Size = new System.Drawing.Size(24, 24);
            this.comportReloadButton.TabIndex = 1;
            this.comportReloadButton.Text = "R";
            this.comportReloadButton.UseVisualStyleBackColor = true;
            this.comportReloadButton.Click += new System.EventHandler(this.ComportReloadButton_Click);
            // 
            // commandGroup
            // 
            this.commandGroup.Controls.Add(this.label9);
            this.commandGroup.Controls.Add(this.label8);
            this.commandGroup.Controls.Add(this.label7);
            this.commandGroup.Controls.Add(this.label6);
            this.commandGroup.Controls.Add(this.label5);
            this.commandGroup.Controls.Add(this.label4);
            this.commandGroup.Controls.Add(this.label3);
            this.commandGroup.Controls.Add(this.label2);
            this.commandGroup.Controls.Add(this.label1);
            this.commandGroup.Controls.Add(this.textboxParam9);
            this.commandGroup.Controls.Add(this.textboxParam8);
            this.commandGroup.Controls.Add(this.textboxParam7);
            this.commandGroup.Controls.Add(this.textboxParam6);
            this.commandGroup.Controls.Add(this.textboxParam5);
            this.commandGroup.Controls.Add(this.textboxParam4);
            this.commandGroup.Controls.Add(this.textboxParam3);
            this.commandGroup.Controls.Add(this.textboxParam2);
            this.commandGroup.Controls.Add(this.textboxParam1);
            this.commandGroup.Controls.Add(this.commandSentB);
            this.commandGroup.Controls.Add(this.commandList);
            this.commandGroup.Location = new System.Drawing.Point(6, 6);
            this.commandGroup.Name = "commandGroup";
            this.commandGroup.Size = new System.Drawing.Size(631, 150);
            this.commandGroup.TabIndex = 4;
            this.commandGroup.TabStop = false;
            this.commandGroup.Text = "Команды";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(323, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = " ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(215, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(112, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = " ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = " ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(427, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = " ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(323, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = " ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(215, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = " ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = " ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = " ";
            // 
            // textboxParam9
            // 
            this.textboxParam9.Location = new System.Drawing.Point(324, 112);
            this.textboxParam9.Name = "textboxParam9";
            this.textboxParam9.Size = new System.Drawing.Size(100, 20);
            this.textboxParam9.TabIndex = 10;
            this.textboxParam9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam8
            // 
            this.textboxParam8.Location = new System.Drawing.Point(218, 112);
            this.textboxParam8.Name = "textboxParam8";
            this.textboxParam8.Size = new System.Drawing.Size(100, 20);
            this.textboxParam8.TabIndex = 9;
            this.textboxParam8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam7
            // 
            this.textboxParam7.Location = new System.Drawing.Point(112, 112);
            this.textboxParam7.Name = "textboxParam7";
            this.textboxParam7.Size = new System.Drawing.Size(100, 20);
            this.textboxParam7.TabIndex = 8;
            this.textboxParam7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam6
            // 
            this.textboxParam6.Location = new System.Drawing.Point(6, 112);
            this.textboxParam6.Name = "textboxParam6";
            this.textboxParam6.Size = new System.Drawing.Size(100, 20);
            this.textboxParam6.TabIndex = 7;
            this.textboxParam6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam5
            // 
            this.textboxParam5.Location = new System.Drawing.Point(430, 62);
            this.textboxParam5.Name = "textboxParam5";
            this.textboxParam5.Size = new System.Drawing.Size(100, 20);
            this.textboxParam5.TabIndex = 6;
            this.textboxParam5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam4
            // 
            this.textboxParam4.Location = new System.Drawing.Point(324, 62);
            this.textboxParam4.Name = "textboxParam4";
            this.textboxParam4.Size = new System.Drawing.Size(100, 20);
            this.textboxParam4.TabIndex = 5;
            this.textboxParam4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam3
            // 
            this.textboxParam3.Location = new System.Drawing.Point(218, 62);
            this.textboxParam3.Name = "textboxParam3";
            this.textboxParam3.Size = new System.Drawing.Size(100, 20);
            this.textboxParam3.TabIndex = 4;
            this.textboxParam3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam2
            // 
            this.textboxParam2.Location = new System.Drawing.Point(112, 62);
            this.textboxParam2.Name = "textboxParam2";
            this.textboxParam2.Size = new System.Drawing.Size(100, 20);
            this.textboxParam2.TabIndex = 3;
            this.textboxParam2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textboxParam1
            // 
            this.textboxParam1.Location = new System.Drawing.Point(6, 62);
            this.textboxParam1.Name = "textboxParam1";
            this.textboxParam1.Size = new System.Drawing.Size(100, 20);
            this.textboxParam1.TabIndex = 2;
            this.textboxParam1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // commandSentB
            // 
            this.commandSentB.Location = new System.Drawing.Point(283, 19);
            this.commandSentB.Name = "commandSentB";
            this.commandSentB.Size = new System.Drawing.Size(75, 21);
            this.commandSentB.TabIndex = 1;
            this.commandSentB.Text = "Отправить";
            this.commandSentB.UseVisualStyleBackColor = true;
            this.commandSentB.Click += new System.EventHandler(this.CommanSentB_Click);
            // 
            // commandList
            // 
            this.commandList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.commandList.Items.AddRange(new object[] {
            "Изменить цвет",
            "Изменить яркость",
            "Режим подстветки",
            "Custom Mode. Seetup блок",
            "Custom Mode. Data блок",
            "Custom Mode. Data copy",
            "Сохранить установки",
            "Множитель яркости",
            "Перезагрузка",
            "Калибровка",
            "Режим передатчика",
            "Получить доп. информацию"});
            this.commandList.Location = new System.Drawing.Point(6, 19);
            this.commandList.Name = "commandList";
            this.commandList.Size = new System.Drawing.Size(271, 21);
            this.commandList.TabIndex = 0;
            this.commandList.SelectedIndexChanged += new System.EventHandler(this.commandList_SelectedIndexChanged);
            // 
            // receiveTextBox
            // 
            this.receiveTextBox.Location = new System.Drawing.Point(3, 3);
            this.receiveTextBox.MaxLength = 64;
            this.receiveTextBox.Name = "receiveTextBox";
            this.receiveTextBox.ReadOnly = true;
            this.receiveTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.receiveTextBox.Size = new System.Drawing.Size(637, 384);
            this.receiveTextBox.TabIndex = 5;
            this.receiveTextBox.Text = "";
            this.receiveTextBox.TextChanged += new System.EventHandler(this.ReceiveTextBox_TextChanged);
            // 
            // comportDisconnectB
            // 
            this.comportDisconnectB.Location = new System.Drawing.Point(268, 12);
            this.comportDisconnectB.Name = "comportDisconnectB";
            this.comportDisconnectB.Size = new System.Drawing.Size(93, 23);
            this.comportDisconnectB.TabIndex = 3;
            this.comportDisconnectB.Text = "Отключиться";
            this.comportDisconnectB.UseVisualStyleBackColor = true;
            this.comportDisconnectB.Click += new System.EventHandler(this.ComportDisconnectB_Click);
            // 
            // VisualizationOnButton
            // 
            this.VisualizationOnButton.Location = new System.Drawing.Point(562, 362);
            this.VisualizationOnButton.Name = "VisualizationOnButton";
            this.VisualizationOnButton.Size = new System.Drawing.Size(75, 23);
            this.VisualizationOnButton.TabIndex = 2;
            this.VisualizationOnButton.Text = "Включить";
            this.VisualizationOnButton.UseVisualStyleBackColor = true;
            this.VisualizationOnButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabFastSettings);
            this.tabControl.Controls.Add(this.tabCommands);
            this.tabControl.Controls.Add(this.tabVisualization);
            this.tabControl.Controls.Add(this.tabTerminal);
            this.tabControl.Controls.Add(this.tabCustomMode);
            this.tabControl.Location = new System.Drawing.Point(12, 41);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(655, 416);
            this.tabControl.TabIndex = 4;
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabFastSettings
            // 
            this.tabFastSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabFastSettings.Controls.Add(this.nsDataUpdate);
            this.tabFastSettings.Controls.Add(this.groupBox3);
            this.tabFastSettings.Controls.Add(this.groupBox2);
            this.tabFastSettings.Controls.Add(this.groupBox1);
            this.tabFastSettings.Location = new System.Drawing.Point(4, 22);
            this.tabFastSettings.Name = "tabFastSettings";
            this.tabFastSettings.Size = new System.Drawing.Size(647, 390);
            this.tabFastSettings.TabIndex = 4;
            this.tabFastSettings.Text = "Newbie Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nsChangeColorB);
            this.groupBox2.Controls.Add(this.nsColorPanel);
            this.groupBox2.Controls.Add(this.nsSetModeB);
            this.groupBox2.Controls.Add(this.nsModeRB5);
            this.groupBox2.Controls.Add(this.nsModeRB4);
            this.groupBox2.Controls.Add(this.nsModeRB3);
            this.groupBox2.Controls.Add(this.nsModeRB2);
            this.groupBox2.Controls.Add(this.nsModeRB1);
            this.groupBox2.Location = new System.Drawing.Point(3, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 138);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Режимы";
            // 
            // nsChangeColorB
            // 
            this.nsChangeColorB.Location = new System.Drawing.Point(179, 13);
            this.nsChangeColorB.Name = "nsChangeColorB";
            this.nsChangeColorB.Size = new System.Drawing.Size(100, 23);
            this.nsChangeColorB.TabIndex = 7;
            this.nsChangeColorB.Text = "Изменить цвет";
            this.nsChangeColorB.UseVisualStyleBackColor = true;
            this.nsChangeColorB.Click += new System.EventHandler(this.nsChangeColorB_Click);
            // 
            // nsColorPanel
            // 
            this.nsColorPanel.BackColor = System.Drawing.Color.Red;
            this.nsColorPanel.Location = new System.Drawing.Point(150, 13);
            this.nsColorPanel.Name = "nsColorPanel";
            this.nsColorPanel.Size = new System.Drawing.Size(23, 23);
            this.nsColorPanel.TabIndex = 6;
            this.nsColorPanel.Click += new System.EventHandler(this.nsChangeColorB_Click);
            // 
            // nsSetModeB
            // 
            this.nsSetModeB.Location = new System.Drawing.Point(150, 105);
            this.nsSetModeB.Name = "nsSetModeB";
            this.nsSetModeB.Size = new System.Drawing.Size(129, 23);
            this.nsSetModeB.TabIndex = 5;
            this.nsSetModeB.Text = "Установить режим";
            this.nsSetModeB.UseVisualStyleBackColor = false;
            this.nsSetModeB.Click += new System.EventHandler(this.nsSetModeB_Click);
            // 
            // nsModeRB5
            // 
            this.nsModeRB5.AutoSize = true;
            this.nsModeRB5.Location = new System.Drawing.Point(6, 111);
            this.nsModeRB5.Name = "nsModeRB5";
            this.nsModeRB5.Size = new System.Drawing.Size(71, 17);
            this.nsModeRB5.TabIndex = 4;
            this.nsModeRB5.TabStop = true;
            this.nsModeRB5.Text = "Сияние 2";
            this.nsModeRB5.UseVisualStyleBackColor = true;
            // 
            // nsModeRB4
            // 
            this.nsModeRB4.AutoSize = true;
            this.nsModeRB4.Location = new System.Drawing.Point(6, 88);
            this.nsModeRB4.Name = "nsModeRB4";
            this.nsModeRB4.Size = new System.Drawing.Size(71, 17);
            this.nsModeRB4.TabIndex = 3;
            this.nsModeRB4.TabStop = true;
            this.nsModeRB4.Text = "Сияние 1";
            this.nsModeRB4.UseVisualStyleBackColor = true;
            // 
            // nsModeRB3
            // 
            this.nsModeRB3.AutoSize = true;
            this.nsModeRB3.Location = new System.Drawing.Point(6, 65);
            this.nsModeRB3.Name = "nsModeRB3";
            this.nsModeRB3.Size = new System.Drawing.Size(122, 17);
            this.nsModeRB3.TabIndex = 2;
            this.nsModeRB3.TabStop = true;
            this.nsModeRB3.Text = "Пользовательский";
            this.nsModeRB3.UseVisualStyleBackColor = true;
            // 
            // nsModeRB2
            // 
            this.nsModeRB2.AutoSize = true;
            this.nsModeRB2.Location = new System.Drawing.Point(6, 42);
            this.nsModeRB2.Name = "nsModeRB2";
            this.nsModeRB2.Size = new System.Drawing.Size(129, 17);
            this.nsModeRB2.TabIndex = 1;
            this.nsModeRB2.TabStop = true;
            this.nsModeRB2.Text = "Предустановленный";
            this.nsModeRB2.UseVisualStyleBackColor = true;
            // 
            // nsModeRB1
            // 
            this.nsModeRB1.AutoSize = true;
            this.nsModeRB1.Location = new System.Drawing.Point(6, 19);
            this.nsModeRB1.Name = "nsModeRB1";
            this.nsModeRB1.Size = new System.Drawing.Size(60, 17);
            this.nsModeRB1.TabIndex = 0;
            this.nsModeRB1.TabStop = true;
            this.nsModeRB1.Text = "Ручной";
            this.nsModeRB1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label32);
            this.groupBox1.Controls.Add(this.nsVisualizationVolumeTB);
            this.groupBox1.Controls.Add(this.nsVisualizationAutoVol);
            this.groupBox1.Controls.Add(this.nsVisualizationStartOn);
            this.groupBox1.Controls.Add(this.nsVisualizationOnCB);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Визуализация";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(147, 46);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(122, 13);
            this.label32.TabIndex = 4;
            this.label32.Text = "Максимальна яркость";
            // 
            // nsVisualizationVolumeTB
            // 
            this.nsVisualizationVolumeTB.AutoSize = false;
            this.nsVisualizationVolumeTB.Location = new System.Drawing.Point(275, 39);
            this.nsVisualizationVolumeTB.Maximum = 100;
            this.nsVisualizationVolumeTB.Minimum = 5;
            this.nsVisualizationVolumeTB.Name = "nsVisualizationVolumeTB";
            this.nsVisualizationVolumeTB.Size = new System.Drawing.Size(96, 20);
            this.nsVisualizationVolumeTB.TabIndex = 3;
            this.nsVisualizationVolumeTB.TickFrequency = 10;
            this.nsVisualizationVolumeTB.Value = 5;
            this.nsVisualizationVolumeTB.ValueChanged += new System.EventHandler(this.nsVisualizationVolumeTB_ValueChanged);
            // 
            // nsVisualizationAutoVol
            // 
            this.nsVisualizationAutoVol.AutoSize = true;
            this.nsVisualizationAutoVol.Location = new System.Drawing.Point(150, 19);
            this.nsVisualizationAutoVol.Name = "nsVisualizationAutoVol";
            this.nsVisualizationAutoVol.Size = new System.Drawing.Size(221, 17);
            this.nsVisualizationAutoVol.TabIndex = 2;
            this.nsVisualizationAutoVol.Text = "Автоматическая регулировка яркости";
            this.nsVisualizationAutoVol.UseVisualStyleBackColor = true;
            this.nsVisualizationAutoVol.CheckedChanged += new System.EventHandler(this.nsVisualizationAutoVol_CheckedChanged);
            // 
            // nsVisualizationStartOn
            // 
            this.nsVisualizationStartOn.AutoSize = true;
            this.nsVisualizationStartOn.Location = new System.Drawing.Point(6, 42);
            this.nsVisualizationStartOn.Name = "nsVisualizationStartOn";
            this.nsVisualizationStartOn.Size = new System.Drawing.Size(133, 17);
            this.nsVisualizationStartOn.TabIndex = 1;
            this.nsVisualizationStartOn.Text = "Включать при старте";
            this.nsVisualizationStartOn.UseVisualStyleBackColor = true;
            this.nsVisualizationStartOn.CheckedChanged += new System.EventHandler(this.nsVisualizationStartOn_CheckedChanged);
            // 
            // nsVisualizationOnCB
            // 
            this.nsVisualizationOnCB.AutoSize = true;
            this.nsVisualizationOnCB.Location = new System.Drawing.Point(6, 19);
            this.nsVisualizationOnCB.Name = "nsVisualizationOnCB";
            this.nsVisualizationOnCB.Size = new System.Drawing.Size(76, 17);
            this.nsVisualizationOnCB.TabIndex = 0;
            this.nsVisualizationOnCB.Text = "Включено";
            this.nsVisualizationOnCB.UseVisualStyleBackColor = true;
            this.nsVisualizationOnCB.CheckedChanged += new System.EventHandler(this.nsVisualizationOnCB_CheckedChanged);
            // 
            // tabCommands
            // 
            this.tabCommands.Controls.Add(this.commandInfoTextBox);
            this.tabCommands.Controls.Add(this.commandGroup);
            this.tabCommands.Location = new System.Drawing.Point(4, 22);
            this.tabCommands.Name = "tabCommands";
            this.tabCommands.Padding = new System.Windows.Forms.Padding(3);
            this.tabCommands.Size = new System.Drawing.Size(647, 390);
            this.tabCommands.TabIndex = 0;
            this.tabCommands.Text = "Команды";
            // 
            // commandInfoTextBox
            // 
            this.commandInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandInfoTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.commandInfoTextBox.Location = new System.Drawing.Point(6, 162);
            this.commandInfoTextBox.Name = "commandInfoTextBox";
            this.commandInfoTextBox.ReadOnly = true;
            this.commandInfoTextBox.Size = new System.Drawing.Size(631, 200);
            this.commandInfoTextBox.TabIndex = 11;
            this.commandInfoTextBox.Text = "";
            // 
            // tabVisualization
            // 
            this.tabVisualization.Controls.Add(this.statLabD3);
            this.tabVisualization.Controls.Add(this.statLabD2);
            this.tabVisualization.Controls.Add(this.statLabD1);
            this.tabVisualization.Controls.Add(this.label29);
            this.tabVisualization.Controls.Add(this.textBoxVolumeUpdateTim);
            this.tabVisualization.Controls.Add(this.statLab12);
            this.tabVisualization.Controls.Add(this.statLab11);
            this.tabVisualization.Controls.Add(this.statLab10);
            this.tabVisualization.Controls.Add(this.statLab9);
            this.tabVisualization.Controls.Add(this.statLab8);
            this.tabVisualization.Controls.Add(this.statLab4);
            this.tabVisualization.Controls.Add(this.statLab7);
            this.tabVisualization.Controls.Add(this.statLab6);
            this.tabVisualization.Controls.Add(this.statLab5);
            this.tabVisualization.Controls.Add(this.statLab3);
            this.tabVisualization.Controls.Add(this.statLab2);
            this.tabVisualization.Controls.Add(this.statLab1);
            this.tabVisualization.Controls.Add(this.statisticCheckBox);
            this.tabVisualization.Controls.Add(this.autoEnableVisCB);
            this.tabVisualization.Controls.Add(this.bufferTrackBarLabel);
            this.tabVisualization.Controls.Add(this.label28);
            this.tabVisualization.Controls.Add(this.label27);
            this.tabVisualization.Controls.Add(this.textBoxDataSendTim);
            this.tabVisualization.Controls.Add(this.textBoxDataProcessTim);
            this.tabVisualization.Controls.Add(this.textBoxFallV);
            this.tabVisualization.Controls.Add(this.textBoxRiseV);
            this.tabVisualization.Controls.Add(this.groupBoxVolume);
            this.tabVisualization.Controls.Add(this.textBoxColV);
            this.tabVisualization.Controls.Add(this.label17);
            this.tabVisualization.Controls.Add(this.textBoxColB);
            this.tabVisualization.Controls.Add(this.textBoxColG);
            this.tabVisualization.Controls.Add(this.textBoxColR);
            this.tabVisualization.Controls.Add(this.label22);
            this.tabVisualization.Controls.Add(this.textBoxFallB);
            this.tabVisualization.Controls.Add(this.textBoxFallG);
            this.tabVisualization.Controls.Add(this.textBoxFallR);
            this.tabVisualization.Controls.Add(this.label23);
            this.tabVisualization.Controls.Add(this.textBoxRiseB);
            this.tabVisualization.Controls.Add(this.textBoxRiseG);
            this.tabVisualization.Controls.Add(this.textBoxRiseR);
            this.tabVisualization.Controls.Add(this.bufferTrackBar);
            this.tabVisualization.Controls.Add(this.label21);
            this.tabVisualization.Controls.Add(this.textBoxFreqH);
            this.tabVisualization.Controls.Add(this.textBoxFreqM);
            this.tabVisualization.Controls.Add(this.textBoxFreqL);
            this.tabVisualization.Controls.Add(this.textBoxVolC);
            this.tabVisualization.Controls.Add(this.volumeComboBox);
            this.tabVisualization.Controls.Add(this.label20);
            this.tabVisualization.Controls.Add(this.label19);
            this.tabVisualization.Controls.Add(this.textBoxFollowerB);
            this.tabVisualization.Controls.Add(this.textBoxFollowerG);
            this.tabVisualization.Controls.Add(this.textBoxFollowerR);
            this.tabVisualization.Controls.Add(this.label18);
            this.tabVisualization.Controls.Add(this.textBoxLeaderB);
            this.tabVisualization.Controls.Add(this.textBoxLeaderG);
            this.tabVisualization.Controls.Add(this.textBoxLeaderR);
            this.tabVisualization.Controls.Add(this.label16);
            this.tabVisualization.Controls.Add(this.label15);
            this.tabVisualization.Controls.Add(this.label14);
            this.tabVisualization.Controls.Add(this.VisualizationButtonApply);
            this.tabVisualization.Controls.Add(this.label13);
            this.tabVisualization.Controls.Add(this.buttonDefaultReset);
            this.tabVisualization.Controls.Add(this.textBoxHFC);
            this.tabVisualization.Controls.Add(this.textBoxMFC);
            this.tabVisualization.Controls.Add(this.textBoxLFC);
            this.tabVisualization.Controls.Add(this.hightFreqComboBox);
            this.tabVisualization.Controls.Add(this.midFreqComboBox);
            this.tabVisualization.Controls.Add(this.lowFreqComboBox);
            this.tabVisualization.Controls.Add(this.label12);
            this.tabVisualization.Controls.Add(this.label11);
            this.tabVisualization.Controls.Add(this.label10);
            this.tabVisualization.Controls.Add(this.VisualizationOnButton);
            this.tabVisualization.Location = new System.Drawing.Point(4, 22);
            this.tabVisualization.Name = "tabVisualization";
            this.tabVisualization.Padding = new System.Windows.Forms.Padding(3);
            this.tabVisualization.Size = new System.Drawing.Size(647, 390);
            this.tabVisualization.TabIndex = 1;
            this.tabVisualization.Text = "Визуализация";
            // 
            // statLabD3
            // 
            this.statLabD3.Location = new System.Drawing.Point(6, 285);
            this.statLabD3.Name = "statLabD3";
            this.statLabD3.Size = new System.Drawing.Size(57, 13);
            this.statLabD3.TabIndex = 87;
            this.statLabD3.Text = "Max";
            this.statLabD3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLabD3.Visible = false;
            // 
            // statLabD2
            // 
            this.statLabD2.Location = new System.Drawing.Point(6, 267);
            this.statLabD2.Name = "statLabD2";
            this.statLabD2.Size = new System.Drawing.Size(57, 13);
            this.statLabD2.TabIndex = 86;
            this.statLabD2.Text = "Current";
            this.statLabD2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLabD2.Visible = false;
            // 
            // statLabD1
            // 
            this.statLabD1.Location = new System.Drawing.Point(6, 249);
            this.statLabD1.Name = "statLabD1";
            this.statLabD1.Size = new System.Drawing.Size(57, 13);
            this.statLabD1.TabIndex = 85;
            this.statLabD1.Text = "Target";
            this.statLabD1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLabD1.Visible = false;
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(550, 173);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(90, 26);
            this.label29.TabIndex = 84;
            this.label29.Text = "Volume update timer";
            this.toolTips.SetToolTip(this.label29, "Период обновления значения громкости");
            // 
            // textBoxVolumeUpdateTim
            // 
            this.textBoxVolumeUpdateTim.Location = new System.Drawing.Point(553, 199);
            this.textBoxVolumeUpdateTim.Name = "textBoxVolumeUpdateTim";
            this.textBoxVolumeUpdateTim.Size = new System.Drawing.Size(60, 20);
            this.textBoxVolumeUpdateTim.TabIndex = 5;
            this.textBoxVolumeUpdateTim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // statLab12
            // 
            this.statLab12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab12.Location = new System.Drawing.Point(455, 267);
            this.statLab12.Name = "statLab12";
            this.statLab12.Size = new System.Drawing.Size(41, 13);
            this.statLab12.TabIndex = 82;
            this.statLab12.Text = "1";
            this.statLab12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab12.Visible = false;
            // 
            // statLab11
            // 
            this.statLab11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab11.Location = new System.Drawing.Point(332, 267);
            this.statLab11.Name = "statLab11";
            this.statLab11.Size = new System.Drawing.Size(41, 13);
            this.statLab11.TabIndex = 81;
            this.statLab11.Text = "1";
            this.statLab11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab11.Visible = false;
            // 
            // statLab10
            // 
            this.statLab10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab10.Location = new System.Drawing.Point(209, 267);
            this.statLab10.Name = "statLab10";
            this.statLab10.Size = new System.Drawing.Size(41, 13);
            this.statLab10.TabIndex = 80;
            this.statLab10.Text = "1";
            this.statLab10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab10.Visible = false;
            // 
            // statLab9
            // 
            this.statLab9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab9.Location = new System.Drawing.Point(86, 267);
            this.statLab9.Name = "statLab9";
            this.statLab9.Size = new System.Drawing.Size(41, 13);
            this.statLab9.TabIndex = 79;
            this.statLab9.Text = "1";
            this.statLab9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab9.Visible = false;
            // 
            // statLab8
            // 
            this.statLab8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab8.Location = new System.Drawing.Point(455, 285);
            this.statLab8.Name = "statLab8";
            this.statLab8.Size = new System.Drawing.Size(41, 13);
            this.statLab8.TabIndex = 78;
            this.statLab8.Text = "1";
            this.statLab8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab8.Visible = false;
            // 
            // statLab4
            // 
            this.statLab4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab4.Location = new System.Drawing.Point(455, 249);
            this.statLab4.Name = "statLab4";
            this.statLab4.Size = new System.Drawing.Size(41, 13);
            this.statLab4.TabIndex = 77;
            this.statLab4.Text = "1";
            this.statLab4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab4.Visible = false;
            // 
            // statLab7
            // 
            this.statLab7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab7.Location = new System.Drawing.Point(332, 285);
            this.statLab7.Name = "statLab7";
            this.statLab7.Size = new System.Drawing.Size(41, 13);
            this.statLab7.TabIndex = 76;
            this.statLab7.Text = "1";
            this.statLab7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab7.Visible = false;
            // 
            // statLab6
            // 
            this.statLab6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab6.Location = new System.Drawing.Point(209, 285);
            this.statLab6.Name = "statLab6";
            this.statLab6.Size = new System.Drawing.Size(41, 13);
            this.statLab6.TabIndex = 75;
            this.statLab6.Text = "1";
            this.statLab6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab6.Visible = false;
            // 
            // statLab5
            // 
            this.statLab5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab5.Location = new System.Drawing.Point(86, 285);
            this.statLab5.Name = "statLab5";
            this.statLab5.Size = new System.Drawing.Size(41, 13);
            this.statLab5.TabIndex = 74;
            this.statLab5.Text = "1";
            this.statLab5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab5.Visible = false;
            // 
            // statLab3
            // 
            this.statLab3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab3.Location = new System.Drawing.Point(332, 249);
            this.statLab3.Name = "statLab3";
            this.statLab3.Size = new System.Drawing.Size(41, 13);
            this.statLab3.TabIndex = 73;
            this.statLab3.Text = "1";
            this.statLab3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab3.Visible = false;
            // 
            // statLab2
            // 
            this.statLab2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab2.Location = new System.Drawing.Point(209, 249);
            this.statLab2.Name = "statLab2";
            this.statLab2.Size = new System.Drawing.Size(41, 13);
            this.statLab2.TabIndex = 72;
            this.statLab2.Text = "1";
            this.statLab2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab2.Visible = false;
            // 
            // statLab1
            // 
            this.statLab1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statLab1.Location = new System.Drawing.Point(86, 249);
            this.statLab1.Name = "statLab1";
            this.statLab1.Size = new System.Drawing.Size(41, 13);
            this.statLab1.TabIndex = 71;
            this.statLab1.Text = "1";
            this.statLab1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.statLab1.Visible = false;
            // 
            // statisticCheckBox
            // 
            this.statisticCheckBox.AutoSize = true;
            this.statisticCheckBox.Location = new System.Drawing.Point(553, 258);
            this.statisticCheckBox.Name = "statisticCheckBox";
            this.statisticCheckBox.Size = new System.Drawing.Size(84, 17);
            this.statisticCheckBox.TabIndex = 40;
            this.statisticCheckBox.Text = "Статистика";
            this.statisticCheckBox.UseVisualStyleBackColor = true;
            this.statisticCheckBox.CheckedChanged += new System.EventHandler(this.statisticCheckBox_CheckedChanged);
            // 
            // autoEnableVisCB
            // 
            this.autoEnableVisCB.AutoSize = true;
            this.autoEnableVisCB.Location = new System.Drawing.Point(553, 281);
            this.autoEnableVisCB.Name = "autoEnableVisCB";
            this.autoEnableVisCB.Size = new System.Drawing.Size(84, 17);
            this.autoEnableVisCB.TabIndex = 41;
            this.autoEnableVisCB.Text = "Auto Enable";
            this.autoEnableVisCB.UseVisualStyleBackColor = true;
            // 
            // bufferTrackBarLabel
            // 
            this.bufferTrackBarLabel.Location = new System.Drawing.Point(517, 37);
            this.bufferTrackBarLabel.Name = "bufferTrackBarLabel";
            this.bufferTrackBarLabel.Size = new System.Drawing.Size(30, 13);
            this.bufferTrackBarLabel.TabIndex = 68;
            this.bufferTrackBarLabel.Text = "12";
            this.bufferTrackBarLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.toolTips.SetToolTip(this.bufferTrackBarLabel, "Размер буфера FFT. 2^значение");
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(550, 121);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(90, 26);
            this.label28.TabIndex = 67;
            this.label28.Text = "Data sending timer";
            this.toolTips.SetToolTip(this.label28, "Период отправки данных на устройство");
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(550, 52);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(90, 26);
            this.label27.TabIndex = 66;
            this.label27.Text = "Data processing timer";
            this.toolTips.SetToolTip(this.label27, "Период выполнения FFT");
            // 
            // textBoxDataSendTim
            // 
            this.textBoxDataSendTim.Location = new System.Drawing.Point(553, 147);
            this.textBoxDataSendTim.Name = "textBoxDataSendTim";
            this.textBoxDataSendTim.Size = new System.Drawing.Size(60, 20);
            this.textBoxDataSendTim.TabIndex = 4;
            this.textBoxDataSendTim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxDataProcessTim
            // 
            this.textBoxDataProcessTim.Location = new System.Drawing.Point(553, 78);
            this.textBoxDataProcessTim.Name = "textBoxDataProcessTim";
            this.textBoxDataProcessTim.Size = new System.Drawing.Size(60, 20);
            this.textBoxDataProcessTim.TabIndex = 3;
            this.textBoxDataProcessTim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFallV
            // 
            this.textBoxFallV.Location = new System.Drawing.Point(432, 225);
            this.textBoxFallV.Name = "textBoxFallV";
            this.textBoxFallV.Size = new System.Drawing.Size(60, 20);
            this.textBoxFallV.TabIndex = 39;
            this.textBoxFallV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxRiseV
            // 
            this.textBoxRiseV.Location = new System.Drawing.Point(432, 199);
            this.textBoxRiseV.Name = "textBoxRiseV";
            this.textBoxRiseV.Size = new System.Drawing.Size(60, 20);
            this.textBoxRiseV.TabIndex = 35;
            this.textBoxRiseV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxVolume
            // 
            this.groupBoxVolume.Controls.Add(this.autoVolumeResB);
            this.groupBoxVolume.Controls.Add(this.label31);
            this.groupBoxVolume.Controls.Add(this.textBoxVolPow);
            this.groupBoxVolume.Controls.Add(this.label26);
            this.groupBoxVolume.Controls.Add(this.label25);
            this.groupBoxVolume.Controls.Add(this.label24);
            this.groupBoxVolume.Controls.Add(this.textBoxAutoVolStep);
            this.groupBoxVolume.Controls.Add(this.textBoxAutoVolCounter);
            this.groupBoxVolume.Controls.Add(this.textBoxAutoVolTarget);
            this.groupBoxVolume.Controls.Add(this.brightnessTrackBar);
            this.groupBoxVolume.Controls.Add(this.autoVolumeCheckBox);
            this.groupBoxVolume.Controls.Add(this.brightnessTrackBarLabel);
            this.groupBoxVolume.Location = new System.Drawing.Point(9, 303);
            this.groupBoxVolume.Name = "groupBoxVolume";
            this.groupBoxVolume.Size = new System.Drawing.Size(547, 82);
            this.groupBoxVolume.TabIndex = 61;
            this.groupBoxVolume.TabStop = false;
            this.groupBoxVolume.Text = "Volume";
            // 
            // autoVolumeResB
            // 
            this.autoVolumeResB.Location = new System.Drawing.Point(91, 52);
            this.autoVolumeResB.Name = "autoVolumeResB";
            this.autoVolumeResB.Size = new System.Drawing.Size(36, 23);
            this.autoVolumeResB.TabIndex = 89;
            this.autoVolumeResB.Text = "Res";
            this.autoVolumeResB.UseVisualStyleBackColor = true;
            this.autoVolumeResB.Click += new System.EventHandler(this.autoVolumeResB_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(449, 57);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(37, 13);
            this.label31.TabIndex = 88;
            this.label31.Text = "Power";
            this.toolTips.SetToolTip(this.label31, "Множитель степени для вычисления значения визуализации громкости");
            // 
            // textBoxVolPow
            // 
            this.textBoxVolPow.Location = new System.Drawing.Point(492, 54);
            this.textBoxVolPow.Name = "textBoxVolPow";
            this.textBoxVolPow.Size = new System.Drawing.Size(40, 20);
            this.textBoxVolPow.TabIndex = 87;
            this.textBoxVolPow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(328, 57);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(55, 13);
            this.label26.TabIndex = 69;
            this.label26.Text = "Hysteresis";
            this.toolTips.SetToolTip(this.label26, "Гистерезис от максимального значения громкости для сброса счетчика");
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(232, 57);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(44, 13);
            this.label25.TabIndex = 68;
            this.label25.Text = "Counter";
            this.toolTips.SetToolTip(this.label25, "Счетчик для перерасчета громкости. Зависит от Volume Update Timer");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(133, 57);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 13);
            this.label24.TabIndex = 67;
            this.label24.Text = "Target";
            this.toolTips.SetToolTip(this.label24, "Целевое значение множителя. Максимум - 1.0");
            // 
            // textBoxAutoVolStep
            // 
            this.textBoxAutoVolStep.Location = new System.Drawing.Point(387, 54);
            this.textBoxAutoVolStep.Name = "textBoxAutoVolStep";
            this.textBoxAutoVolStep.Size = new System.Drawing.Size(40, 20);
            this.textBoxAutoVolStep.TabIndex = 10;
            this.textBoxAutoVolStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxAutoVolCounter
            // 
            this.textBoxAutoVolCounter.Location = new System.Drawing.Point(282, 54);
            this.textBoxAutoVolCounter.Name = "textBoxAutoVolCounter";
            this.textBoxAutoVolCounter.Size = new System.Drawing.Size(40, 20);
            this.textBoxAutoVolCounter.TabIndex = 9;
            this.textBoxAutoVolCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxAutoVolTarget
            // 
            this.textBoxAutoVolTarget.Location = new System.Drawing.Point(177, 54);
            this.textBoxAutoVolTarget.Name = "textBoxAutoVolTarget";
            this.textBoxAutoVolTarget.Size = new System.Drawing.Size(40, 20);
            this.textBoxAutoVolTarget.TabIndex = 8;
            this.textBoxAutoVolTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // brightnessTrackBar
            // 
            this.brightnessTrackBar.AutoSize = false;
            this.brightnessTrackBar.Enabled = false;
            this.brightnessTrackBar.Location = new System.Drawing.Point(37, 19);
            this.brightnessTrackBar.Maximum = 158;
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.Size = new System.Drawing.Size(504, 25);
            this.brightnessTrackBar.TabIndex = 6;
            this.brightnessTrackBar.TickFrequency = 10;
            this.brightnessTrackBar.Value = 50;
            this.brightnessTrackBar.ValueChanged += new System.EventHandler(this.BrightnessTrackBar_ValueChanged);
            // 
            // autoVolumeCheckBox
            // 
            this.autoVolumeCheckBox.Checked = true;
            this.autoVolumeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoVolumeCheckBox.Location = new System.Drawing.Point(6, 56);
            this.autoVolumeCheckBox.Name = "autoVolumeCheckBox";
            this.autoVolumeCheckBox.Size = new System.Drawing.Size(86, 17);
            this.autoVolumeCheckBox.TabIndex = 7;
            this.autoVolumeCheckBox.Text = "Auto Volume";
            this.autoVolumeCheckBox.UseVisualStyleBackColor = true;
            this.autoVolumeCheckBox.CheckedChanged += new System.EventHandler(this.autoVolumeCheckBox_CheckedChanged);
            // 
            // brightnessTrackBarLabel
            // 
            this.brightnessTrackBarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.brightnessTrackBarLabel.Location = new System.Drawing.Point(3, 30);
            this.brightnessTrackBarLabel.Name = "brightnessTrackBarLabel";
            this.brightnessTrackBarLabel.Size = new System.Drawing.Size(38, 13);
            this.brightnessTrackBarLabel.TabIndex = 60;
            this.brightnessTrackBarLabel.Text = "1";
            this.brightnessTrackBarLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // textBoxColV
            // 
            this.textBoxColV.Location = new System.Drawing.Point(432, 121);
            this.textBoxColV.Name = "textBoxColV";
            this.textBoxColV.Size = new System.Drawing.Size(60, 20);
            this.textBoxColV.TabIndex = 25;
            this.textBoxColV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 128);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 57;
            this.label17.Text = "Default";
            this.toolTips.SetToolTip(this.label17, "Первоначальное значение");
            // 
            // textBoxColB
            // 
            this.textBoxColB.Location = new System.Drawing.Point(309, 121);
            this.textBoxColB.Name = "textBoxColB";
            this.textBoxColB.Size = new System.Drawing.Size(60, 20);
            this.textBoxColB.TabIndex = 24;
            this.textBoxColB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxColG
            // 
            this.textBoxColG.Location = new System.Drawing.Point(186, 121);
            this.textBoxColG.Name = "textBoxColG";
            this.textBoxColG.Size = new System.Drawing.Size(60, 20);
            this.textBoxColG.TabIndex = 23;
            this.textBoxColG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxColR
            // 
            this.textBoxColR.Location = new System.Drawing.Point(63, 121);
            this.textBoxColR.Name = "textBoxColR";
            this.textBoxColR.Size = new System.Drawing.Size(60, 20);
            this.textBoxColR.TabIndex = 22;
            this.textBoxColR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(6, 232);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(57, 13);
            this.label22.TabIndex = 53;
            this.label22.Text = "Max Fall";
            this.toolTips.SetToolTip(this.label22, "Максимальная скорость падения значения цвета");
            // 
            // textBoxFallB
            // 
            this.textBoxFallB.Location = new System.Drawing.Point(309, 225);
            this.textBoxFallB.Name = "textBoxFallB";
            this.textBoxFallB.Size = new System.Drawing.Size(60, 20);
            this.textBoxFallB.TabIndex = 38;
            this.textBoxFallB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFallG
            // 
            this.textBoxFallG.Location = new System.Drawing.Point(186, 225);
            this.textBoxFallG.Name = "textBoxFallG";
            this.textBoxFallG.Size = new System.Drawing.Size(60, 20);
            this.textBoxFallG.TabIndex = 37;
            this.textBoxFallG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFallR
            // 
            this.textBoxFallR.Location = new System.Drawing.Point(63, 225);
            this.textBoxFallR.Name = "textBoxFallR";
            this.textBoxFallR.Size = new System.Drawing.Size(60, 20);
            this.textBoxFallR.TabIndex = 36;
            this.textBoxFallR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.Location = new System.Drawing.Point(6, 206);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(57, 13);
            this.label23.TabIndex = 49;
            this.label23.Text = "Max Rise";
            this.toolTips.SetToolTip(this.label23, "Максимальная скорость нарастания значения цвета");
            // 
            // textBoxRiseB
            // 
            this.textBoxRiseB.Location = new System.Drawing.Point(309, 199);
            this.textBoxRiseB.Name = "textBoxRiseB";
            this.textBoxRiseB.Size = new System.Drawing.Size(60, 20);
            this.textBoxRiseB.TabIndex = 34;
            this.textBoxRiseB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxRiseG
            // 
            this.textBoxRiseG.Location = new System.Drawing.Point(186, 199);
            this.textBoxRiseG.Name = "textBoxRiseG";
            this.textBoxRiseG.Size = new System.Drawing.Size(60, 20);
            this.textBoxRiseG.TabIndex = 33;
            this.textBoxRiseG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxRiseR
            // 
            this.textBoxRiseR.Location = new System.Drawing.Point(63, 199);
            this.textBoxRiseR.Name = "textBoxRiseR";
            this.textBoxRiseR.Size = new System.Drawing.Size(60, 20);
            this.textBoxRiseR.TabIndex = 32;
            this.textBoxRiseR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bufferTrackBar
            // 
            this.bufferTrackBar.AutoSize = false;
            this.bufferTrackBar.Location = new System.Drawing.Point(553, 25);
            this.bufferTrackBar.Maximum = 16;
            this.bufferTrackBar.Minimum = 11;
            this.bufferTrackBar.Name = "bufferTrackBar";
            this.bufferTrackBar.Size = new System.Drawing.Size(75, 25);
            this.bufferTrackBar.TabIndex = 45;
            this.toolTips.SetToolTip(this.bufferTrackBar, "Размер буфера FFT. 2^значение");
            this.bufferTrackBar.Value = 12;
            this.bufferTrackBar.Scroll += new System.EventHandler(this.bufferTrackBar_Scroll_1);
            this.bufferTrackBar.ValueChanged += new System.EventHandler(this.bufferTrackBar_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(3, 59);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 13);
            this.label21.TabIndex = 43;
            this.label21.Text = "Frequency";
            this.toolTips.SetToolTip(this.label21, "Частота среза");
            // 
            // textBoxFreqH
            // 
            this.textBoxFreqH.Location = new System.Drawing.Point(309, 52);
            this.textBoxFreqH.Name = "textBoxFreqH";
            this.textBoxFreqH.Size = new System.Drawing.Size(60, 20);
            this.textBoxFreqH.TabIndex = 17;
            this.textBoxFreqH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFreqM
            // 
            this.textBoxFreqM.Location = new System.Drawing.Point(186, 52);
            this.textBoxFreqM.Name = "textBoxFreqM";
            this.textBoxFreqM.Size = new System.Drawing.Size(60, 20);
            this.textBoxFreqM.TabIndex = 16;
            this.textBoxFreqM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFreqL
            // 
            this.textBoxFreqL.Location = new System.Drawing.Point(63, 52);
            this.textBoxFreqL.Name = "textBoxFreqL";
            this.textBoxFreqL.Size = new System.Drawing.Size(60, 20);
            this.textBoxFreqL.TabIndex = 15;
            this.textBoxFreqL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxVolC
            // 
            this.textBoxVolC.Location = new System.Drawing.Point(432, 78);
            this.textBoxVolC.Name = "textBoxVolC";
            this.textBoxVolC.Size = new System.Drawing.Size(60, 20);
            this.textBoxVolC.TabIndex = 21;
            this.textBoxVolC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // volumeComboBox
            // 
            this.volumeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.volumeComboBox.Enabled = false;
            this.volumeComboBox.FormattingEnabled = true;
            this.volumeComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "Volume",
            "Nothing"});
            this.volumeComboBox.Location = new System.Drawing.Point(375, 25);
            this.volumeComboBox.Name = "volumeComboBox";
            this.volumeComboBox.Size = new System.Drawing.Size(117, 21);
            this.volumeComboBox.TabIndex = 14;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(378, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(120, 13);
            this.label20.TabIndex = 37;
            this.label20.Text = "Volume";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(6, 180);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 36;
            this.label19.Text = "Follower";
            this.toolTips.SetToolTip(this.label19, "Множитель, применяемый для цвета с не наибольшим значением");
            // 
            // textBoxFollowerB
            // 
            this.textBoxFollowerB.Location = new System.Drawing.Point(309, 173);
            this.textBoxFollowerB.Name = "textBoxFollowerB";
            this.textBoxFollowerB.Size = new System.Drawing.Size(60, 20);
            this.textBoxFollowerB.TabIndex = 31;
            this.textBoxFollowerB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFollowerG
            // 
            this.textBoxFollowerG.Location = new System.Drawing.Point(186, 173);
            this.textBoxFollowerG.Name = "textBoxFollowerG";
            this.textBoxFollowerG.Size = new System.Drawing.Size(60, 20);
            this.textBoxFollowerG.TabIndex = 30;
            this.textBoxFollowerG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxFollowerR
            // 
            this.textBoxFollowerR.Location = new System.Drawing.Point(63, 173);
            this.textBoxFollowerR.Name = "textBoxFollowerR";
            this.textBoxFollowerR.Size = new System.Drawing.Size(60, 20);
            this.textBoxFollowerR.TabIndex = 29;
            this.textBoxFollowerR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(6, 154);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 32;
            this.label18.Text = "Leader";
            this.toolTips.SetToolTip(this.label18, "Множитель, применяемый для цвета с наибольшим значением");
            // 
            // textBoxLeaderB
            // 
            this.textBoxLeaderB.Location = new System.Drawing.Point(309, 147);
            this.textBoxLeaderB.Name = "textBoxLeaderB";
            this.textBoxLeaderB.Size = new System.Drawing.Size(60, 20);
            this.textBoxLeaderB.TabIndex = 28;
            this.textBoxLeaderB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxLeaderG
            // 
            this.textBoxLeaderG.Location = new System.Drawing.Point(186, 147);
            this.textBoxLeaderG.Name = "textBoxLeaderG";
            this.textBoxLeaderG.Size = new System.Drawing.Size(60, 20);
            this.textBoxLeaderG.TabIndex = 27;
            this.textBoxLeaderG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxLeaderR
            // 
            this.textBoxLeaderR.Location = new System.Drawing.Point(63, 147);
            this.textBoxLeaderR.Name = "textBoxLeaderR";
            this.textBoxLeaderR.Size = new System.Drawing.Size(60, 20);
            this.textBoxLeaderR.TabIndex = 26;
            this.textBoxLeaderR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(341, 105);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(28, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "Blue";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(210, 105);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Green";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(96, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(27, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "Red";
            // 
            // VisualizationButtonApply
            // 
            this.VisualizationButtonApply.Location = new System.Drawing.Point(562, 333);
            this.VisualizationButtonApply.Name = "VisualizationButtonApply";
            this.VisualizationButtonApply.Size = new System.Drawing.Size(75, 23);
            this.VisualizationButtonApply.TabIndex = 1;
            this.VisualizationButtonApply.Text = "Применить";
            this.VisualizationButtonApply.UseVisualStyleBackColor = true;
            this.VisualizationButtonApply.Click += new System.EventHandler(this.VisualizationButtonApply_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 81);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Coefficient";
            this.toolTips.SetToolTip(this.label13, "Множитель для значения FFT данного диапазона");
            // 
            // buttonDefaultReset
            // 
            this.buttonDefaultReset.Location = new System.Drawing.Point(562, 304);
            this.buttonDefaultReset.Name = "buttonDefaultReset";
            this.buttonDefaultReset.Size = new System.Drawing.Size(75, 23);
            this.buttonDefaultReset.TabIndex = 0;
            this.buttonDefaultReset.Text = "Сброс";
            this.buttonDefaultReset.UseVisualStyleBackColor = true;
            this.buttonDefaultReset.Click += new System.EventHandler(this.buttonDefaultReset_Click);
            // 
            // textBoxHFC
            // 
            this.textBoxHFC.Location = new System.Drawing.Point(309, 78);
            this.textBoxHFC.Name = "textBoxHFC";
            this.textBoxHFC.Size = new System.Drawing.Size(60, 20);
            this.textBoxHFC.TabIndex = 20;
            this.textBoxHFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxMFC
            // 
            this.textBoxMFC.Location = new System.Drawing.Point(186, 78);
            this.textBoxMFC.Name = "textBoxMFC";
            this.textBoxMFC.Size = new System.Drawing.Size(60, 20);
            this.textBoxMFC.TabIndex = 19;
            this.textBoxMFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxLFC
            // 
            this.textBoxLFC.Location = new System.Drawing.Point(63, 78);
            this.textBoxLFC.Name = "textBoxLFC";
            this.textBoxLFC.Size = new System.Drawing.Size(60, 20);
            this.textBoxLFC.TabIndex = 18;
            this.textBoxLFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // hightFreqComboBox
            // 
            this.hightFreqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hightFreqComboBox.FormattingEnabled = true;
            this.hightFreqComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "Volume",
            "Nothing"});
            this.hightFreqComboBox.Location = new System.Drawing.Point(252, 25);
            this.hightFreqComboBox.Name = "hightFreqComboBox";
            this.hightFreqComboBox.Size = new System.Drawing.Size(117, 21);
            this.hightFreqComboBox.TabIndex = 13;
            // 
            // midFreqComboBox
            // 
            this.midFreqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.midFreqComboBox.FormattingEnabled = true;
            this.midFreqComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "Volume",
            "Nothing"});
            this.midFreqComboBox.Location = new System.Drawing.Point(129, 25);
            this.midFreqComboBox.Name = "midFreqComboBox";
            this.midFreqComboBox.Size = new System.Drawing.Size(117, 21);
            this.midFreqComboBox.TabIndex = 12;
            // 
            // lowFreqComboBox
            // 
            this.lowFreqComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lowFreqComboBox.FormattingEnabled = true;
            this.lowFreqComboBox.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "Volume",
            "Nothing"});
            this.lowFreqComboBox.Location = new System.Drawing.Point(6, 25);
            this.lowFreqComboBox.Name = "lowFreqComboBox";
            this.lowFreqComboBox.Size = new System.Drawing.Size(117, 21);
            this.lowFreqComboBox.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(255, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Hight frequency";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(129, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Middle frequency";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Low frequency";
            // 
            // tabTerminal
            // 
            this.tabTerminal.Controls.Add(this.receiveTextBox);
            this.tabTerminal.Location = new System.Drawing.Point(4, 22);
            this.tabTerminal.Name = "tabTerminal";
            this.tabTerminal.Size = new System.Drawing.Size(647, 390);
            this.tabTerminal.TabIndex = 2;
            this.tabTerminal.Text = "Терминал";
            // 
            // tabCustomMode
            // 
            this.tabCustomMode.BackColor = System.Drawing.SystemColors.Control;
            this.tabCustomMode.Controls.Add(this.label30);
            this.tabCustomMode.Controls.Add(this.cmBrtMultextBox);
            this.tabCustomMode.Controls.Add(this.cmSaveCB);
            this.tabCustomMode.Controls.Add(this.cmCustomModeOnOffB);
            this.tabCustomMode.Controls.Add(this.cmLoadToDeviceB);
            this.tabCustomMode.Controls.Add(this.cmChangeB);
            this.tabCustomMode.Controls.Add(this.cmDeleteSelectedB);
            this.tabCustomMode.Controls.Add(this.cmAddB);
            this.tabCustomMode.Controls.Add(this.cmTreeView);
            this.tabCustomMode.Location = new System.Drawing.Point(4, 22);
            this.tabCustomMode.Name = "tabCustomMode";
            this.tabCustomMode.Size = new System.Drawing.Size(647, 390);
            this.tabCustomMode.TabIndex = 3;
            this.tabCustomMode.Text = "Custom Mode";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(289, 12);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(109, 13);
            this.label30.TabIndex = 17;
            this.label30.Text = "Множитель яркости";
            // 
            // cmBrtMultextBox
            // 
            this.cmBrtMultextBox.Location = new System.Drawing.Point(404, 9);
            this.cmBrtMultextBox.Name = "cmBrtMultextBox";
            this.cmBrtMultextBox.Size = new System.Drawing.Size(44, 20);
            this.cmBrtMultextBox.TabIndex = 16;
            this.cmBrtMultextBox.Text = "1";
            this.cmBrtMultextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmSaveCB
            // 
            this.cmSaveCB.AutoSize = true;
            this.cmSaveCB.Checked = true;
            this.cmSaveCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cmSaveCB.Location = new System.Drawing.Point(484, 340);
            this.cmSaveCB.Name = "cmSaveCB";
            this.cmSaveCB.Size = new System.Drawing.Size(128, 17);
            this.cmSaveCB.TabIndex = 6;
            this.cmSaveCB.Text = "Сохранить в память";
            this.cmSaveCB.UseVisualStyleBackColor = true;
            // 
            // cmCustomModeOnOffB
            // 
            this.cmCustomModeOnOffB.Location = new System.Drawing.Point(565, 363);
            this.cmCustomModeOnOffB.Name = "cmCustomModeOnOffB";
            this.cmCustomModeOnOffB.Size = new System.Drawing.Size(75, 23);
            this.cmCustomModeOnOffB.TabIndex = 5;
            this.cmCustomModeOnOffB.Text = "Включить";
            this.cmCustomModeOnOffB.UseVisualStyleBackColor = true;
            this.cmCustomModeOnOffB.Click += new System.EventHandler(this.cmCustomModeOnOffB_Click);
            // 
            // cmLoadToDeviceB
            // 
            this.cmLoadToDeviceB.Location = new System.Drawing.Point(484, 363);
            this.cmLoadToDeviceB.Name = "cmLoadToDeviceB";
            this.cmLoadToDeviceB.Size = new System.Drawing.Size(75, 23);
            this.cmLoadToDeviceB.TabIndex = 4;
            this.cmLoadToDeviceB.Text = "Загрузить";
            this.cmLoadToDeviceB.UseVisualStyleBackColor = true;
            this.cmLoadToDeviceB.Click += new System.EventHandler(this.cmLoadToDeviceB_Click);
            // 
            // cmChangeB
            // 
            this.cmChangeB.Location = new System.Drawing.Point(373, 37);
            this.cmChangeB.Name = "cmChangeB";
            this.cmChangeB.Size = new System.Drawing.Size(75, 23);
            this.cmChangeB.TabIndex = 3;
            this.cmChangeB.Text = "Изменить";
            this.cmChangeB.UseVisualStyleBackColor = true;
            this.cmChangeB.Click += new System.EventHandler(this.cmChangeB_Click);
            // 
            // cmDeleteSelectedB
            // 
            this.cmDeleteSelectedB.Location = new System.Drawing.Point(292, 66);
            this.cmDeleteSelectedB.Name = "cmDeleteSelectedB";
            this.cmDeleteSelectedB.Size = new System.Drawing.Size(133, 23);
            this.cmDeleteSelectedB.TabIndex = 2;
            this.cmDeleteSelectedB.Text = "Удалить отмеченное";
            this.cmDeleteSelectedB.UseVisualStyleBackColor = true;
            this.cmDeleteSelectedB.Click += new System.EventHandler(this.cmDeleteSelectedB_Click);
            // 
            // cmAddB
            // 
            this.cmAddB.Location = new System.Drawing.Point(292, 37);
            this.cmAddB.Name = "cmAddB";
            this.cmAddB.Size = new System.Drawing.Size(75, 23);
            this.cmAddB.TabIndex = 1;
            this.cmAddB.Text = "Добавить";
            this.cmAddB.UseVisualStyleBackColor = true;
            this.cmAddB.Click += new System.EventHandler(this.cmAddB_Click);
            // 
            // cmTreeView
            // 
            this.cmTreeView.CheckBoxes = true;
            this.cmTreeView.FullRowSelect = true;
            this.cmTreeView.HideSelection = false;
            this.cmTreeView.HotTracking = true;
            this.cmTreeView.Location = new System.Drawing.Point(3, 3);
            this.cmTreeView.Name = "cmTreeView";
            treeNode1.Name = "cmTreeViewNode0";
            treeNode1.Text = "Отметить все";
            this.cmTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.cmTreeView.ShowLines = false;
            this.cmTreeView.ShowPlusMinus = false;
            this.cmTreeView.ShowRootLines = false;
            this.cmTreeView.Size = new System.Drawing.Size(280, 383);
            this.cmTreeView.TabIndex = 0;
            this.cmTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.cmTreeView_AfterCheck);
            this.cmTreeView.DoubleClick += new System.EventHandler(this.cmTreeView_DoubleClick);
            this.cmTreeView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cmTreeView_MouseMove);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "HUlamp Terminal";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // autoConnectCB
            // 
            this.autoConnectCB.AutoSize = true;
            this.autoConnectCB.Location = new System.Drawing.Point(367, 18);
            this.autoConnectCB.Name = "autoConnectCB";
            this.autoConnectCB.Size = new System.Drawing.Size(180, 17);
            this.autoConnectCB.TabIndex = 50;
            this.autoConnectCB.Text = "Подключаться автоматически";
            this.autoConnectCB.UseVisualStyleBackColor = true;
            this.autoConnectCB.CheckedChanged += new System.EventHandler(this.autoConnectCB_CheckedChanged);
            // 
            // timerStatusUpdate
            // 
            this.timerStatusUpdate.Enabled = true;
            this.timerStatusUpdate.Interval = 1000;
            this.timerStatusUpdate.Tick += new System.EventHandler(this.timerStatusUpdate_Tick);
            // 
            // formMinimizeCB
            // 
            this.formMinimizeCB.AutoSize = true;
            this.formMinimizeCB.Location = new System.Drawing.Point(553, 18);
            this.formMinimizeCB.Name = "formMinimizeCB";
            this.formMinimizeCB.Size = new System.Drawing.Size(114, 17);
            this.formMinimizeCB.TabIndex = 51;
            this.formMinimizeCB.Text = "Минимизоровать";
            this.formMinimizeCB.UseVisualStyleBackColor = true;
            this.formMinimizeCB.CheckedChanged += new System.EventHandler(this.formMinimizeCB_CheckedChanged);
            // 
            // cmListContextMenu
            // 
            this.cmListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmMenuItemInsert,
            this.cmMenuItemDuplicate,
            this.cmMenuItemChange,
            this.cmMenuItemDelete});
            this.cmListContextMenu.Name = "cmListContextMenu";
            this.cmListContextMenu.Size = new System.Drawing.Size(147, 92);
            // 
            // cmMenuItemInsert
            // 
            this.cmMenuItemInsert.Name = "cmMenuItemInsert";
            this.cmMenuItemInsert.Size = new System.Drawing.Size(146, 22);
            this.cmMenuItemInsert.Text = "Добавить";
            this.cmMenuItemInsert.Click += new System.EventHandler(this.cmMenuItemInsert_Click);
            // 
            // cmMenuItemDuplicate
            // 
            this.cmMenuItemDuplicate.Name = "cmMenuItemDuplicate";
            this.cmMenuItemDuplicate.Size = new System.Drawing.Size(146, 22);
            this.cmMenuItemDuplicate.Text = "Дублировать";
            this.cmMenuItemDuplicate.Click += new System.EventHandler(this.cmMenuItemDuplicate_Click);
            // 
            // cmMenuItemChange
            // 
            this.cmMenuItemChange.Name = "cmMenuItemChange";
            this.cmMenuItemChange.Size = new System.Drawing.Size(146, 22);
            this.cmMenuItemChange.Text = "Изменить";
            this.cmMenuItemChange.Click += new System.EventHandler(this.cmMenuItemChange_Click);
            // 
            // cmMenuItemDelete
            // 
            this.cmMenuItemDelete.Name = "cmMenuItemDelete";
            this.cmMenuItemDelete.Size = new System.Drawing.Size(146, 22);
            this.cmMenuItemDelete.Text = "Удалить";
            this.cmMenuItemDelete.Click += new System.EventHandler(this.cmMenuItemDelete_Click);
            // 
            // retryTimer
            // 
            this.retryTimer.Enabled = true;
            this.retryTimer.Interval = 1000;
            this.retryTimer.Tick += new System.EventHandler(this.retryTimer_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nsDeviceRebootB);
            this.groupBox3.Controls.Add(this.nsBacklightSetB);
            this.groupBox3.Controls.Add(this.label33);
            this.groupBox3.Controls.Add(this.nsBacklightTB);
            this.groupBox3.Controls.Add(this.nsTransmitterStateCB);
            this.groupBox3.Controls.Add(this.nsBacklightStateCB);
            this.groupBox3.Location = new System.Drawing.Point(305, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 138);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Общие";
            // 
            // nsBacklightStateCB
            // 
            this.nsBacklightStateCB.AutoSize = true;
            this.nsBacklightStateCB.Enabled = false;
            this.nsBacklightStateCB.Location = new System.Drawing.Point(6, 20);
            this.nsBacklightStateCB.Name = "nsBacklightStateCB";
            this.nsBacklightStateCB.Size = new System.Drawing.Size(133, 17);
            this.nsBacklightStateCB.TabIndex = 1;
            this.nsBacklightStateCB.Text = "Подсветка вкл/выкл";
            this.nsBacklightStateCB.UseVisualStyleBackColor = true;
            // 
            // nsTransmitterStateCB
            // 
            this.nsTransmitterStateCB.AutoSize = true;
            this.nsTransmitterStateCB.Location = new System.Drawing.Point(6, 66);
            this.nsTransmitterStateCB.Name = "nsTransmitterStateCB";
            this.nsTransmitterStateCB.Size = new System.Drawing.Size(182, 17);
            this.nsTransmitterStateCB.TabIndex = 2;
            this.nsTransmitterStateCB.Text = "Передатчик энергии вкл/выкл";
            this.nsTransmitterStateCB.UseVisualStyleBackColor = true;
            this.nsTransmitterStateCB.CheckedChanged += new System.EventHandler(this.nsTransmitterStateCB_CheckedChanged);
            // 
            // nsBacklightTB
            // 
            this.nsBacklightTB.AutoSize = false;
            this.nsBacklightTB.Location = new System.Drawing.Point(118, 43);
            this.nsBacklightTB.Maximum = 100;
            this.nsBacklightTB.Name = "nsBacklightTB";
            this.nsBacklightTB.Size = new System.Drawing.Size(170, 20);
            this.nsBacklightTB.TabIndex = 4;
            this.nsBacklightTB.TickFrequency = 10;
            this.nsBacklightTB.Value = 5;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 44);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(106, 13);
            this.label33.TabIndex = 5;
            this.label33.Text = "Яркость подсветки";
            // 
            // nsBacklightSetB
            // 
            this.nsBacklightSetB.Location = new System.Drawing.Point(294, 40);
            this.nsBacklightSetB.Name = "nsBacklightSetB";
            this.nsBacklightSetB.Size = new System.Drawing.Size(34, 23);
            this.nsBacklightSetB.TabIndex = 6;
            this.nsBacklightSetB.Text = "OK";
            this.nsBacklightSetB.UseVisualStyleBackColor = true;
            this.nsBacklightSetB.Click += new System.EventHandler(this.nsBacklightSetB_Click);
            // 
            // nsDataUpdate
            // 
            this.nsDataUpdate.Location = new System.Drawing.Point(532, 219);
            this.nsDataUpdate.Name = "nsDataUpdate";
            this.nsDataUpdate.Size = new System.Drawing.Size(108, 23);
            this.nsDataUpdate.TabIndex = 3;
            this.nsDataUpdate.Text = "Обновить данные";
            this.nsDataUpdate.UseVisualStyleBackColor = true;
            this.nsDataUpdate.Click += new System.EventHandler(this.nsDataUpdate_Click);
            // 
            // nsDeviceRebootB
            // 
            this.nsDeviceRebootB.Location = new System.Drawing.Point(168, 105);
            this.nsDeviceRebootB.Name = "nsDeviceRebootB";
            this.nsDeviceRebootB.Size = new System.Drawing.Size(160, 23);
            this.nsDeviceRebootB.TabIndex = 7;
            this.nsDeviceRebootB.Text = "Перезагрузить устройство";
            this.nsDeviceRebootB.UseVisualStyleBackColor = true;
            this.nsDeviceRebootB.Click += new System.EventHandler(this.nsDeviceRebootB_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 459);
            this.Controls.Add(this.formMinimizeCB);
            this.Controls.Add(this.autoConnectCB);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comportDisconnectB);
            this.Controls.Add(this.comportReloadButton);
            this.Controls.Add(this.comportConnectB);
            this.Controls.Add(this.comportBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "HUlamp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.commandGroup.ResumeLayout(false);
            this.commandGroup.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabFastSettings.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nsVisualizationVolumeTB)).EndInit();
            this.tabCommands.ResumeLayout(false);
            this.tabVisualization.ResumeLayout(false);
            this.tabVisualization.PerformLayout();
            this.groupBoxVolume.ResumeLayout(false);
            this.groupBoxVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bufferTrackBar)).EndInit();
            this.tabTerminal.ResumeLayout(false);
            this.tabCustomMode.ResumeLayout(false);
            this.tabCustomMode.PerformLayout();
            this.cmListContextMenu.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nsBacklightTB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comportBox;
        private System.Windows.Forms.Button comportConnectB;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button comportReloadButton;
        private System.Windows.Forms.GroupBox commandGroup;
        private System.Windows.Forms.ComboBox commandList;
        private System.Windows.Forms.TextBox textboxParam9;
        private System.Windows.Forms.TextBox textboxParam8;
        private System.Windows.Forms.TextBox textboxParam7;
        private System.Windows.Forms.TextBox textboxParam6;
        private System.Windows.Forms.TextBox textboxParam5;
        private System.Windows.Forms.TextBox textboxParam4;
        private System.Windows.Forms.TextBox textboxParam3;
        private System.Windows.Forms.TextBox textboxParam2;
        private System.Windows.Forms.TextBox textboxParam1;
        private System.Windows.Forms.Button commandSentB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button comportDisconnectB;
        private System.Windows.Forms.Button VisualizationOnButton;
        public System.Windows.Forms.RichTextBox receiveTextBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCommands;
        private System.Windows.Forms.TabPage tabVisualization;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TextBox textBoxHFC;
        private System.Windows.Forms.TextBox textBoxMFC;
        private System.Windows.Forms.TextBox textBoxLFC;
        private System.Windows.Forms.ComboBox hightFreqComboBox;
        private System.Windows.Forms.ComboBox midFreqComboBox;
        private System.Windows.Forms.ComboBox lowFreqComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonDefaultReset;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button VisualizationButtonApply;
        private System.Windows.Forms.TabPage tabTerminal;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxLeaderB;
        private System.Windows.Forms.TextBox textBoxLeaderG;
        private System.Windows.Forms.TextBox textBoxLeaderR;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxFollowerB;
        private System.Windows.Forms.TextBox textBoxFollowerG;
        private System.Windows.Forms.TextBox textBoxFollowerR;
        private System.Windows.Forms.TextBox textBoxVolC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxFreqH;
        private System.Windows.Forms.TextBox textBoxFreqM;
        private System.Windows.Forms.TextBox textBoxFreqL;
        private System.Windows.Forms.TrackBar bufferTrackBar;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxFallB;
        private System.Windows.Forms.TextBox textBoxFallG;
        private System.Windows.Forms.TextBox textBoxFallR;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxRiseB;
        private System.Windows.Forms.TextBox textBoxRiseG;
        private System.Windows.Forms.TextBox textBoxRiseR;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBoxColB;
        private System.Windows.Forms.TextBox textBoxColG;
        private System.Windows.Forms.TextBox textBoxColR;
        private System.Windows.Forms.TextBox textBoxColV;
        private System.Windows.Forms.CheckBox autoVolumeCheckBox;
        private System.Windows.Forms.Label brightnessTrackBarLabel;
        private System.Windows.Forms.GroupBox groupBoxVolume;
        private System.Windows.Forms.TextBox textBoxFallV;
        private System.Windows.Forms.TextBox textBoxRiseV;
        private System.Windows.Forms.TextBox textBoxAutoVolCounter;
        private System.Windows.Forms.TextBox textBoxAutoVolTarget;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxAutoVolStep;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBoxDataSendTim;
        private System.Windows.Forms.TextBox textBoxDataProcessTim;
        private System.Windows.Forms.Label bufferTrackBarLabel;
        private System.Windows.Forms.CheckBox autoConnectCB;
        private System.Windows.Forms.CheckBox autoEnableVisCB;
        private System.Windows.Forms.RichTextBox commandInfoTextBox;
        private System.Windows.Forms.Label statLab8;
        private System.Windows.Forms.Label statLab4;
        private System.Windows.Forms.Label statLab7;
        private System.Windows.Forms.Label statLab6;
        private System.Windows.Forms.Label statLab5;
        private System.Windows.Forms.Label statLab3;
        private System.Windows.Forms.Label statLab2;
        private System.Windows.Forms.Label statLab1;
        private System.Windows.Forms.CheckBox statisticCheckBox;
        private System.Windows.Forms.Label statLab12;
        private System.Windows.Forms.Label statLab11;
        private System.Windows.Forms.Label statLab10;
        private System.Windows.Forms.Label statLab9;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBoxVolumeUpdateTim;
        private System.Windows.Forms.ComboBox volumeComboBox;
        private System.Windows.Forms.Timer timerStatusUpdate;
        private System.Windows.Forms.CheckBox formMinimizeCB;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.TabPage tabCustomMode;
        private System.Windows.Forms.Button cmAddB;
        private System.Windows.Forms.ContextMenuStrip cmListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmMenuItemChange;
        private System.Windows.Forms.ToolStripMenuItem cmMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem cmMenuItemInsert;
        private System.Windows.Forms.Button cmChangeB;
        private System.Windows.Forms.Button cmDeleteSelectedB;
        private System.Windows.Forms.CheckBox cmSaveCB;
        private System.Windows.Forms.Button cmCustomModeOnOffB;
        private System.Windows.Forms.Button cmLoadToDeviceB;
        private System.Windows.Forms.ToolStripMenuItem cmMenuItemDuplicate;
        private System.Windows.Forms.TreeView cmTreeView;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox cmBrtMultextBox;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBoxVolPow;
        private System.Windows.Forms.Timer retryTimer;
        private System.Windows.Forms.Label statLabD3;
        private System.Windows.Forms.Label statLabD2;
        private System.Windows.Forms.Label statLabD1;
        private System.Windows.Forms.Button autoVolumeResB;
        private System.Windows.Forms.TabPage tabFastSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton nsModeRB5;
        private System.Windows.Forms.RadioButton nsModeRB4;
        private System.Windows.Forms.RadioButton nsModeRB3;
        private System.Windows.Forms.RadioButton nsModeRB2;
        private System.Windows.Forms.RadioButton nsModeRB1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TrackBar nsVisualizationVolumeTB;
        private System.Windows.Forms.CheckBox nsVisualizationAutoVol;
        private System.Windows.Forms.CheckBox nsVisualizationStartOn;
        private System.Windows.Forms.CheckBox nsVisualizationOnCB;
        private System.Windows.Forms.Button nsChangeColorB;
        private System.Windows.Forms.Panel nsColorPanel;
        private System.Windows.Forms.Button nsSetModeB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox nsBacklightStateCB;
        private System.Windows.Forms.CheckBox nsTransmitterStateCB;
        private System.Windows.Forms.Button nsBacklightSetB;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TrackBar nsBacklightTB;
        private System.Windows.Forms.Button nsDataUpdate;
        private System.Windows.Forms.Button nsDeviceRebootB;
    }
}

