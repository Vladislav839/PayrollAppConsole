using BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace Data
{
    public class Repository
    {
        public List<User> Users { get; }
        public List<ReportNote> DirectorsNotes { get; }
        public List<ReportNote> EmployeesNotes { get; }
        public List<ReportNote> FreelansersNotes { get; }
        public Repository(List<User> users, List<ReportNote> emloyeesNotes, 
            List<ReportNote> directorsNotes, List<ReportNote> freelancersNotes)
        {
            Users = users;
            DirectorsNotes = directorsNotes;
            EmployeesNotes = emloyeesNotes;
            FreelansersNotes = freelancersNotes;
        }
    }
}
