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
            this.recordButton = new System.Windows.Forms.Button();
            this.saveDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.SaveDirectorylabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecordsButton = new System.Windows.Forms.Button();
            this.goToSaveFolderButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // recordButton
            // 
            this.recordButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.recordButton.Location = new System.Drawing.Point(299, 107);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(84, 71);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Stop recording";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // saveDirectoryTextBox
            // 
            this.saveDirectoryTextBox.Location = new System.Drawing.Point(26, 68);
            this.saveDirectoryTextBox.Name = "saveDirectoryTextBox";
            this.saveDirectoryTextBox.ReadOnly = true;
            this.saveDirectoryTextBox.Size = new System.Drawing.Size(254, 20);
            this.saveDirectoryTextBox.TabIndex = 1;
            // 
            // SaveDirectorylabel
            // 
            this.SaveDirectorylabel.AutoSize = true;
            this.SaveDirectorylabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveDirectorylabel.Location = new System.Drawing.Point(22, 33);
            this.SaveDirectorylabel.Name = "SaveDirectorylabel";
            this.SaveDirectorylabel.Size = new System.Drawing.Size(109, 20);
            this.SaveDirectorylabel.TabIndex = 2;
            this.SaveDirectorylabel.Text = "Save directory";
            // 
            // browseButton
            // 
            this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.browseButton.Location = new System.Drawing.Point(299, 68);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(84, 23);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.statisticsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(395, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
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
            this.openRecordsButton.Location = new System.Drawing.Point(26, 151);
            this.openRecordsButton.Name = "openRecordsButton";
            this.openRecordsButton.Size = new System.Drawing.Size(254, 27);
            this.openRecordsButton.TabIndex = 5;
            this.openRecordsButton.Text = "Show Records";
            this.openRecordsButton.UseVisualStyleBackColor = true;
            // 
            // goToSaveFolderButton
            // 
            this.goToSaveFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.goToSaveFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.goToSaveFolderButton.Location = new System.Drawing.Point(26, 107);
            this.goToSaveFolderButton.Name = "goToSaveFolderButton";
            this.goToSaveFolderButton.Size = new System.Drawing.Size(254, 28);
            this.goToSaveFolderButton.TabIndex = 6;
            this.goToSaveFolderButton.Text = "Go to save directory";
            this.goToSaveFolderButton.UseVisualStyleBackColor = true;
            this.goToSaveFolderButton.Click += new System.EventHandler(this.goToSaveFolderButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 199);
            this.Controls.Add(this.goToSaveFolderButton);
            this.Controls.Add(this.openRecordsButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.SaveDirectorylabel);
            this.Controls.Add(this.saveDirectoryTextBox);
            this.Controls.Add(this.recordButton);
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

        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.TextBox saveDirectoryTextBox;
        private System.Windows.Forms.Label SaveDirectorylabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button openRecordsButton;
        private System.Windows.Forms.Button goToSaveFolderButton;
    }
}

