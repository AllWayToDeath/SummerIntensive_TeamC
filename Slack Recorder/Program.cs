using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slack_Recorder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*Application.Run(new Form1());
            try
            {
                
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                MessageBox.Show("Can't connect to voice call server");
            }*/
            StartApp();
        }

        static void StartApp()
        {
            try
            {
                Application.Run(new Form1());
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                MessageBox.Show("Can't connect to voice call server.\nPlease, drop current call\nRecorder will be relaunched", "Slack recorder", MessageBoxButtons.OK);
                StartApp();
            }
        }
    }
}
