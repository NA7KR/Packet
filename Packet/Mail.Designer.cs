namespace Packet
{
    partial class Mail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.keepQTYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.cleanListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mailConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.routeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(202, 565);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(346, 565);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 2;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.AllowUserToAddRows = false;
            this.DataGridView1.AllowUserToDeleteRows = false;
            this.DataGridView1.AllowUserToResizeColumns = false;
            this.DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle9;
            this.DataGridView1.Location = new System.Drawing.Point(0, 27);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.Size = new System.Drawing.Size(1208, 521);
            this.DataGridView1.TabIndex = 3;
            this.DataGridView1.Visible = false;
            this.DataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DataGridView1_Scroll);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.setupToolStripMenuItem,
            this.mailConfigToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1380, 24);
            this.menuStrip1.TabIndex = 5;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keepTimeToolStripMenuItem,
            this.keepQTYToolStripMenuItem,
            this.cleanListToolStripMenuItem});
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            // 
            // keepTimeToolStripMenuItem
            // 
            this.keepTimeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.keepTimeToolStripMenuItem.Name = "keepTimeToolStripMenuItem";
            this.keepTimeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.keepTimeToolStripMenuItem.Text = "Keep Time";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBox1.AutoToolTip = true;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Off",
            "30 Day",
            "90 Day",
            "1 Year",
            "Custom"});
            this.toolStripComboBox1.MergeIndex = 0;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.Text = "Off";
            // 
            // keepQTYToolStripMenuItem
            // 
            this.keepQTYToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.keepQTYToolStripMenuItem.Name = "keepQTYToolStripMenuItem";
            this.keepQTYToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.keepQTYToolStripMenuItem.Text = "Keep QTY";
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBox2.AutoToolTip = true;
            this.toolStripComboBox2.Items.AddRange(new object[] {
            "Off",
            "1000",
            "10000",
            "Custom"});
            this.toolStripComboBox2.MergeIndex = 0;
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox2.Text = "Off";
            this.toolStripComboBox2.ToolTipText = "how log to keep list";
            // 
            // cleanListToolStripMenuItem
            // 
            this.cleanListToolStripMenuItem.Name = "cleanListToolStripMenuItem";
            this.cleanListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cleanListToolStripMenuItem.Text = "Clean List";
            // 
            // mailConfigToolStripMenuItem
            // 
            this.mailConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toToolStripMenuItem,
            this.routeToolStripMenuItem,
            this.fromToolStripMenuItem,
            this.subjectToolStripMenuItem});
            this.mailConfigToolStripMenuItem.Name = "mailConfigToolStripMenuItem";
            this.mailConfigToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.mailConfigToolStripMenuItem.Text = "Mail Config";
            // 
            // toToolStripMenuItem
            // 
            this.toToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.toToolStripMenuItem.Name = "toToolStripMenuItem";
            this.toToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.toToolStripMenuItem.Text = "To";
            // 
            // routeToolStripMenuItem
            // 
            this.routeToolStripMenuItem.Name = "routeToolStripMenuItem";
            this.routeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.routeToolStripMenuItem.Text = "Route";
            // 
            // fromToolStripMenuItem
            // 
            this.fromToolStripMenuItem.Name = "fromToolStripMenuItem";
            this.fromToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fromToolStripMenuItem.Text = "From";
            // 
            // subjectToolStripMenuItem
            // 
            this.subjectToolStripMenuItem.Name = "subjectToolStripMenuItem";
            this.subjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.subjectToolStripMenuItem.Text = "Subject";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Config";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Mail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 657);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Mail";
            this.Text = "Mail";
            this.Load += new System.EventHandler(this.Mail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.DataGridView DataGridView1;
        protected System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keepTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem keepQTYToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox2;
        private System.Windows.Forms.ToolStripMenuItem cleanListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mailConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem routeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}