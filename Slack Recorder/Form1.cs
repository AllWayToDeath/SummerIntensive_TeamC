using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slack_Recorder.Models;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Management;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.IO;

namespace Slack_Recorder
{
    public partial class Form1 : Form
    {
        bool isExpanded = false;
        public static int counter = 0;

        Repository _rep;
        FolderBrowserDialog saveDirectory = new FolderBrowserDialog();

        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;

        //recorder and writer for mic
        WaveInEvent waveIn;
        WaveFileWriter writer;
        string micRecordFIleName = "Record_from_mic.wav";

        //recorder and writer for speaker
        WasapiLoopbackCapture CaptureInstance;
        WaveFileWriter RecordedAudioWriter;
        string playbackRecordFileName = "Record_from_speakers.wav";

        public Form1()
        {
            _rep = new Repository();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadRecords();
            saveDirectory.SelectedPath = saveDirectoryTextBox.Text;

            startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"Slack.exe\""));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"Slack.exe\""));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();
        }

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

                        MixingSampleProvider mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));

                        MessageBox.Show("DELETE ME"); //ПАНИКА -> АПАТИЯ -> СМИРЕНИЕ
                        AudioFileReader MicFileReader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + micRecordFIleName);
                        AudioFileReader SpeakerFilereader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + playbackRecordFileName);

                        mixer.AddMixerInput((ISampleProvider)MicFileReader);
                        mixer.AddMixerInput((ISampleProvider)SpeakerFilereader);

                        var waveProvider = mixer.ToWaveProvider();

                        WaveFileWriter.CreateWaveFile(saveDirectory.SelectedPath + "\\" + "result.wav", waveProvider);

                        ProcessStartInfo psi = new ProcessStartInfo();

                        psi.FileName = "lame.exe";
                        psi.Arguments = "-V2 " + saveDirectory.SelectedPath + @"\" + "result.wav " + saveDirectory.SelectedPath + @"\" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm") + ".mp3";
                        psi.WindowStyle = ProcessWindowStyle.Hidden;

                        Process p = Process.Start(psi);

                        p.WaitForExit();

                        MicFileReader.Dispose();
                        SpeakerFilereader.Dispose();


                        Record record = new Record();
                        record.Date = DateTime.Now.ToString("dd.MM.yyyy");
                        record.Time = DateTime.Now.ToString("HH.mm");

                        _rep.Insert(record);
                        ReadRecords();
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
                        this.RecordedAudioWriter = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + playbackRecordFileName, CaptureInstance.WaveFormat);

                        this.CaptureInstance.DataAvailable += (s, a) =>
                        {
                            this.RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
                        };

                        this.CaptureInstance.RecordingStopped += (s, a) =>
                        {
                            this.RecordedAudioWriter.Dispose();  //здесь может быть баг
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

        void waveIn_RecordingStopped(object sender, EventArgs e)
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

        private void ReadRecords()
        {
            List<Record> clients = _rep.GetRecords();
           
            BindingList<Record> bindingList = new BindingList<Record>(clients);
            this.dataGridView.DataSource = new BindingSource(bindingList, null);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (saveDirectory.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(saveDirectory.SelectedPath))
            {
                saveDirectoryTextBox.Text = saveDirectory.SelectedPath;
            }
        }

        private void openGridButton_Click(object sender, EventArgs e)
        {
            if (!isExpanded)
            {
                for (int i = 0; i < 65; i++)
                {
                    this.Width += 5;
                    Thread.Sleep(5);
                }
                isExpanded = true;
            }

            else
            {
                for (int i = 0; i < 65; i++)
                {
                    this.Width -= 5;
                    Thread.Sleep(5);
                }
                isExpanded = false;
            }
        }

        private void openSaveDirectoryButton_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", saveDirectory.SelectedPath);
        }

        private void recordActivatedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (recordActivatedCheckBox.Checked)
            {
                startWatch.Start();
                stopWatch.Start();
            }
            else
            {
                startWatch.Stop();
                stopWatch.Stop();
            }
        }

        private void runAtSturtUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (runAtSturtUpCheckBox.Checked)
            {
                rk.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, Application.ExecutablePath);
            }

            else
            {
                rk.DeleteValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, false);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
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


        private void button2_Click(object sender, EventArgs e)
        {
            Record record = new Record();
            record.Date = DateTime.Now.ToString("dd.MM.yyyy");
            record.Time = DateTime.Now.ToString("HH.mm");

            _rep.Insert(record);

            ReadRecords();
        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            Record record = (Record)dataGridView.CurrentRow.DataBoundItem;
            if (!record.Id.HasValue)
            {
                MessageBox.Show("Select a row first");
            }
            else
            {
                _rep.Delete(record);
                ReadRecords();
                File.Delete(saveDirectory.SelectedPath + "\\" + record.Date + "_" + record.Time + ".mp3");
            }
        }

        private void openRecordButton_Click(object sender, EventArgs e)
        {
            string selectedFile = saveDirectory.SelectedPath + "\\" + dataGridView.CurrentRow.Cells[1].Value.ToString() + "_" + dataGridView.CurrentRow.Cells[2].Value.ToString() + ".mp3";

            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", selectedFile));
        }

        private void helpButton_Click(object sender, EventArgs e)
        {

        }
    }
}
