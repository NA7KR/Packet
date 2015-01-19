using System.ComponentModel;
using System.Windows.Forms;

namespace Packet
{
    partial class Custom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Custom));
            this.Cancel_button = new System.Windows.Forms.Button();
            this.OK_button = new System.Windows.Forms.Button();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.name_textBox = new System.Windows.Forms.TextBox();
            this.Query_richTextBox = new System.Windows.Forms.RichTextBox();
            this.name_label = new System.Windows.Forms.Label();
            this.table_label = new System.Windows.Forms.Label();
            this.Query_label = new System.Windows.Forms.Label();
            this.Active_label = new System.Windows.Forms.Label();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.MSGFrom_radioButton = new System.Windows.Forms.RadioButton();
            this.MSGRoute_radioButton = new System.Windows.Forms.RadioButton();
            this.MSGTSLD_radioButton = new System.Windows.Forms.RadioButton();
            this.MSGSubject_radioButton = new System.Windows.Forms.RadioButton();
            this.panel_radioButton = new System.Windows.Forms.Panel();
            this.edit_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.panel_radioButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(187, 451);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 0;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(47, 451);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 1;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // DataGridView1
            // 
            this.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DataGridView1.Location = new System.Drawing.Point(434, 12);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            this.DataGridView1.Size = new System.Drawing.Size(439, 462);
            this.DataGridView1.TabIndex = 2;
            this.DataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // name_textBox
            // 
            this.name_textBox.Location = new System.Drawing.Point(117, 64);
            this.name_textBox.Name = "name_textBox";
            this.name_textBox.Size = new System.Drawing.Size(301, 20);
            this.name_textBox.TabIndex = 3;
            // 
            // Query_richTextBox
            // 
            this.Query_richTextBox.Location = new System.Drawing.Point(117, 112);
            this.Query_richTextBox.Name = "Query_richTextBox";
            this.Query_richTextBox.Size = new System.Drawing.Size(301, 129);
            this.Query_richTextBox.TabIndex = 6;
            this.Query_richTextBox.Text = "";
            
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Location = new System.Drawing.Point(32, 71);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(35, 13);
            this.name_label.TabIndex = 8;
            this.name_label.Text = "Name";
            // 
            // table_label
            // 
            this.table_label.AutoSize = true;
            this.table_label.Location = new System.Drawing.Point(32, 298);
            this.table_label.Name = "table_label";
            this.table_label.Size = new System.Drawing.Size(34, 13);
            this.table_label.TabIndex = 9;
            this.table_label.Text = "Table";
            // 
            // Query_label
            // 
            this.Query_label.AutoSize = true;
            this.Query_label.Location = new System.Drawing.Point(33, 164);
            this.Query_label.Name = "Query_label";
            this.Query_label.Size = new System.Drawing.Size(35, 13);
            this.Query_label.TabIndex = 10;
            this.Query_label.Text = "Query";
            // 
            // Active_label
            // 
            this.Active_label.AutoSize = true;
            this.Active_label.Location = new System.Drawing.Point(31, 390);
            this.Active_label.Name = "Active_label";
            this.Active_label.Size = new System.Drawing.Size(37, 13);
            this.Active_label.TabIndex = 12;
            this.Active_label.Text = "Active";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(117, 390);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(59, 17);
            this.checkBox5.TabIndex = 18;
            this.checkBox5.Text = "Enable";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // MSGFrom_radioButton
            // 
            this.MSGFrom_radioButton.AutoSize = true;
            this.MSGFrom_radioButton.Location = new System.Drawing.Point(11, 31);
            this.MSGFrom_radioButton.Name = "MSGFrom_radioButton";
            this.MSGFrom_radioButton.Size = new System.Drawing.Size(72, 17);
            this.MSGFrom_radioButton.TabIndex = 19;
            this.MSGFrom_radioButton.TabStop = true;
            this.MSGFrom_radioButton.Text = "MSGFrom";
            this.MSGFrom_radioButton.UseVisualStyleBackColor = true;
            // 
            // MSGRoute_radioButton
            // 
            this.MSGRoute_radioButton.AutoSize = true;
            this.MSGRoute_radioButton.Location = new System.Drawing.Point(10, 54);
            this.MSGRoute_radioButton.Name = "MSGRoute_radioButton";
            this.MSGRoute_radioButton.Size = new System.Drawing.Size(78, 17);
            this.MSGRoute_radioButton.TabIndex = 20;
            this.MSGRoute_radioButton.TabStop = true;
            this.MSGRoute_radioButton.Text = "MSGRoute";
            this.MSGRoute_radioButton.UseVisualStyleBackColor = true;
            // 
            // MSGTSLD_radioButton
            // 
            this.MSGTSLD_radioButton.AutoSize = true;
            this.MSGTSLD_radioButton.Location = new System.Drawing.Point(11, 8);
            this.MSGTSLD_radioButton.Name = "MSGTSLD_radioButton";
            this.MSGTSLD_radioButton.Size = new System.Drawing.Size(77, 17);
            this.MSGTSLD_radioButton.TabIndex = 21;
            this.MSGTSLD_radioButton.TabStop = true;
            this.MSGTSLD_radioButton.Text = "MSGTSLD";
            this.MSGTSLD_radioButton.UseVisualStyleBackColor = true;
            // 
            // MSGSubject_radioButton
            // 
            this.MSGSubject_radioButton.AutoSize = true;
            this.MSGSubject_radioButton.Location = new System.Drawing.Point(11, 77);
            this.MSGSubject_radioButton.Name = "MSGSubject_radioButton";
            this.MSGSubject_radioButton.Size = new System.Drawing.Size(85, 17);
            this.MSGSubject_radioButton.TabIndex = 22;
            this.MSGSubject_radioButton.TabStop = true;
            this.MSGSubject_radioButton.Text = "MSGSubject";
            this.MSGSubject_radioButton.UseVisualStyleBackColor = true;
            // 
            // panel_radioButton
            // 
            this.panel_radioButton.Controls.Add(this.MSGTSLD_radioButton);
            this.panel_radioButton.Controls.Add(this.MSGSubject_radioButton);
            this.panel_radioButton.Controls.Add(this.MSGFrom_radioButton);
            this.panel_radioButton.Controls.Add(this.MSGRoute_radioButton);
            this.panel_radioButton.Location = new System.Drawing.Point(100, 263);
            this.panel_radioButton.Name = "panel_radioButton";
            this.panel_radioButton.Size = new System.Drawing.Size(170, 109);
            this.panel_radioButton.TabIndex = 24;
            // 
            // edit_button
            // 
            this.edit_button.Location = new System.Drawing.Point(319, 451);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(75, 23);
            this.edit_button.TabIndex = 25;
            this.edit_button.Text = "Edit";
            this.edit_button.UseVisualStyleBackColor = true;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // Custom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_button;
            this.ClientSize = new System.Drawing.Size(1228, 509);
            this.Controls.Add(this.edit_button);
            this.Controls.Add(this.panel_radioButton);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.Active_label);
            this.Controls.Add(this.Query_richTextBox);
            this.Controls.Add(this.Query_label);
            this.Controls.Add(this.table_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.name_textBox);
            this.Controls.Add(this.DataGridView1);
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.Cancel_button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Custom";
            this.Text = "Custom";
            this.Load += new System.EventHandler(this.Custom_Load);
            this.Resize += new System.EventHandler(this.Custom_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.panel_radioButton.ResumeLayout(false);
            this.panel_radioButton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Cancel_button;
        private Button OK_button;
        private DataGridView DataGridView1;
        private TextBox name_textBox;
        private RichTextBox Query_richTextBox;
        private Label name_label;
        private Label table_label;
        private Label Query_label;
        private Label Active_label;
        private CheckBox checkBox5;
        private RadioButton MSGFrom_radioButton;
        private RadioButton MSGRoute_radioButton;
        private RadioButton MSGTSLD_radioButton;
        private RadioButton MSGSubject_radioButton;
        private Panel panel_radioButton;
        private Button edit_button;
    }
}