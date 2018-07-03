namespace Slack_Recorder
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.saveDirectoryLabel = new System.Windows.Forms.Label();
            this.saveDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.openSaveDirectoryButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.openGridButton = new System.Windows.Forms.Button();
            this.recordActivatedCheckBox = new System.Windows.Forms.CheckBox();
            this.runAtSturtUpCheckBox = new System.Windows.Forms.CheckBox();
            this.openRecordButton = new System.Windows.Forms.Button();
            this.deleteSelectedButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.helpButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // saveDirectoryLabel
            // 
            this.saveDirectoryLabel.AutoSize = true;
            this.saveDirectoryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveDirectoryLabel.Location = new System.Drawing.Point(12, 12);
            this.saveDirectoryLabel.Name = "saveDirectoryLabel";
            this.saveDirectoryLabel.Size = new System.Drawing.Size(80, 17);
            this.saveDirectoryLabel.TabIndex = 4;
            this.saveDirectoryLabel.Text = "Save folder";
            // 
            // saveDirectoryTextBox
            // 
            this.saveDirectoryTextBox.Location = new System.Drawing.Point(15, 43);
            this.saveDirectoryTextBox.Name = "saveDirectoryTextBox";
            this.saveDirectoryTextBox.Size = new System.Drawing.Size(227, 20);
            this.saveDirectoryTextBox.TabIndex = 5;
            this.saveDirectoryTextBox.Text = "C:\\Users\\Default\\Music";
            // 
            // openSaveDirectoryButton
            // 
            this.openSaveDirectoryButton.Location = new System.Drawing.Point(15, 78);
            this.openSaveDirectoryButton.Name = "openSaveDirectoryButton";
            this.openSaveDirectoryButton.Size = new System.Drawing.Size(227, 30);
            this.openSaveDirectoryButton.TabIndex = 6;
            this.openSaveDirectoryButton.Text = "Open save folder";
            this.openSaveDirectoryButton.UseVisualStyleBackColor = true;
            this.openSaveDirectoryButton.Click += new System.EventHandler(this.openSaveDirectoryButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(248, 43);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // openGridButton
            // 
            this.openGridButton.Image = ((System.Drawing.Image)(resources.GetObject("openGridButton.Image")));
            this.openGridButton.Location = new System.Drawing.Point(248, 78);
            this.openGridButton.Name = "openGridButton";
            this.openGridButton.Size = new System.Drawing.Size(75, 73);
            this.openGridButton.TabIndex = 8;
            this.openGridButton.UseVisualStyleBackColor = true;
            this.openGridButton.Click += new System.EventHandler(this.openGridButton_Click);
            // 
            // recordActivatedCheckBox
            // 
            this.recordActivatedCheckBox.AutoSize = true;
            this.recordActivatedCheckBox.Checked = true;
            this.recordActivatedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recordActivatedCheckBox.Location = new System.Drawing.Point(15, 133);
            this.recordActivatedCheckBox.Name = "recordActivatedCheckBox";
            this.recordActivatedCheckBox.Size = new System.Drawing.Size(75, 17);
            this.recordActivatedCheckBox.TabIndex = 9;
            this.recordActivatedCheckBox.Text = "Recording";
            this.recordActivatedCheckBox.UseVisualStyleBackColor = true;
            this.recordActivatedCheckBox.CheckedChanged += new System.EventHandler(this.recordActivatedCheckBox_CheckedChanged);
            // 
            // runAtSturtUpCheckBox
            // 
            this.runAtSturtUpCheckBox.AutoSize = true;
            this.runAtSturtUpCheckBox.Location = new System.Drawing.Point(146, 133);
            this.runAtSturtUpCheckBox.Name = "runAtSturtUpCheckBox";
            this.runAtSturtUpCheckBox.Size = new System.Drawing.Size(96, 17);
            this.runAtSturtUpCheckBox.TabIndex = 10;
            this.runAtSturtUpCheckBox.Text = "Run at start up";
            this.runAtSturtUpCheckBox.UseVisualStyleBackColor = true;
            this.runAtSturtUpCheckBox.CheckedChanged += new System.EventHandler(this.runAtSturtUpCheckBox_CheckedChanged);
            // 
            // openRecordButton
            // 
            this.openRecordButton.Location = new System.Drawing.Point(351, 133);
            this.openRecordButton.Name = "openRecordButton";
            this.openRecordButton.Size = new System.Drawing.Size(129, 23);
            this.openRecordButton.TabIndex = 11;
            this.openRecordButton.Text = "Open selected record";
            this.openRecordButton.UseVisualStyleBackColor = true;
            this.openRecordButton.Click += new System.EventHandler(this.openRecordButton_Click);
            // 
            // deleteSelectedButton
            // 
            this.deleteSelectedButton.Location = new System.Drawing.Point(506, 133);
            this.deleteSelectedButton.Name = "deleteSelectedButton";
            this.deleteSelectedButton.Size = new System.Drawing.Size(142, 23);
            this.deleteSelectedButton.TabIndex = 14;
            this.deleteSelectedButton.Text = "Delete selected record";
            this.deleteSelectedButton.UseVisualStyleBackColor = true;
            this.deleteSelectedButton.Click += new System.EventHandler(this.deleteSelectedButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Slack Recorder";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notification";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Date,
            this.Time});
            this.dataGridView.Location = new System.Drawing.Point(351, 12);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(297, 105);
            this.dataGridView.TabIndex = 0;
            // 
            // helpButton
            // 
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.Image = ((System.Drawing.Image)(resources.GetObject("helpButton.Image")));
            this.helpButton.Location = new System.Drawing.Point(302, 12);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(21, 23);
            this.helpButton.TabIndex = 15;
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(654, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 161);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.deleteSelectedButton);
            this.Controls.Add(this.openRecordButton);
            this.Controls.Add(this.runAtSturtUpCheckBox);
            this.Controls.Add(this.recordActivatedCheckBox);
            this.Controls.Add(this.openGridButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.openSaveDirectoryButton);
            this.Controls.Add(this.saveDirectoryTextBox);
            this.Controls.Add(this.saveDirectoryLabel);
            this.Controls.Add(this.dataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Slack Recorder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label saveDirectoryLabel;
        private System.Windows.Forms.TextBox saveDirectoryTextBox;
        private System.Windows.Forms.Button openSaveDirectoryButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button openGridButton;
        private System.Windows.Forms.CheckBox recordActivatedCheckBox;
        private System.Windows.Forms.CheckBox runAtSturtUpCheckBox;
        private System.Windows.Forms.Button openRecordButton;
        private System.Windows.Forms.Button deleteSelectedButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
    }
}

