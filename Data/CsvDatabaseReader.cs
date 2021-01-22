using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data
{
    class CsvDatabaseReader
    {
        private static User GetUserFromString(string info)
        {
            string[] attributes = info.Split(',');
            return new User(attributes[0], attributes[1]);
        }
        private static ReportNote GetReportNotefromString(string info)
        {
            string[] attributes = info.Split(',');
            return new ReportNote(Convert.ToDateTime(attributes[0]), attributes[1], int.Parse(attributes[2]), attributes[3]);
        }
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            FileInfo file = new FileInfo(CsvDatabaseConfig.Path + CsvDatabaseConfig.UsersFileName);
            if(file.Exists)
            {
                using (var sr = new StreamReader(CsvDatabaseConfig.Path + CsvDatabaseConfig.UsersFileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        users.Add(GetUserFromString(line));
                    }
                }
            }
            return users;
        }
        public static List<ReportNote> GetReportNotes(string fileName)
        {
            List<ReportNote> reportNotes = new List<ReportNote>();
            FileInfo file = new FileInfo(CsvDatabaseConfig.Path + fileName);
            if(file.Exists)
            {
                using (var sr = new StreamReader(CsvDatabaseConfig.Path + fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        reportNotes.Add(GetReportNotefromString(line));
                    }
                }
            }
            return reportNotes;
        }
    }
}
