namespace Packet
{
    partial class Com_Form
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
            this.groupBoxBaudRate = new System.Windows.Forms.GroupBox();
            this.radioButton57600 = new System.Windows.Forms.RadioButton();
            this.radioButton38400 = new System.Windows.Forms.RadioButton();
            this.radioButton19200 = new System.Windows.Forms.RadioButton();
            this.radioButton1200 = new System.Windows.Forms.RadioButton();
            this.radioButton9600 = new System.Windows.Forms.RadioButton();
            this.radioButton600 = new System.Windows.Forms.RadioButton();
            this.radioButton4800 = new System.Windows.Forms.RadioButton();
            this.radioButton300 = new System.Windows.Forms.RadioButton();
            this.radioButton2400 = new System.Windows.Forms.RadioButton();
            this.radioButton110 = new System.Windows.Forms.RadioButton();
            this.groupBoxDataBits = new System.Windows.Forms.GroupBox();
            this.radioButton_Data_8 = new System.Windows.Forms.RadioButton();
            this.radioButton_Data_7 = new System.Windows.Forms.RadioButton();
            this.radioButton_Data_6 = new System.Windows.Forms.RadioButton();
            this.radioButton_Data_5 = new System.Windows.Forms.RadioButton();
            this.groupBoxParity = new System.Windows.Forms.GroupBox();
            this.radioButton_Parity_Space = new System.Windows.Forms.RadioButton();
            this.radioButton_Parity_Mark = new System.Windows.Forms.RadioButton();
            this.radioButton_Parity_Even = new System.Windows.Forms.RadioButton();
            this.radioButton_Parity_Odd = new System.Windows.Forms.RadioButton();
            this.radioButton_Parity_None = new System.Windows.Forms.RadioButton();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.groupBoxStopBits = new System.Windows.Forms.GroupBox();
            this.radioButton_Stop_2 = new System.Windows.Forms.RadioButton();
            this.radioButton_Stop_1_5 = new System.Windows.Forms.RadioButton();
            this.radioButton_Stop_1 = new System.Windows.Forms.RadioButton();
            this.groupBoxFlow = new System.Windows.Forms.GroupBox();
            this.radioButton_Flow_None = new System.Windows.Forms.RadioButton();
            this.radioButton_Flow_Hardware = new System.Windows.Forms.RadioButton();
            this.radioButton_Flow_Xon_Xoff = new System.Windows.Forms.RadioButton();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.Port_label = new System.Windows.Forms.Label();
            this.groupBoxBaudRate.SuspendLayout();
            this.groupBoxDataBits.SuspendLayout();
            this.groupBoxParity.SuspendLayout();
            this.groupBoxStopBits.SuspendLayout();
            this.groupBoxFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBaudRate
            // 
            this.groupBoxBaudRate.Controls.Add(this.radioButton57600);
            this.groupBoxBaudRate.Controls.Add(this.radioButton38400);
            this.groupBoxBaudRate.Controls.Add(this.radioButton19200);
            this.groupBoxBaudRate.Controls.Add(this.radioButton1200);
            this.groupBoxBaudRate.Controls.Add(this.radioButton9600);
            this.groupBoxBaudRate.Controls.Add(this.radioButton600);
            this.groupBoxBaudRate.Controls.Add(this.radioButton4800);
            this.groupBoxBaudRate.Controls.Add(this.radioButton300);
            this.groupBoxBaudRate.Controls.Add(this.radioButton2400);
            this.groupBoxBaudRate.Controls.Add(this.radioButton110);
            this.groupBoxBaudRate.Location = new System.Drawing.Point(40, 44);
            this.groupBoxBaudRate.Name = "groupBoxBaudRate";
            this.groupBoxBaudRate.Size = new System.Drawing.Size(343, 102);
            this.groupBoxBaudRate.TabIndex = 0;
            this.groupBoxBaudRate.TabStop = false;
            this.groupBoxBaudRate.Text = "Baud Rate";
            // 
            // radioButton57600
            // 
            this.radioButton57600.AutoSize = true;
            this.radioButton57600.Location = new System.Drawing.Point(274, 64);
            this.radioButton57600.Name = "radioButton57600";
            this.radioButton57600.Size = new System.Drawing.Size(55, 17);
            this.radioButton57600.TabIndex = 9;
            this.radioButton57600.TabStop = true;
            this.radioButton57600.Text = "57600";
            this.radioButton57600.UseVisualStyleBackColor = true;
            this.radioButton57600.CheckedChanged += new System.EventHandler(this.radioButton57600_CheckedChanged);
            // 
            // radioButton38400
            // 
            this.radioButton38400.AutoSize = true;
            this.radioButton38400.Location = new System.Drawing.Point(209, 64);
            this.radioButton38400.Name = "radioButton38400";
            this.radioButton38400.Size = new System.Drawing.Size(55, 17);
            this.radioButton38400.TabIndex = 8;
            this.radioButton38400.TabStop = true;
            this.radioButton38400.Text = "38400";
            this.radioButton38400.UseVisualStyleBackColor = true;
            this.radioButton38400.CheckedChanged += new System.EventHandler(this.radioButton38400_CheckedChanged);
            // 
            // radioButton19200
            // 
            this.radioButton19200.AutoSize = true;
            this.radioButton19200.Location = new System.Drawing.Point(144, 64);
            this.radioButton19200.Name = "radioButton19200";
            this.radioButton19200.Size = new System.Drawing.Size(55, 17);
            this.radioButton19200.TabIndex = 7;
            this.radioButton19200.TabStop = true;
            this.radioButton19200.Text = "19200";
            this.radioButton19200.UseVisualStyleBackColor = true;
            this.radioButton19200.CheckedChanged += new System.EventHandler(this.radioButton19200_CheckedChanged);
            // 
            // radioButton1200
            // 
            this.radioButton1200.AutoSize = true;
            this.radioButton1200.Location = new System.Drawing.Point(209, 41);
            this.radioButton1200.Name = "radioButton1200";
            this.radioButton1200.Size = new System.Drawing.Size(49, 17);
            this.radioButton1200.TabIndex = 6;
            this.radioButton1200.TabStop = true;
            this.radioButton1200.Text = "1200";
            this.radioButton1200.UseVisualStyleBackColor = true;
            this.radioButton1200.CheckedChanged += new System.EventHandler(this.radioButton1200_CheckedChanged);
            // 
            // radioButton9600
            // 
            this.radioButton9600.AutoSize = true;
            this.radioButton9600.Location = new System.Drawing.Point(85, 64);
            this.radioButton9600.Name = "radioButton9600";
            this.radioButton9600.Size = new System.Drawing.Size(49, 17);
            this.radioButton9600.TabIndex = 5;
            this.radioButton9600.TabStop = true;
            this.radioButton9600.Text = "9600";
            this.radioButton9600.UseVisualStyleBackColor = true;
            this.radioButton9600.CheckedChanged += new System.EventHandler(this.radioButton9600_CheckedChanged);
            // 
            // radioButton600
            // 
            this.radioButton600.AutoSize = true;
            this.radioButton600.Location = new System.Drawing.Point(144, 41);
            this.radioButton600.Name = "radioButton600";
            this.radioButton600.Size = new System.Drawing.Size(43, 17);
            this.radioButton600.TabIndex = 4;
            this.radioButton600.TabStop = true;
            this.radioButton600.Text = "600";
            this.radioButton600.UseVisualStyleBackColor = true;
            this.radioButton600.CheckedChanged += new System.EventHandler(this.radioButton600_CheckedChanged);
            // 
            // radioButton4800
            // 
            this.radioButton4800.AutoSize = true;
            this.radioButton4800.Location = new System.Drawing.Point(25, 64);
            this.radioButton4800.Name = "radioButton4800";
            this.radioButton4800.Size = new System.Drawing.Size(49, 17);
            this.radioButton4800.TabIndex = 3;
            this.radioButton4800.TabStop = true;
            this.radioButton4800.Text = "4800";
            this.radioButton4800.UseVisualStyleBackColor = true;
            this.radioButton4800.CheckedChanged += new System.EventHandler(this.radioButton4800_CheckedChanged);
            // 
            // radioButton300
            // 
            this.radioButton300.AutoSize = true;
            this.radioButton300.Location = new System.Drawing.Point(85, 41);
            this.radioButton300.Name = "radioButton300";
            this.radioButton300.Size = new System.Drawing.Size(43, 17);
            this.radioButton300.TabIndex = 2;
            this.radioButton300.TabStop = true;
            this.radioButton300.Text = "300";
            this.radioButton300.UseVisualStyleBackColor = true;
            this.radioButton300.CheckedChanged += new System.EventHandler(this.radioButton300_CheckedChanged);
            // 
            // radioButton2400
            // 
            this.radioButton2400.AutoSize = true;
            this.radioButton2400.Location = new System.Drawing.Point(274, 41);
            this.radioButton2400.Name = "radioButton2400";
            this.radioButton2400.Size = new System.Drawing.Size(49, 17);
            this.radioButton2400.TabIndex = 1;
            this.radioButton2400.TabStop = true;
            this.radioButton2400.Text = "2400";
            this.radioButton2400.UseVisualStyleBackColor = true;
            this.radioButton2400.CheckedChanged += new System.EventHandler(this.radioButton2400_CheckedChanged);
            // 
            // radioButton110
            // 
            this.radioButton110.AutoSize = true;
            this.radioButton110.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.radioButton110.Location = new System.Drawing.Point(25, 41);
            this.radioButton110.Name = "radioButton110";
            this.radioButton110.Size = new System.Drawing.Size(43, 17);
            this.radioButton110.TabIndex = 0;
            this.radioButton110.TabStop = true;
            this.radioButton110.Text = "110";
            this.radioButton110.UseVisualStyleBackColor = true;
            this.radioButton110.CheckedChanged += new System.EventHandler(this.radioButton110_CheckedChanged);
            // 
            // groupBoxDataBits
            // 
            this.groupBoxDataBits.Controls.Add(this.radioButton_Data_8);
            this.groupBoxDataBits.Controls.Add(this.radioButton_Data_7);
            this.groupBoxDataBits.Controls.Add(this.radioButton_Data_6);
            this.groupBoxDataBits.Controls.Add(this.radioButton_Data_5);
            this.groupBoxDataBits.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBoxDataBits.Location = new System.Drawing.Point(40, 173);
            this.groupBoxDataBits.Name = "groupBoxDataBits";
            this.groupBoxDataBits.Size = new System.Drawing.Size(189, 83);
            this.groupBoxDataBits.TabIndex = 1;
            this.groupBoxDataBits.TabStop = false;
            this.groupBoxDataBits.Text = "Data Bits";
            // 
            // radioButton_Data_8
            // 
            this.radioButton_Data_8.AutoSize = true;
            this.radioButton_Data_8.Location = new System.Drawing.Point(139, 42);
            this.radioButton_Data_8.Name = "radioButton_Data_8";
            this.radioButton_Data_8.Size = new System.Drawing.Size(31, 17);
            this.radioButton_Data_8.TabIndex = 4;
            this.radioButton_Data_8.TabStop = true;
            this.radioButton_Data_8.Text = "8";
            this.radioButton_Data_8.UseVisualStyleBackColor = true;
            this.radioButton_Data_8.CheckedChanged += new System.EventHandler(this.radioButton_Data_8_CheckedChanged);
            // 
            // radioButton_Data_7
            // 
            this.radioButton_Data_7.AutoSize = true;
            this.radioButton_Data_7.Location = new System.Drawing.Point(102, 42);
            this.radioButton_Data_7.Name = "radioButton_Data_7";
            this.radioButton_Data_7.Size = new System.Drawing.Size(31, 17);
            this.radioButton_Data_7.TabIndex = 3;
            this.radioButton_Data_7.TabStop = true;
            this.radioButton_Data_7.Text = "7";
            this.radioButton_Data_7.UseVisualStyleBackColor = true;
            this.radioButton_Data_7.CheckedChanged += new System.EventHandler(this.radioButton_Data_7_CheckedChanged);
            // 
            // radioButton_Data_6
            // 
            this.radioButton_Data_6.AutoSize = true;
            this.radioButton_Data_6.Location = new System.Drawing.Point(62, 41);
            this.radioButton_Data_6.Name = "radioButton_Data_6";
            this.radioButton_Data_6.Size = new System.Drawing.Size(31, 17);
            this.radioButton_Data_6.TabIndex = 2;
            this.radioButton_Data_6.TabStop = true;
            this.radioButton_Data_6.Text = "6";
            this.radioButton_Data_6.UseVisualStyleBackColor = true;
            this.radioButton_Data_6.CheckedChanged += new System.EventHandler(this.radioButton_Data_6_CheckedChanged);
            // 
            // radioButton_Data_5
            // 
            this.radioButton_Data_5.AutoSize = true;
            this.radioButton_Data_5.Location = new System.Drawing.Point(25, 41);
            this.radioButton_Data_5.Name = "radioButton_Data_5";
            this.radioButton_Data_5.Size = new System.Drawing.Size(31, 17);
            this.radioButton_Data_5.TabIndex = 1;
            this.radioButton_Data_5.TabStop = true;
            this.radioButton_Data_5.Text = "5";
            this.radioButton_Data_5.UseVisualStyleBackColor = true;
            this.radioButton_Data_5.CheckedChanged += new System.EventHandler(this.radioButton_Data_5_CheckedChanged);
            // 
            // groupBoxParity
            // 
            this.groupBoxParity.Controls.Add(this.radioButton_Parity_Space);
            this.groupBoxParity.Controls.Add(this.radioButton_Parity_Mark);
            this.groupBoxParity.Controls.Add(this.radioButton_Parity_Even);
            this.groupBoxParity.Controls.Add(this.radioButton_Parity_Odd);
            this.groupBoxParity.Controls.Add(this.radioButton_Parity_None);
            this.groupBoxParity.Location = new System.Drawing.Point(40, 277);
            this.groupBoxParity.Name = "groupBoxParity";
            this.groupBoxParity.Size = new System.Drawing.Size(91, 152);
            this.groupBoxParity.TabIndex = 2;
            this.groupBoxParity.TabStop = false;
            this.groupBoxParity.Text = "Parity";
            // 
            // radioButton_Parity_Space
            // 
            this.radioButton_Parity_Space.AutoSize = true;
            this.radioButton_Parity_Space.Location = new System.Drawing.Point(26, 121);
            this.radioButton_Parity_Space.Name = "radioButton_Parity_Space";
            this.radioButton_Parity_Space.Size = new System.Drawing.Size(56, 17);
            this.radioButton_Parity_Space.TabIndex = 6;
            this.radioButton_Parity_Space.TabStop = true;
            this.radioButton_Parity_Space.Text = "Space";
            this.radioButton_Parity_Space.UseVisualStyleBackColor = true;
            this.radioButton_Parity_Space.CheckedChanged += new System.EventHandler(this.radioButton_Parity_Space_CheckedChanged);
            // 
            // radioButton_Parity_Mark
            // 
            this.radioButton_Parity_Mark.AutoSize = true;
            this.radioButton_Parity_Mark.Location = new System.Drawing.Point(25, 98);
            this.radioButton_Parity_Mark.Name = "radioButton_Parity_Mark";
            this.radioButton_Parity_Mark.Size = new System.Drawing.Size(49, 17);
            this.radioButton_Parity_Mark.TabIndex = 5;
            this.radioButton_Parity_Mark.TabStop = true;
            this.radioButton_Parity_Mark.Text = "Mark";
            this.radioButton_Parity_Mark.UseVisualStyleBackColor = true;
            this.radioButton_Parity_Mark.CheckedChanged += new System.EventHandler(this.radioButton_Parity_Mark_CheckedChanged);
            // 
            // radioButton_Parity_Even
            // 
            this.radioButton_Parity_Even.AutoSize = true;
            this.radioButton_Parity_Even.Location = new System.Drawing.Point(25, 75);
            this.radioButton_Parity_Even.Name = "radioButton_Parity_Even";
            this.radioButton_Parity_Even.Size = new System.Drawing.Size(50, 17);
            this.radioButton_Parity_Even.TabIndex = 4;
            this.radioButton_Parity_Even.TabStop = true;
            this.radioButton_Parity_Even.Text = "Even";
            this.radioButton_Parity_Even.UseVisualStyleBackColor = true;
            this.radioButton_Parity_Even.CheckedChanged += new System.EventHandler(this.radioButton_Parity_Even_CheckedChanged);
            // 
            // radioButton_Parity_Odd
            // 
            this.radioButton_Parity_Odd.AutoSize = true;
            this.radioButton_Parity_Odd.Location = new System.Drawing.Point(25, 52);
            this.radioButton_Parity_Odd.Name = "radioButton_Parity_Odd";
            this.radioButton_Parity_Odd.Size = new System.Drawing.Size(45, 17);
            this.radioButton_Parity_Odd.TabIndex = 3;
            this.radioButton_Parity_Odd.TabStop = true;
            this.radioButton_Parity_Odd.Text = "Odd";
            this.radioButton_Parity_Odd.UseVisualStyleBackColor = true;
            this.radioButton_Parity_Odd.CheckedChanged += new System.EventHandler(this.radioButton_Parity_Odd_CheckedChanged);
            // 
            // radioButton_Parity_None
            // 
            this.radioButton_Parity_None.AutoSize = true;
            this.radioButton_Parity_None.Location = new System.Drawing.Point(26, 29);
            this.radioButton_Parity_None.Name = "radioButton_Parity_None";
            this.radioButton_Parity_None.Size = new System.Drawing.Size(51, 17);
            this.radioButton_Parity_None.TabIndex = 2;
            this.radioButton_Parity_None.TabStop = true;
            this.radioButton_Parity_None.Text = "None";
            this.radioButton_Parity_None.UseVisualStyleBackColor = true;
            this.radioButton_Parity_None.CheckedChanged += new System.EventHandler(this.radioButton_Parity_None_CheckedChanged);
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(403, 79);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 3;
            this.ok_button.Text = "Done";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(403, 123);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 4;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // groupBoxStopBits
            // 
            this.groupBoxStopBits.Controls.Add(this.radioButton_Stop_2);
            this.groupBoxStopBits.Controls.Add(this.radioButton_Stop_1_5);
            this.groupBoxStopBits.Controls.Add(this.radioButton_Stop_1);
            this.groupBoxStopBits.Location = new System.Drawing.Point(249, 186);
            this.groupBoxStopBits.Name = "groupBoxStopBits";
            this.groupBoxStopBits.Size = new System.Drawing.Size(172, 70);
            this.groupBoxStopBits.TabIndex = 5;
            this.groupBoxStopBits.TabStop = false;
            this.groupBoxStopBits.Text = "Stop Bits";
            // 
            // radioButton_Stop_2
            // 
            this.radioButton_Stop_2.AutoSize = true;
            this.radioButton_Stop_2.Location = new System.Drawing.Point(122, 30);
            this.radioButton_Stop_2.Name = "radioButton_Stop_2";
            this.radioButton_Stop_2.Size = new System.Drawing.Size(31, 17);
            this.radioButton_Stop_2.TabIndex = 6;
            this.radioButton_Stop_2.TabStop = true;
            this.radioButton_Stop_2.Text = "2";
            this.radioButton_Stop_2.UseVisualStyleBackColor = true;
            this.radioButton_Stop_2.CheckedChanged += new System.EventHandler(this.radioButton_Stop_2_CheckedChanged);
            // 
            // radioButton_Stop_1_5
            // 
            this.radioButton_Stop_1_5.AutoSize = true;
            this.radioButton_Stop_1_5.Location = new System.Drawing.Point(65, 30);
            this.radioButton_Stop_1_5.Name = "radioButton_Stop_1_5";
            this.radioButton_Stop_1_5.Size = new System.Drawing.Size(40, 17);
            this.radioButton_Stop_1_5.TabIndex = 5;
            this.radioButton_Stop_1_5.TabStop = true;
            this.radioButton_Stop_1_5.Text = "1.5";
            this.radioButton_Stop_1_5.UseVisualStyleBackColor = true;
            this.radioButton_Stop_1_5.CheckedChanged += new System.EventHandler(this.radioButton_Stop_1_5_CheckedChanged);
            // 
            // radioButton_Stop_1
            // 
            this.radioButton_Stop_1.AutoSize = true;
            this.radioButton_Stop_1.Location = new System.Drawing.Point(18, 30);
            this.radioButton_Stop_1.Name = "radioButton_Stop_1";
            this.radioButton_Stop_1.Size = new System.Drawing.Size(31, 17);
            this.radioButton_Stop_1.TabIndex = 4;
            this.radioButton_Stop_1.TabStop = true;
            this.radioButton_Stop_1.Text = "1";
            this.radioButton_Stop_1.UseVisualStyleBackColor = true;
            this.radioButton_Stop_1.CheckedChanged += new System.EventHandler(this.radioButton_Stop_1_CheckedChanged);
            // 
            // groupBoxFlow
            // 
            this.groupBoxFlow.Controls.Add(this.radioButton_Flow_None);
            this.groupBoxFlow.Controls.Add(this.radioButton_Flow_Hardware);
            this.groupBoxFlow.Controls.Add(this.radioButton_Flow_Xon_Xoff);
            this.groupBoxFlow.Location = new System.Drawing.Point(157, 277);
            this.groupBoxFlow.Name = "groupBoxFlow";
            this.groupBoxFlow.Size = new System.Drawing.Size(110, 111);
            this.groupBoxFlow.TabIndex = 6;
            this.groupBoxFlow.TabStop = false;
            this.groupBoxFlow.Text = "Flow Control";
            // 
            // radioButton_Flow_None
            // 
            this.radioButton_Flow_None.AutoSize = true;
            this.radioButton_Flow_None.Location = new System.Drawing.Point(21, 75);
            this.radioButton_Flow_None.Name = "radioButton_Flow_None";
            this.radioButton_Flow_None.Size = new System.Drawing.Size(51, 17);
            this.radioButton_Flow_None.TabIndex = 8;
            this.radioButton_Flow_None.TabStop = true;
            this.radioButton_Flow_None.Text = "None";
            this.radioButton_Flow_None.UseVisualStyleBackColor = true;
            this.radioButton_Flow_None.CheckedChanged += new System.EventHandler(this.radioButton_Flow_None_CheckedChanged);
            // 
            // radioButton_Flow_Hardware
            // 
            this.radioButton_Flow_Hardware.AutoSize = true;
            this.radioButton_Flow_Hardware.Location = new System.Drawing.Point(21, 52);
            this.radioButton_Flow_Hardware.Name = "radioButton_Flow_Hardware";
            this.radioButton_Flow_Hardware.Size = new System.Drawing.Size(71, 17);
            this.radioButton_Flow_Hardware.TabIndex = 7;
            this.radioButton_Flow_Hardware.TabStop = true;
            this.radioButton_Flow_Hardware.Text = "Hardware";
            this.radioButton_Flow_Hardware.UseVisualStyleBackColor = true;
            this.radioButton_Flow_Hardware.CheckedChanged += new System.EventHandler(this.radioButton_Flow_Hardware_CheckedChanged);
            // 
            // radioButton_Flow_Xon_Xoff
            // 
            this.radioButton_Flow_Xon_Xoff.AutoSize = true;
            this.radioButton_Flow_Xon_Xoff.Location = new System.Drawing.Point(21, 29);
            this.radioButton_Flow_Xon_Xoff.Name = "radioButton_Flow_Xon_Xoff";
            this.radioButton_Flow_Xon_Xoff.Size = new System.Drawing.Size(77, 17);
            this.radioButton_Flow_Xon_Xoff.TabIndex = 6;
            this.radioButton_Flow_Xon_Xoff.TabStop = true;
            this.radioButton_Flow_Xon_Xoff.Text = "Xon /  Xoff";
            this.radioButton_Flow_Xon_Xoff.UseVisualStyleBackColor = true;
            this.radioButton_Flow_Xon_Xoff.CheckedChanged += new System.EventHandler(this.radioButton_Flow_Xon_Xoff_CheckedChanged);
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(300, 302);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPort.TabIndex = 7;
            // 
            // Port_label
            // 
            this.Port_label.AutoSize = true;
            this.Port_label.Location = new System.Drawing.Point(297, 277);
            this.Port_label.Name = "Port_label";
            this.Port_label.Size = new System.Drawing.Size(31, 13);
            this.Port_label.TabIndex = 8;
            this.Port_label.Text = "Ports";
            // 
            // Com_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(520, 455);
            this.Controls.Add(this.Port_label);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.groupBoxFlow);
            this.Controls.Add(this.groupBoxStopBits);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.groupBoxParity);
            this.Controls.Add(this.groupBoxDataBits);
            this.Controls.Add(this.groupBoxBaudRate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Com_Form";
            this.Text = "Com Port Config";
            this.Load += new System.EventHandler(this.Com_Form_Load);
            this.groupBoxBaudRate.ResumeLayout(false);
            this.groupBoxBaudRate.PerformLayout();
            this.groupBoxDataBits.ResumeLayout(false);
            this.groupBoxDataBits.PerformLayout();
            this.groupBoxParity.ResumeLayout(false);
            this.groupBoxParity.PerformLayout();
            this.groupBoxStopBits.ResumeLayout(false);
            this.groupBoxStopBits.PerformLayout();
            this.groupBoxFlow.ResumeLayout(false);
            this.groupBoxFlow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBaudRate;
        private System.Windows.Forms.RadioButton radioButton110;
        private System.Windows.Forms.RadioButton radioButton19200;
        private System.Windows.Forms.RadioButton radioButton1200;
        private System.Windows.Forms.RadioButton radioButton9600;
        private System.Windows.Forms.RadioButton radioButton600;
        private System.Windows.Forms.RadioButton radioButton4800;
        private System.Windows.Forms.RadioButton radioButton300;
        private System.Windows.Forms.GroupBox groupBoxDataBits;
        private System.Windows.Forms.RadioButton radioButton_Data_8;
        private System.Windows.Forms.RadioButton radioButton_Data_7;
        private System.Windows.Forms.RadioButton radioButton_Data_6;
        private System.Windows.Forms.RadioButton radioButton_Data_5;
        private System.Windows.Forms.GroupBox groupBoxParity;
        private System.Windows.Forms.RadioButton radioButton_Parity_Space;
        private System.Windows.Forms.RadioButton radioButton_Parity_Mark;
        private System.Windows.Forms.RadioButton radioButton_Parity_Even;
        private System.Windows.Forms.RadioButton radioButton_Parity_Odd;
        private System.Windows.Forms.RadioButton radioButton_Parity_None;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.GroupBox groupBoxStopBits;
        private System.Windows.Forms.RadioButton radioButton_Stop_2;
        private System.Windows.Forms.RadioButton radioButton_Stop_1_5;
        private System.Windows.Forms.RadioButton radioButton_Stop_1;
        private System.Windows.Forms.GroupBox groupBoxFlow;
        private System.Windows.Forms.RadioButton radioButton_Flow_None;
        private System.Windows.Forms.RadioButton radioButton_Flow_Hardware;
        private System.Windows.Forms.RadioButton radioButton_Flow_Xon_Xoff;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Label Port_label;
        private System.Windows.Forms.RadioButton radioButton57600;
        private System.Windows.Forms.RadioButton radioButton38400;
        private System.Windows.Forms.RadioButton radioButton2400;
    }
}