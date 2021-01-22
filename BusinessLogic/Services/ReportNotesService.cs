using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class ReportNotesService
    {
        private class ReportNoteComparer : IEqualityComparer<ReportNote>
        {
            public bool Equals(ReportNote x, ReportNote y)
            {
                if (ReferenceEquals(x, y)) return true;

                if (ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                return x.UserName.Equals(y.UserName);
            }

            public int GetHashCode(ReportNote obj)
            {
                if (ReferenceEquals(obj, null)) return 0;

                return obj.UserName.GetHashCode();
            }
        }

        public static Dictionary<string, List<ReportNote>> GetEachPersonNotes(List<ReportNote> reportNotes)
        {
            Dictionary<string, List<ReportNote>> eachPersonNotes = new Dictionary<string, List<ReportNote>>();
            List<string> uniqueNames = reportNotes.Distinct(new ReportNoteComparer()).Select(rn => rn.UserName).ToList();
            foreach (var userName in uniqueNames)
            {
                List<ReportNote> reportNotesPerPerson = new List<ReportNote>();
                foreach (var reportNote in reportNotes)
                {
                    if (reportNote.UserName == userName)
                    {
                        reportNotesPerPerson.Add(reportNote);
                    }
                }
                eachPersonNotes.Add(userName, reportNotesPerPerson);
            }
            return eachPersonNotes;
        }

        public static List<AllUsersReportNote> GetAllUsersReportNotes(List<ReportNote> reportNotes, List<User> users)
        {
            List<AllUsersReportNote> allUsersReportNotes = new List<AllUsersReportNote>();
            Dictionary<string, List<ReportNote>> eachPersonNotes = GetEachPersonNotes(reportNotes);
            foreach (KeyValuePair<string, List<ReportNote>> keyValue in eachPersonNotes)
            {
                User user = users.FirstOrDefault(u => u.Name == keyValue.Key);
                SalaryCalculator salaryCalculator = new SalaryCalculator(user, keyValue.Value);
                allUsersReportNotes.Add(new AllUsersReportNote
                    (
                        keyValue.Key,
                        keyValue.Value.Sum(rn => rn.Hours),
                        salaryCalculator.GetSalaryPerPeriod()
                    ));
            }
            return allUsersReportNotes;
        }
    }
}
