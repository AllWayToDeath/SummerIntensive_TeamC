namespace Slack_Recorder.Models
{
    public class Record
    {
        public int? Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public override string ToString()
        {
            return Date + " " + Time;
        }
    }
}
