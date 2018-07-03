/*using Slack_Recorder.DAL;
using Slack_Recorder.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Slack_Recorder
{
    public class Repository : DbConnection 
    {
        public List<Record> GetRecords()
        {
            var records = new List<Record>();

            const string sqlExpression = "SELECT * FROM [dbo].[Table];";

            var command = new SqlCommand(sqlExpression, this.Connection);
            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    records.Add(new Record
                    {
                        Id = (int)reader["Id"],
                        Date = (string)reader["Date"],
                        Time = (string)reader["Time"],
                    });
                }
            }
            reader.Close();

            return records;
        }

        public void Insert(Record record)
        {
            const string sqlExpression = "INSERT INTO [dbo].[Table] ([Date], [Time]) VALUES (@Date, @Time) ; SELECT SCOPE_IDENTITY()";

            using (var command = new SqlCommand(sqlExpression, this.Connection))
            {
                command.Parameters.Add("@Date", SqlDbType.NVarChar, 50).Value = record.Date;
                command.Parameters.Add("@Time", SqlDbType.NVarChar, 50).Value = record.Time;

                var identity = command.ExecuteScalar();
            }
        }

        public void Delete(Record record)
        {
            const string sqlExpression = "DELETE FROM [dbo].[Table] WHERE Id = @Id";

            using (var command = new SqlCommand(sqlExpression, this.Connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = record.Id;

                int affectedRowsCount = command.ExecuteNonQuery(); // тут вылезает какая то фигня
            }
        }
    }
}*/
