using System;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using NAudio.Wave;
using Microsoft.Win32;
using System.Threading;
using System.Data.SQLite;
using System.Data;
using TagLib.Mpeg;
using TagLib;
using NAudio.Lame;
using NAudio.CoreAudioApi;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string sql = "Select Name, Path, Duration From Records"; //Sql запрос (достать все из таблицы customer)

        static string path = "DBase.db"; //Путь к файлу БД

        string ConnectionString = "Data Source=" + path + ";Version=3;New=True;Compress=True;"; //Строка соеденения (так хочет sqlite)
        SQLiteConnection conn = null;
        IWaveIn waveIn;
        WaveFileWriter writer;

        IWaveIn CaptureInstance;
        WaveFileWriter RecordedAudioWriter;

        FolderBrowserDialog saveDirectory = new FolderBrowserDialog();

        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;
        string resultfile = "Result.wav";
        string mp3File = (DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString() + ".mp3").Replace(":", "_");

        string playbackRecordFileName = "Record_from_speakers.wav";
        string micRecordFIleName = "Record_from_mic.wav";

        public Form1()
        { 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            conn = new SQLiteConnection(ConnectionString); //Создаем соединение
            conn.Open();
            DataTable ds = Load_Db(conn);

            dataGridView1.DataSource = ds;
           
            

            startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"Slack.exe\""));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"Slack.exe\""));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();

            saveDirectory.SelectedPath = saveDirectoryTextBox.Text;


            
        }

       

 
        public static int counter = 0;
        DataTable Load_Db(SQLiteConnection conn)
        {
            SQLiteCommand sql = new SQLiteCommand();
            sql.CommandText ="Select Name, Path, Duration From Records";
            sql.Connection = conn;
            DataTable ds = new DataTable();
            SQLiteDataReader da = sql.ExecuteReader();

            ds.Load(da);//Заполняем DataSet cодержимым DataAdapter'a

            return ds;//Заполняем созданный на форме dataGridView1//Создаем объект класса DataAdapter (тут мы передаем наш запрос и получаем ответ)
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

                   if (waveIn == null && CaptureInstance == null)
                    {
                        return;

                    }
                        waveIn.StopRecording();
                        CaptureInstance.StopRecording();

                        MixTwoSamples();

                        ConvertToMP3(saveDirectory.SelectedPath + "//" + resultfile, saveDirectory.SelectedPath + "//" + mp3File, 320);
                        DeleteTempFile();
                        AddToDatabase();

                    


                    counter = 0;
                }
                counter++;
            }
        }
        void DeleteTempFile()
        {
            System.IO.File.Delete(saveDirectory.SelectedPath + "//" + micRecordFIleName);
            System.IO.File.Delete(saveDirectory.SelectedPath + "//" + playbackRecordFileName);
            System.IO.File.Delete(saveDirectory.SelectedPath + "//" + resultfile);
        }
        void AddToDatabase()
        {
            string Name = "";
            string Path = "";
            string Duration = "";
            using (var audio = new AudioFile(saveDirectory.SelectedPath + "\\" + mp3File, ReadStyle.Average))
            {
                Name = mp3File;
                Path = saveDirectory.SelectedPath;
                Duration = audio.Properties.Duration.ToString("hh\\:mm\\:ss");
            };
            Mp3File file = new Mp3File(Name, Path, Duration);


            SQLiteCommand CMD = conn.CreateCommand();
            CMD.CommandText = "insert into Records(Name, Path, Duration) values (@name, @path, @duration )";
            CMD.Parameters.AddWithValue("@name", file.get_Name() );
            CMD.Parameters.AddWithValue("@path", file.get_Path() );
            CMD.Parameters.AddWithValue("@duration", file.get_Duration() );
            CMD.ExecuteNonQuery();

           
        }

        void MixTwoSamples()
        {
            if (writer == null && RecordedAudioWriter == null)
            {
                using (AudioFileReader _in = new AudioFileReader(saveDirectory.SelectedPath + "//" + micRecordFIleName))
                using (AudioFileReader _out = new AudioFileReader(saveDirectory.SelectedPath + "//" + playbackRecordFileName))
                {
                    WaveMixerStream32 res = new WaveMixerStream32();

                    res.AddInputStream(_in);
                    res.AddInputStream(_out);

                    WaveFileWriter.CreateWaveFile(saveDirectory.SelectedPath + "//" + resultfile, res);
                }
            }
            else
            {
                writer.Dispose();
                RecordedAudioWriter.Dispose();
                writer = null;
                RecordedAudioWriter = null;
                MixTwoSamples();

            }
        }



        void ConvertToMP3(string waveFileName, string mp3FileName, int bitRate = 128)
        {
            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
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
                        waveIn = new WasapiCapture();
                        writer = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + micRecordFIleName, waveIn.WaveFormat);


                        waveIn.DataAvailable += (s, a) =>
                        {
                            writer.Write(a.Buffer, 0, a.BytesRecorded);
                        };
                        waveIn.RecordingStopped += (s, a) =>
                        {
                           
                            if (waveIn != null)
                            {
                                waveIn.Dispose();
                                waveIn = null;
                            }
                            if (a.Exception != null)
                            {
                                throw a.Exception;
                            }
                        };
                        waveIn.StartRecording();

                        CaptureInstance = new WasapiLoopbackCapture();
                        RecordedAudioWriter = new WaveFileWriter(saveDirectory.SelectedPath + "\\"+ playbackRecordFileName, CaptureInstance.WaveFormat);

                        CaptureInstance.DataAvailable += (s, a) =>
                        {
                            RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
                        };

                        CaptureInstance.RecordingStopped += (s, a) =>
                        {
                            
                            if (CaptureInstance != null)
                            {
                                CaptureInstance.Dispose();
                                CaptureInstance = null;
                            }
                            if (a.Exception != null)
                            {
                                throw a.Exception;
                            }
                        };
                        CaptureInstance.StartRecording();
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
           
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            
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
