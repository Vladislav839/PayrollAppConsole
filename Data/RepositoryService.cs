using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class RepositoryService
    {
        public Repository Database { get; }

        public RepositoryService()
        {
            Database = new Repository(CsvDatabaseReader.GetUsers(),
                CsvDatabaseReader.GetReportNotes(CsvDatabaseConfig.EmployeesNotesFileName),
                CsvDatabaseReader.GetReportNotes(CsvDatabaseConfig.DirectorsNotesFileName),
                CsvDatabaseReader.GetReportNotes(CsvDatabaseConfig.FreelancersNotesFileName));
        }

        public User GetUserByName(string name)
        {
            return Database.Users.FirstOrDefault(u => u.Name == name);
        }
        public List<ReportNote> GetAllReportNotes()
        {
            List<ReportNote> reportNotes = Database.DirectorsNotes;
            reportNotes.AddRange(Database.EmployeesNotes);
            reportNotes.AddRange(Database.FreelansersNotes);
            reportNotes.Sort();
            return reportNotes;
        }
        public void AddUser(User user)
        {
            Database.Users.Add(user);
            CsvDatabaseWriter.WriteUser(user);
        }
        public void AddReportNote(ReportNote reportNote, string role)
        {
            switch(role)
            {
                case "сотрудник":
                    Database.EmployeesNotes.Add(reportNote);
                    CsvDatabaseWriter.WriteReportNote(reportNote, CsvDatabaseConfig.EmployeesNotesFileName);
                    return;
                case "руководитель":
                    Database.DirectorsNotes.Add(reportNote);
                    CsvDatabaseWriter.WriteReportNote(reportNote, CsvDatabaseConfig.DirectorsNotesFileName);
                    return;
                case "фрилансер":
                    Database.FreelansersNotes.Add(reportNote);
                    CsvDatabaseWriter.WriteReportNote(reportNote, CsvDatabaseConfig.FreelancersNotesFileName);
                    return;
            }
        }
    }
}
