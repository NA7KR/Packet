using System.ComponentModel;
using System.Windows.Forms;

namespace Packet
{
    partial class Mail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mail));
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxTime = new System.Windows.Forms.ToolStripComboBox();
            this.keepQTYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxQTY = new System.Windows.Forms.ToolStripComboBox();
            this.cleanListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.mailConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSubject = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTO = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRoute = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemToDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.Location = new System.Drawing.Point(0, 27);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
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
            this.toolStripComboBoxTime});
            this.keepTimeToolStripMenuItem.Name = "keepTimeToolStripMenuItem";
            this.keepTimeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.keepTimeToolStripMenuItem.Text = "Keep Time";
            // 
            // toolStripComboBoxTime
            // 
            this.toolStripComboBoxTime.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.toolStripComboBoxTime.AutoToolTip = true;
            this.toolStripComboBoxTime.MergeIndex = 0;
            this.toolStripComboBoxTime.Name = "toolStripComboBoxTime";
            this.toolStripComboBoxTime.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxTime.Text = "Off";
            this.toolStripComboBoxTime.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // keepQTYToolStripMenuItem
            // 
            this.keepQTYToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxQTY});
            this.keepQTYToolStripMenuItem.Name = "keepQTYToolStripMenuItem";
            this.keepQTYToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.keepQTYToolStripMenuItem.Text = "Keep QTY";
            // 
            // toolStripComboBoxQTY
            // 
            this.toolStripComboBoxQTY.Name = "toolStripComboBoxQTY";
            this.toolStripComboBoxQTY.Size = new System.Drawing.Size(100, 23);
            this.toolStripComboBoxQTY.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxQTY_SelectedIndexChanged);
            // 
            // cleanListToolStripMenuItem
            // 
            this.cleanListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItemClear});
            this.cleanListToolStripMenuItem.Name = "cleanListToolStripMenuItem";
            this.cleanListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cleanListToolStripMenuItem.Text = "Clean List";
            // 
            // allToolStripMenuItemClear
            // 
            this.allToolStripMenuItemClear.Name = "allToolStripMenuItemClear";
            this.allToolStripMenuItemClear.Size = new System.Drawing.Size(88, 22);
            this.allToolStripMenuItemClear.Text = "All";
            this.allToolStripMenuItemClear.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // mailConfigToolStripMenuItem
            // 
            this.mailConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.mailConfigToolStripMenuItem.Name = "mailConfigToolStripMenuItem";
            this.mailConfigToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.mailConfigToolStripMenuItem.Text = "Mail Config";
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSubject,
            this.toToolStripMenuItem,
            this.ToolStripMenuItemFrom,
            this.ToolStripMenuItemRoute});
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.downloadToolStripMenuItem.Text = "To Download";
            // 
            // ToolStripMenuItemSubject
            // 
            this.ToolStripMenuItemSubject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem2,
            this.customToolStripMenuItem});
            this.ToolStripMenuItemSubject.Name = "ToolStripMenuItemSubject";
            this.ToolStripMenuItemSubject.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemSubject.Text = "Subject";
            // 
            // configToolStripMenuItem2
            // 
            this.configToolStripMenuItem2.Name = "configToolStripMenuItem2";
            this.configToolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
            this.configToolStripMenuItem2.Text = "Config";
            this.configToolStripMenuItem2.Click += new System.EventHandler(this.configToolStripMenuItem2_Click_1);
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.customToolStripMenuItem.Text = "Custom";
            // 
            // toToolStripMenuItem
            // 
            this.toToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTO});
            this.toToolStripMenuItem.Name = "toToolStripMenuItem";
            this.toToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.toToolStripMenuItem.Text = "To";
            // 
            // toolStripMenuItemTO
            // 
            this.toolStripMenuItemTO.Name = "toolStripMenuItemTO";
            this.toolStripMenuItemTO.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItemTO.Text = "Config";
            this.toolStripMenuItemTO.Click += new System.EventHandler(this.toolStripMenuItemTO_Click);
            // 
            // ToolStripMenuItemFrom
            // 
            this.ToolStripMenuItemFrom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem1});
            this.ToolStripMenuItemFrom.Name = "ToolStripMenuItemFrom";
            this.ToolStripMenuItemFrom.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemFrom.Text = "From";
            // 
            // configToolStripMenuItem1
            // 
            this.configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            this.configToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
            this.configToolStripMenuItem1.Text = "Config";
            this.configToolStripMenuItem1.Click += new System.EventHandler(this.configToolStripMenuItem1_Click_1);
            // 
            // ToolStripMenuItemRoute
            // 
            this.ToolStripMenuItemRoute.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.ToolStripMenuItemRoute.Name = "ToolStripMenuItemRoute";
            this.ToolStripMenuItemRoute.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemRoute.Text = "Route";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.configToolStripMenuItem.Text = "Config";
            this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click_1);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem6,
            this.toolStripMenuItem8});
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "To Delete";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem1.Text = "Subject";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem2.Text = "Config";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(116, 22);
            this.toolStripMenuItem3.Text = "Custom";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemToDelete});
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem4.Text = "To";
            // 
            // toolStripMenuItemToDelete
            // 
            this.toolStripMenuItemToDelete.Name = "toolStripMenuItemToDelete";
            this.toolStripMenuItemToDelete.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItemToDelete.Text = "Config";
            this.toolStripMenuItemToDelete.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem6.Text = "From";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItem7.Text = "Config";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem9});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem8.Text = "Route";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItem9.Text = "Config";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // Mail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(1380, 657);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Mail";
            this.Text = "Mail";
            this.Load += new System.EventHandler(this.Mail_Load);
            this.Resize += new System.EventHandler(this.Mail_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button_Cancel;
        public DataGridView DataGridView1;
        protected Button button_OK;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem setupToolStripMenuItem;
        private ToolStripMenuItem keepTimeToolStripMenuItem;
        private ToolStripComboBox toolStripComboBoxTime;
        private ToolStripMenuItem keepQTYToolStripMenuItem;
        private ToolStripMenuItem cleanListToolStripMenuItem;
		private ToolStripMenuItem mailConfigToolStripMenuItem;
        private ToolStripMenuItem allToolStripMenuItemClear;
		private ToolStripMenuItem downloadToolStripMenuItem;
		private ToolStripMenuItem ToolStripMenuItemSubject;
		private ToolStripMenuItem configToolStripMenuItem2;
		private ToolStripMenuItem customToolStripMenuItem;
		private ToolStripMenuItem toToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItemTO;
		private ToolStripMenuItem ToolStripMenuItemFrom;
		private ToolStripMenuItem configToolStripMenuItem1;
		private ToolStripMenuItem ToolStripMenuItemRoute;
		private ToolStripMenuItem configToolStripMenuItem;
		private ToolStripMenuItem deleteToolStripMenuItem;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem toolStripMenuItem2;
		private ToolStripMenuItem toolStripMenuItem3;
		private ToolStripMenuItem toolStripMenuItem4;
		private ToolStripMenuItem toolStripMenuItemToDelete;
		private ToolStripMenuItem toolStripMenuItem6;
		private ToolStripMenuItem toolStripMenuItem7;
		private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripComboBox toolStripComboBoxQTY;
    }
}