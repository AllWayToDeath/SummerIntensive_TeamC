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
            this.saveDirectoryTextBox = new System.Windows.Forms.TextBox();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveDirectoryTextBox
            // 
            this.saveDirectoryTextBox.Location = new System.Drawing.Point(16, 68);
            this.saveDirectoryTextBox.Name = "saveDirectoryTextBox";
            this.saveDirectoryTextBox.ReadOnly = true;
            this.saveDirectoryTextBox.Size = new System.Drawing.Size(209, 20);
            this.saveDirectoryTextBox.TabIndex = 1;
            this.saveDirectoryTextBox.Text = "C:\\Users\\Default\\Music";
            // 
            // browseButton
            // 
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.browseButton.Location = new System.Drawing.Point(245, 68);
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
            this.notifyIcon.Text = "notifyIcon";
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
            this.openRecordsButton.Location = new System.Drawing.Point(245, 107);
            this.openRecordsButton.Name = "openRecordsButton";
            this.openRecordsButton.Size = new System.Drawing.Size(77, 74);
            this.openRecordsButton.TabIndex = 5;
            this.openRecordsButton.UseVisualStyleBackColor = true;
            // 
            // goToSaveFolderButton
            // 
            this.goToSaveFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goToSaveFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.goToSaveFolderButton.Location = new System.Drawing.Point(16, 107);
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
            this.runAtStartUpCheckBox.Location = new System.Drawing.Point(132, 164);
            this.runAtStartUpCheckBox.Name = "runAtStartUpCheckBox";
            this.runAtStartUpCheckBox.Size = new System.Drawing.Size(93, 17);
            this.runAtStartUpCheckBox.TabIndex = 7;
            this.runAtStartUpCheckBox.Text = "Run at startup";
            this.runAtStartUpCheckBox.UseVisualStyleBackColor = true;
            // 
            // recordActivatedCheckBox
            // 
            this.recordActivatedCheckBox.AutoSize = true;
            this.recordActivatedCheckBox.Checked = true;
            this.recordActivatedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recordActivatedCheckBox.Location = new System.Drawing.Point(16, 164);
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
            this.SaveDirectorylabel.Location = new System.Drawing.Point(12, 33);
            this.SaveDirectorylabel.Name = "SaveDirectorylabel";
            this.SaveDirectorylabel.Size = new System.Drawing.Size(109, 20);
            this.SaveDirectorylabel.TabIndex = 2;
            this.SaveDirectorylabel.Text = "Save directory";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem1,
            this.statisticsToolStripMenuItem1,
            this.aboutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(343, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem1
            // 
            this.menuToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.menuToolStripMenuItem1.Name = "menuToolStripMenuItem1";
            this.menuToolStripMenuItem1.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem1.Text = "Menu";
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 201);
            this.Controls.Add(this.recordActivatedCheckBox);
            this.Controls.Add(this.runAtStartUpCheckBox);
            this.Controls.Add(this.goToSaveFolderButton);
            this.Controls.Add(this.openRecordsButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.SaveDirectorylabel);
            this.Controls.Add(this.saveDirectoryTextBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Slack recorder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox saveDirectoryTextBox;
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    }
}

