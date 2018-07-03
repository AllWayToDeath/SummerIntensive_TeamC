using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Mp3File
    {
        private string Name;
        private string Path;
        private string Duration;

        public string get_Name()
        {
            return Name;
        }
        public string get_Path()
        {
            return Path;
        }

        public string get_Duration()
        {
            return Duration;
        }
        public void set_Name(string name)
        {
            Name = name;
        }
        public void set_Path(string path)
        {
           Path = path;
        }

        public void set_Duration(string duration)
        {
            Duration= duration;
        }

        public Mp3File(string name, string path, string duration)
        {
            Name = name;
            Path = path;
            Duration = duration;

        }



    }
}
