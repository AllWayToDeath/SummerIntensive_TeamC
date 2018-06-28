using System;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Toolkit.Uwp.Notifications;
using Tulpep.NotificationWindow;
using System.Drawing;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using Yeti.MMedia;
using Yeti.MMedia.Mp3;
using WaveLib;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // WaveIn - поток для записи
        WaveIn waveIn;
        //Класс для записи в файл
        WaveFileWriter writer;
        //Имя файла для записи
        string outputFilename = "имя_файла.wav";
        public Form1()
        {
            InitializeComponent();
        }

        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;

        
        

        private void Form1_Load(object sender, EventArgs e)
        {
            startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"Slack.exe\""));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"Slack.exe\""));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();
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

                    notifyIcon.Visible = true;
                    notifyIcon.ShowBalloonTip(1000, "Slack Recorder", "Record started", ToolTipIcon.Info);
                    notifyIcon.Visible = false;

                    counter = 0;
                }
                counter++;
            }
        }
        // ЗАПИСЬ ЗВУКА И КОНВЕРТИРОВАНИЕ ИХ В mp3
        //Получение данных из входного буфера и обработка полученных с микрофона данных
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler<WaveInEventArgs>(waveIn_DataAvailable), sender, e);
            }
            else
            {
                //Записываем данные из буфера в файл
                writer.WriteData(e.Buffer, 0, e.BytesRecorded);
            }
        }
        //Завершаем запись
        void StopRecording()
        {
            MessageBox.Show("StopRecording");
            waveIn.StopRecording();
            MP3Convert();
        }
        //Окончание записи
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



        //Начинаем запись
        private void recordButton_Click(object sender, EventArgs e)
        {
           if (recordButton.Text == "Start recording")
           {
                startWatch.Start();
                stopWatch.Start();
                // ПРЕРЫВАЕМ ЗАПИСЬ
                if (waveIn != null)
                {
                    StopRecording();
                }
                recordButton.Text = "Stop recording";
           }

           else
           {
                startWatch.Stop();
                stopWatch.Stop();
                // ЗАПИСЬ ЗВУКА
                try
                {
                    //MessageBox.Show("Start Recording");
                    waveIn = new WaveIn();
                    //Дефолтное устройство для записи (если оно имеется)
                    waveIn.DeviceNumber = 0;
                    //Прикрепляем к событию DataAvailable обработчик, возникающий при наличии записываемых данных
                    waveIn.DataAvailable += waveIn_DataAvailable;
                    //Прикрепляем обработчик завершения записи
                    waveIn.RecordingStopped += waveIn_RecordingStopped;
                    //Формат wav-файла - принимает параметры - частоту дискретизации и количество каналов(здесь mono)
                    waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
                    //Инициализируем объект WaveFileWriter
                    writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                    //Начало записи
                    waveIn.StartRecording();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
                recordButton.Text = "Start recording";
           }
        }
        // КОНВЕРТАЦИЯ
        public void MP3Convert()
        {
            writer.Close();
            WaveLib.WaveStream InStr = new WaveLib.WaveStream(outputFilename);
            try
            {
                Mp3Writer writer = new Mp3Writer(new FileStream("Готовый.mp3",
                                                    FileMode.Create), InStr.Format);
                try
                {
                    byte[] buff = new byte[writer.OptimalBufferSize];
                    int read = 0;
                    while ((read = InStr.Read(buff, 0, buff.Length)) > 0)
                    {
                        writer.Write(buff, 0, read);
                    }
                }
                finally
                {
                    writer.Close();
                }
            }
            finally
            {
                InStr.Close();
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
                //notifyIcon.ShowBalloonTip(1000, "test title", "test content", ToolTipIcon.Info);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void recordSwitch_Scroll(object sender, EventArgs e)
        {

        }
    }
}
