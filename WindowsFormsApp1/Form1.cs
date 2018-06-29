using System;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using NAudio.Wave;
using Microsoft.Win32;
using CSCore;
//using CSCore.SoundIn;
using CSCore.Codecs.WAV;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        WaveInEvent waveIn;
        WaveFileWriter writer;

        WasapiLoopbackCapture CaptureInstance;
        WaveFileWriter RecordedAudioWriter;

        

        FolderBrowserDialog saveDirectory = new FolderBrowserDialog();
        
        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;

        string playbackRecordFileName = "Record_from_speakers.wav";
        string micRecordFIleName = "Record_from_mic.wav";

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

            this.Visible = false;

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
                        this.CaptureInstance.StopRecording();

                        NAudio.Wave.SampleProviders.MixingSampleProvider mixer = new NAudio.Wave.SampleProviders.MixingSampleProvider(NAudio.Wave.WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));

                        MessageBox.Show("YAY2");
                        AudioFileReader audioFileReader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + micRecordFIleName);
                        AudioFileReader _audioFileReader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + playbackRecordFileName);

                        MessageBox.Show("YAY3");
                        mixer.AddMixerInput((ISampleProvider)audioFileReader);
                        mixer.AddMixerInput((ISampleProvider)_audioFileReader);

                        MessageBox.Show("YAY4");
                        var waveProvider = mixer.ToWaveProvider();

                        MessageBox.Show("YAY5");
                        WaveFileWriter.CreateWaveFile(saveDirectory.SelectedPath + "\\" + "result.wav", waveProvider);

                        ProcessStartInfo psi = new ProcessStartInfo();

                        psi.FileName = "lame.exe";
                        psi.Arguments = "-V2 " + saveDirectory.SelectedPath + "\\" + " result.wav" + " " + saveDirectory.SelectedPath + "\\" + " result.mp3";

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
                        waveIn.WaveFormat = new NAudio.Wave.WaveFormat(44100, 32, 2);

                        waveIn.DataAvailable += waveIn_DataAvailable;
                        waveIn.RecordingStopped += waveIn_RecordingStopped;

                        writer = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + micRecordFIleName, waveIn.WaveFormat);

                        waveIn.StartRecording();

                        this.CaptureInstance = new WasapiLoopbackCapture();
                        this.RecordedAudioWriter = new WaveFileWriter(saveDirectory.SelectedPath + "\\"+ playbackRecordFileName, CaptureInstance.WaveFormat);

                        this.CaptureInstance.DataAvailable += (s, a) =>
                        {
                            this.RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
                        };

                        this.CaptureInstance.RecordingStopped += (s, a) =>
                        {
                            this.RecordedAudioWriter.Dispose();
                            this.RecordedAudioWriter = null;
                            CaptureInstance.Dispose();
                        };

                        this.CaptureInstance.StartRecording();

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

        private void chkStartUp_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
            ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkStartUp.Checked)
                rk.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, Application.ExecutablePath);
            else
                rk.DeleteValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, false);
        }
    }
}
