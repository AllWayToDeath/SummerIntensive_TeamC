using System;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using NAudio.Wave;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        WaveInEvent waveIn;
        WaveFileWriter writer;

        FolderBrowserDialog saveDirectory = new FolderBrowserDialog();

        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;

        string outputFilename = "test.wav";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"Slack.exe\""));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"Slack.exe\""));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();

            saveDirectory.SelectedPath = saveDirectoryTextBox.Text;
        }

        public static int counter = 0;

        void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Slack");
            if (proc.Length < 8)
            {
                if (counter == 3)
                {
                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, "Slack Recorder", "Call recorded", ToolTipIcon.Info);
                    notifyIcon.Visible = false;

                    if (waveIn != null)
                    {
                        waveIn.StopRecording();

                        ProcessStartInfo psi = new ProcessStartInfo();

                        psi.FileName = "lame.exe";
                        psi.Arguments = "-V2 " + "test.wav" + " " + "test.mp3";

                        psi.WindowStyle = ProcessWindowStyle.Hidden;

                        Process p = Process.Start(psi);
                        p.WaitForExit();
                    }

                    counter = 0;
                }
                counter++;
            }
        }

        void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Slack");
            if (proc.Length >= 8)
            {

                if (counter == 3)
                {
                    try
                    {
                        waveIn = new WaveInEvent();

                        waveIn.DeviceNumber = 0;
                        waveIn.WaveFormat = new NAudio.Wave.WaveFormat(41000, 1);

                        waveIn.DataAvailable += waveIn_DataAvailable;
                        waveIn.RecordingStopped += waveIn_RecordingStopped;

                        writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);

                        waveIn.StartRecording();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, "Slack Recorder", "Record started", ToolTipIcon.Info);
                    notifyIcon.Visible = false;

                    counter = 0;
                }
                counter++;
            }
        }

        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                writer.Write(e.Buffer, 0, e.BytesRecorded);
            }
        }

        private void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(waveIn_RecordingStopped), sender, e);
            }
            else
            {
                waveIn.Dispose();
                waveIn = null;
                writer.Close();
                writer = null;

            }
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
           if (recordButton.Text == "Start recording")
           {
                startWatch.Start();
                stopWatch.Start();
                recordButton.Text = "Stop recording";
            }

           else
           {
                startWatch.Stop();
                stopWatch.Stop();
                
                recordButton.Text = "Start recording";
           }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            

            if (saveDirectory.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveDirectory.SelectedPath))
            {
                saveDirectoryTextBox.Text = saveDirectory.SelectedPath;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bool cursorNotInBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (this.WindowState == FormWindowState.Minimized && cursorNotInBar)
            {
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
                this.Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void goToSaveFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", saveDirectory.SelectedPath);
        }
    }
}
