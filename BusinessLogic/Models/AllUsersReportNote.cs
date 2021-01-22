using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class AllUsersReportNote
    {
        public string UserName { get; }
        public int Hours { get; }
        public decimal Salary { get; }
        public AllUsersReportNote(string name, int hours, decimal salary)
        {
            UserName = name;
            Hours = hours;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"{UserName} отработал {Hours} часов и заработал за период {Salary} руб";
        }
    }
}
