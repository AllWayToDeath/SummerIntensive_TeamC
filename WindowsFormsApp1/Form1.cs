using System;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using NAudio;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Toolkit.Uwp.Notifications;
using Tulpep.NotificationWindow;
using System.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;

        NAudio.Wave.WaveIn sourceStream = null;
        NAudio.Wave.DirectSoundOut waveOut = null;
        NAudio.Wave.WaveFileWriter waveWriter = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            /*startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"Slack.exe\""));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"Slack.exe\""));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();*/
        }

        
        

        public static int counter = 0;


        void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Slack");
            if (proc.Length < 8)
            {
                if (counter == 3)
                {

                    //Stop record

                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, "Slack Recorder", "Call recorded", ToolTipIcon.Info);
                    notifyIcon.Visible = false;

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

                    //Start record

                   /* int deviceNumber = sourceList.SelectedItems[0].Index;

                    sourceStream = new NAudio.Wave.WaveIn();
                    sourceStream.DeviceNumber = deviceNumber;
                    sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

                    NAudio.Wave.WaveInProvider waveIn = new NAudio.Wave.WaveInProvider(sourceStream);

                    waveOut = new NAudio.Wave.DirectSoundOut();
                    waveOut.Init(waveIn);

                    sourceStream.StartRecording();
                    waveOut.Play();

                    recordButton.Visible = false;
                    stopRecord.Visible = true;

                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, "Slack Recorder", "Record started", ToolTipIcon.Info);
                    notifyIcon.Visible = false;

                    counter = 0;*/
                }
                counter++;
            }
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
           if (recordButton.Text == "Start recording")
           {
                /*startWatch.Start();
                stopWatch.Start();*/
                recordButton.Text = "Stop recording";
           }

           else
           {
               /* startWatch.Stop();
                stopWatch.Stop();*/
                recordButton.Text = "Start recording";
           }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveDirectory = new FolderBrowserDialog();

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
            if (saveDirectoryTextBox.Text == "")
            {
                return;
            }
            else
            {
                string Directory = saveDirectoryTextBox.Text;
                Process.Start("explorer", Directory);

            }
        }
    }
}
