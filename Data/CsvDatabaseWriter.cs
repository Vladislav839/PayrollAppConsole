using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Data
{
    class CsvDatabaseWriter
    {
        public static void WriteUser(User user)
        {
            using(var sw = new StreamWriter(CsvDatabaseConfig.Path + CsvDatabaseConfig.UsersFileName, true))
            {
                sw.WriteLine(user.ToString());
            }
        }
        public static void WriteReportNote(ReportNote reportNote, string filename)
        {
            using (var sw = new StreamWriter(CsvDatabaseConfig.Path + filename, true))
            {
                sw.WriteLine(reportNote.ToString());
            }
        }
    }
}
