using System;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.IO;
using System.Data.SQLite;
using System.Data;

using Microsoft.Win32;
using Slack_Recorder.Models;
using NAudio.Wave;
using NAudio.Lame;
using System.Linq;

namespace Slack_Recorder
{
    public partial class Form1 : Form
    {
        bool isExpanded = false;
        public static int counter = 0;

        FolderBrowserDialog saveDirectory = new FolderBrowserDialog();

        ManagementEventWatcher startWatch;
        ManagementEventWatcher stopWatch;

        //recorder and writer for mic
        WaveInEvent waveIn = null;
        WaveFileWriter writer = null;
        string micRecordFIleName;

        //recorder and writer for speaker
        WasapiLoopbackCapture CaptureInstance = null;
        WaveFileWriter RecordedAudioWriter = null;
        string playBackRecordFileName;

        private String dbFileName;
        private SQLiteConnection m_dbConn;
        private SQLiteCommand m_sqlCmd;

        private string saveDate;
        private string saveTime;

        AudioFileReader MicFileReader = null;
        AudioFileReader SpeakerFileReader = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToDatabase();
            ReadFromDatabase();

            saveDirectory.SelectedPath = saveDirectoryTextBox.Text;

            startWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace WHERE ProcessName = \"Slack.exe\""));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace WHERE ProcessName = \"Slack.exe\""));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();
        }

        private void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Slack");
            if (proc.Length < 8)
            {
                if (counter == 3)
                {
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(1000, "Slack Recorder", "Call recorded", ToolTipIcon.Info);
                    notifyIcon1.Visible = false;

                    if (waveIn != null)
                    {
                        try
                        {
                            waveIn.StopRecording();
                            CaptureInstance.StopRecording();

                            saveDate = DateTime.Now.ToString("dd.MM.yyyy");
                            saveTime = DateTime.Now.ToString("HH.mm.ss");

                            MixTwoSamples();

                            ConvertToMP3(saveDirectory.SelectedPath + "\\" + "result.wav", saveDirectory.SelectedPath + "\\" + saveDate + "_" + saveTime + ".mp3", 128);

                            DeleteTempFiles();

                            InsertIntoDatabase(saveDate, saveTime, saveDirectory.SelectedPath);
                        }
                        catch (NullReferenceException ex)
                        {
                            waveIn.StopRecording();
                            CaptureInstance.StopRecording();
                        }
                        
                    }
                    counter = 0;
                }
                counter++;
            }
        }

        private void DeleteTempFiles()
        {
            File.Delete(saveDirectory.SelectedPath + "\\" + "result.wav");
            File.Delete(saveDirectory.SelectedPath + "\\" + micRecordFIleName);
            File.Delete(saveDirectory.SelectedPath + "\\" + playBackRecordFileName);
        }

        void ConvertToMP3(string waveFileName, string mp3FileName, int bitRate = 128)
        {
            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);
        }

        private void MixTwoSamples()
        {
            while (writer == null && RecordedAudioWriter == null)
            {

            }

            Thread.Sleep(1000);

            MicFileReader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + micRecordFIleName);
            SpeakerFileReader = new AudioFileReader(saveDirectory.SelectedPath + "\\" + playBackRecordFileName);

            WaveMixerStream32 mixer = new WaveMixerStream32();

            mixer.AddInputStream(MicFileReader);
            mixer.AddInputStream(SpeakerFileReader);

            WaveFileWriter.CreateWaveFile(saveDirectory.SelectedPath + "\\" + "result.wav", mixer);

            MicFileReader.Close();
            MicFileReader.Dispose();
            MicFileReader = null;

            SpeakerFileReader.Close();
            SpeakerFileReader.Dispose();
            SpeakerFileReader = null;
        }

        private void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Process[] proc = Process.GetProcessesByName("Slack");
            if (proc.Length >= 8)
            {
                if (counter == 3)
                {
                    try
                    {
                        if (MicFileReader != null && SpeakerFileReader != null)
                        {
                            MicFileReader.Close();
                            MicFileReader.Dispose();
                            MicFileReader = null;

                            SpeakerFileReader.Close();
                            SpeakerFileReader.Dispose();
                            SpeakerFileReader = null;
                        }

                        micRecordFIleName = RandomString(15) + ".wav";
                        playBackRecordFileName = RandomString(15) + ".wav";

                        if (waveIn != null)
                        {
                            waveIn.StopRecording();
                        }

                        waveIn = new WaveInEvent();

                        waveIn.DeviceNumber = 0;
                        waveIn.WaveFormat = new NAudio.Wave.WaveFormat(44100, 32, 2);

                        waveIn.DataAvailable += waveIn_DataAvailable;
                        waveIn.RecordingStopped += waveIn_RecordingStopped;

                        try
                        {
                            writer = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + micRecordFIleName, waveIn.WaveFormat);
                        }
                        catch (Exception ex)
                        {
                            writer.Close();
                            writer.Dispose();
                            writer = null;

                            writer = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + micRecordFIleName, waveIn.WaveFormat);
                        }

                        waveIn.StartRecording();

                        if (CaptureInstance != null)
                        {
                            CaptureInstance.StopRecording();
                        }

                        CaptureInstance = new WasapiLoopbackCapture();
                        
                        try
                        {
                            RecordedAudioWriter = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + playBackRecordFileName, CaptureInstance.WaveFormat);
                        }
                        catch (Exception ex)
                        {
                            RecordedAudioWriter.Close();
                            RecordedAudioWriter.Dispose();
                            RecordedAudioWriter = null;

                            RecordedAudioWriter = new WaveFileWriter(saveDirectory.SelectedPath + "\\" + playBackRecordFileName, CaptureInstance.WaveFormat);
                        }    
                    
                        CaptureInstance.DataAvailable += (s, a) =>
                        {
                            RecordedAudioWriter.Write(a.Buffer, 0, a.BytesRecorded);
                        };

                        CaptureInstance.RecordingStopped += (s, a) =>
                        {
                            
                            if (RecordedAudioWriter != null)
                            {
                                try
                                {
                                    RecordedAudioWriter.Dispose();  //здесь может быть баг
                                    RecordedAudioWriter = null;
                                }
                                catch (Exception ex)
                                {
                                    File.Delete(saveDirectory.SelectedPath + "\\" + micRecordFIleName);
                                    File.Delete(saveDirectory.SelectedPath + "\\" + playBackRecordFileName);
                                }
                            }

                            if (CaptureInstance != null)
                            {
                                CaptureInstance.Dispose();
                                CaptureInstance = null;
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

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
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

        private void ConnectToDatabase()
        {
            m_dbConn = new SQLiteConnection();
            m_sqlCmd = new SQLiteCommand();

            dbFileName = "CallsDatabase.sqlite";

            if (!File.Exists(dbFileName))
                SQLiteConnection.CreateFile(dbFileName);

            m_dbConn = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;

            m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Calls (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Time TEXT, Path TEXT)";
            m_sqlCmd.ExecuteNonQuery();
        }

        private void ReadFromDatabase()
        {
            DataTable dTable = new DataTable();
            String sqlQuery;

            sqlQuery = "SELECT * FROM Calls";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, m_dbConn);
            adapter.Fill(dTable);
            dataGridView.Rows.Clear();

            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                dataGridView.Rows.Add(dTable.Rows[i].ItemArray);
            }
        }

        private void InsertIntoDatabase(string Date, string Time, string Path)
        {
            m_sqlCmd.CommandText = "INSERT INTO Calls ('Date', 'Time', 'Path') values ('" + Date + "' , '" + Time + "' , '" + Path + "')";
            m_sqlCmd.ExecuteNonQuery();
        }

        private void DeleteFromDatabase()
        {
            Record record = new Record();
            record.Id = Convert.ToInt16(dataGridView.CurrentRow.Cells[0].Value);
            record.Date = dataGridView.CurrentRow.Cells[1].Value.ToString();
            record.Time = dataGridView.CurrentRow.Cells[2].Value.ToString();

            if (!record.Id.HasValue)
            {
                MessageBox.Show("Select a row first");
            }
            else
            {
                m_sqlCmd.CommandText = "DELETE FROM Calls WHERE Id = @Id";
                m_sqlCmd.Parameters.AddWithValue("@Id", record.Id);
                m_sqlCmd.ExecuteNonQuery();
                File.Delete(saveDirectory.SelectedPath + "\\" + record.Date + "_" + record.Time + ".mp3");
            }
        }

        /*private void ReadRecords()
        {
            List<Record> clients = _rep.GetRecords();
           
            BindingList<Record> bindingList = new BindingList<Record>(clients);
            this.dataGridView.DataSource = new BindingSource(bindingList, null);
        }*/

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
                ReadFromDatabase();
                for (int i = 0; i < 86; i++)
                {
                    this.Width += 5;
                    Thread.Sleep(5);
                }
                isExpanded = true;
            }

            else
            {
                for (int i = 0; i < 86; i++)
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

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            DeleteFromDatabase();
            ReadFromDatabase();
        }

        private void openRecordButton_Click(object sender, EventArgs e)
        {
            string selectedFile = dataGridView.CurrentRow.Cells[3].Value.ToString() + "\\" + dataGridView.CurrentRow.Cells[1].Value.ToString() + "_" + dataGridView.CurrentRow.Cells[2].Value.ToString() + ".mp3";

            Process.Start("explorer.exe", string.Format("/select,\"{0}\"", selectedFile));
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Process.Start("NewProject.chm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadFromDatabase();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 0)
            {
                openRecordButton.Size = new System.Drawing.Size(148, 23);
                deleteSelectedButton.Size = new System.Drawing.Size(151, 23);
            }

            else
            {
                openRecordButton.Size = new System.Drawing.Size(117, 23);
                deleteSelectedButton.Size = new System.Drawing.Size(122, 23);
            }
        }
    }
}
