namespace WindowsFormsApp1
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
            this.browseButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecordsButton = new System.Windows.Forms.Button();
            this.goToSaveFolderButton = new System.Windows.Forms.Button();
            this.runAtStartUpCheckBox = new System.Windows.Forms.CheckBox();
            this.recordActivatedCheckBox = new System.Windows.Forms.CheckBox();
            this.SaveDirectorylabel = new System.Windows.Forms.Label();
            this.saveDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.menuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.browseButton.Location = new System.Drawing.Point(245, 42);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(77, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIcon.BalloonTipText = "text";
            this.notifyIcon.BalloonTipTitle = "title";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Slack Recorder";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            this.statisticsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.statisticsToolStripMenuItem.Text = "Statistics";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // openRecordsButton
            // 
            this.openRecordsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openRecordsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openRecordsButton.Image = ((System.Drawing.Image)(resources.GetObject("openRecordsButton.Image")));
            this.openRecordsButton.Location = new System.Drawing.Point(245, 79);
            this.openRecordsButton.Name = "openRecordsButton";
            this.openRecordsButton.Size = new System.Drawing.Size(77, 74);
            this.openRecordsButton.TabIndex = 5;
            this.openRecordsButton.UseVisualStyleBackColor = true;
            this.openRecordsButton.Click += new System.EventHandler(this.openRecordsButton_Click);
            // 
            // goToSaveFolderButton
            // 
            this.goToSaveFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goToSaveFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.goToSaveFolderButton.Location = new System.Drawing.Point(16, 79);
            this.goToSaveFolderButton.Name = "goToSaveFolderButton";
            this.goToSaveFolderButton.Size = new System.Drawing.Size(209, 34);
            this.goToSaveFolderButton.TabIndex = 6;
            this.goToSaveFolderButton.Text = "Go to save directory";
            this.goToSaveFolderButton.UseVisualStyleBackColor = true;
            this.goToSaveFolderButton.Click += new System.EventHandler(this.goToSaveFolderButton_Click);
            // 
            // runAtStartUpCheckBox
            // 
            this.runAtStartUpCheckBox.AutoSize = true;
            this.runAtStartUpCheckBox.Location = new System.Drawing.Point(132, 136);
            this.runAtStartUpCheckBox.Name = "runAtStartUpCheckBox";
            this.runAtStartUpCheckBox.Size = new System.Drawing.Size(93, 17);
            this.runAtStartUpCheckBox.TabIndex = 7;
            this.runAtStartUpCheckBox.Text = "Run at startup";
            this.runAtStartUpCheckBox.UseVisualStyleBackColor = true;
            this.runAtStartUpCheckBox.CheckedChanged += new System.EventHandler(this.runAtStartUpCheckBox_CheckedChanged);
            // 
            // recordActivatedCheckBox
            // 
            this.recordActivatedCheckBox.AutoSize = true;
            this.recordActivatedCheckBox.Checked = true;
            this.recordActivatedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recordActivatedCheckBox.Location = new System.Drawing.Point(16, 136);
            this.recordActivatedCheckBox.Name = "recordActivatedCheckBox";
            this.recordActivatedCheckBox.Size = new System.Drawing.Size(75, 17);
            this.recordActivatedCheckBox.TabIndex = 8;
            this.recordActivatedCheckBox.Text = "Recording";
            this.recordActivatedCheckBox.UseVisualStyleBackColor = true;
            this.recordActivatedCheckBox.CheckedChanged += new System.EventHandler(this.recordActivatedCheckBox_CheckedChanged);
            // 
            // SaveDirectorylabel
            // 
            this.SaveDirectorylabel.AutoSize = true;
            this.SaveDirectorylabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveDirectorylabel.Location = new System.Drawing.Point(12, 9);
            this.SaveDirectorylabel.Name = "SaveDirectorylabel";
            this.SaveDirectorylabel.Size = new System.Drawing.Size(109, 20);
            this.SaveDirectorylabel.TabIndex = 2;
            this.SaveDirectorylabel.Text = "Save directory";
            // 
            // saveDirectoryTextBox
            // 
            this.saveDirectoryTextBox.Location = new System.Drawing.Point(16, 42);
            this.saveDirectoryTextBox.Name = "saveDirectoryTextBox";
            this.saveDirectoryTextBox.ReadOnly = true;
            this.saveDirectoryTextBox.Size = new System.Drawing.Size(209, 20);
            this.saveDirectoryTextBox.TabIndex = 9;
            this.saveDirectoryTextBox.Text = "C:\\Users\\Default\\Music";
            // 
            // menuToolStripMenuItem1
            // 
            this.menuToolStripMenuItem1.Name = "menuToolStripMenuItem1";
            this.menuToolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem1.Text = "Menu";
            // 
            // statisticsToolStripMenuItem1
            // 
            this.statisticsToolStripMenuItem1.Name = "statisticsToolStripMenuItem1";
            this.statisticsToolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.statisticsToolStripMenuItem1.Text = "Statistics";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem1.Text = "About";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(347, 130);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(150, 23);
            this.openButton.TabIndex = 12;
            this.openButton.Text = "Open selected record";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(503, 130);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(147, 23);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "Delete selected record";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(347, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(303, 112);
            this.dataGridView1.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 163);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.saveDirectoryTextBox);
            this.Controls.Add(this.recordActivatedCheckBox);
            this.Controls.Add(this.runAtStartUpCheckBox);
            this.Controls.Add(this.goToSaveFolderButton);
            this.Controls.Add(this.openRecordsButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.SaveDirectorylabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Slack recorder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button openRecordsButton;
        private System.Windows.Forms.Button goToSaveFolderButton;
        private System.Windows.Forms.CheckBox runAtStartUpCheckBox;
        private System.Windows.Forms.CheckBox recordActivatedCheckBox;
        private System.Windows.Forms.Label SaveDirectorylabel;
        private System.Windows.Forms.TextBox saveDirectoryTextBox;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

