using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class ReportNote : IComparable
    {
        public DateTime Date { get; }
        public string UserName { get; }
        public int Hours { get; }
        public string Message { get; }
        public ReportNote(string userName, int hours, string message)
        {
            Date = DateTime.Now;
            UserName = userName;
            Hours = hours;
            Message = message;
        }

        public ReportNote(DateTime date, string userName, int hours, string message)
        {
            Date = date;
            UserName = userName;
            Hours = hours;
            Message = message;
        }
        public override string ToString()
        {
            return $"{Date:d},{UserName},{Hours},{Message}";
        }

        public int CompareTo(object obj)
        {
            ReportNote reportNote = (ReportNote)obj;
            return Date.CompareTo(reportNote.Date);
        }
    }
}
