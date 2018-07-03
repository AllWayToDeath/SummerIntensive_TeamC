using System;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using NAudio.Wave;
using Microsoft.Win32;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Configuration;
using SlackRecorder.Model;
using System.Data.SqlClient;
using System.ComponentModel;

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

        Repository _rep;
        BindingList<Record> bindingList;

        string playbackRecordFileName = "Record_from_speakers.wav";
        string micRecordFIleName = "Record_from_mic.wav";

        public Form1()
        {
            _rep = new Repository();

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

            //dataGridView1.Dispose();

            FeelGrid();
        }

        private void ReadRecords()
        {           
            List<Record> clients = _rep.GetRecords();

            bindingList = new BindingList<Record>(clients);
            this.dataGridView1.DataSource = new BindingSource(bindingList, null);
        }

        void FeelGrid()
        {
            ReadRecords();
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

                        MixTwoSamples();

                        ConvertToMP3();

                        AddToDatabase();                       
                    }

                    counter = 0;
                }
                counter++;
            }
        }

        void AddToDatabase()
        {
            Record record = new Record();

            record.Date = DateTime.Now.ToString("dd.MM.yyyy");
            record.Time = DateTime.Now.ToString("HH.mm");

            _rep.Insert(record);

            //FeelGrid(); вызывает ошибку при повторном использовании
        }

        void MixTwoSamples()
        {
            NAudio.Wave.SampleProviders.MixingSampleProvider mixer = new NAudio.Wave.SampleProviders.MixingSampleProvider(NAudio.Wave.WaveFormat.CreateIeeeFloatWaveFormat(44100, 2));

            //Найти способ избавиться от этого мсгбокса, без него AudioFileReader не инициализируется
            MessageBox.Show("Click me to get a record");
            
            AudioFileReader MicFileReader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + micRecordFIleName);
            AudioFileReader SpeakerFilereader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + playbackRecordFileName);

            AddSampleInMixer(mixer, MicFileReader);
            AddSampleInMixer(mixer, SpeakerFilereader);

            var waveProvider = mixer.ToWaveProvider();

            WaveFileWriter.CreateWaveFile(saveDirectory.SelectedPath + "\\" + "result.wav", waveProvider);
        }

        void AddSampleInMixer(NAudio.Wave.SampleProviders.MixingSampleProvider mixer, AudioFileReader audioFileReader)
        {
            mixer.AddMixerInput((ISampleProvider)audioFileReader);
        }

        void ConvertToMP3()
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.FileName = "lame.exe";
            psi.Arguments = "-V2 " + saveDirectory.SelectedPath + @"\" + "result.wav " + saveDirectory.SelectedPath + @"\" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm") + ".mp3";
            psi.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = Process.Start(psi);

            p.WaitForExit();
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool isExpanded = false;

        private void openRecordsButton_Click(object sender, EventArgs e)
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

        private void openButton_Click(object sender, EventArgs e)
        {
            //string argument = "/select, \"" + saveDirectory.SelectedPath + dataGridView1.CurrentRow.Cells[1].Value + "_" + dataGridView1.CurrentRow.Cells[2].Value + "\"";

            //Process p = Process.Start("explorer.exe", argument);

            //p.WaitForExit();

            string selectedFile = saveDirectory.SelectedPath + "\\" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "_" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + ".mp3";

            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", selectedFile));
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            Record record = (Record)dataGridView1.CurrentRow.DataBoundItem;
            if (!record.Id.HasValue)
            {
                MessageBox.Show("Select a row first");
            }
            else
            {
                _rep.Delete(record);
                ReadRecords();
            }
        }

        private void runAtStartUpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (runAtStartUpCheckBox.Checked)
            {
                rk.SetValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, Application.ExecutablePath);
            }

            else
            {
                rk.DeleteValue(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, false);
            }
        }
    }
}
