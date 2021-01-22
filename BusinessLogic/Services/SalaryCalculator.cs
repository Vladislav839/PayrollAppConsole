using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    public class SalaryCalculator
    {
        private const decimal directorsMonthSalary = 200000;
        private const decimal allUsersHours = 160;
        private const decimal workingHoursPerDay = 8;
        private const decimal employeeMonthSalary = 120000;
        private const decimal freelancerSalaryPerHour = 1000;
        private const decimal directorsMonthBonus = 20000;
        public User User { get; }
        public List<ReportNote> ReportNotes;
        public SalaryCalculator(User user, List<ReportNote> reportNotes)
        {
            User = user;
            ReportNotes = reportNotes;
        }

        public decimal GetSalaryPerPeriod()
        {
            decimal totalPay = 0;
            switch (User.Role)
            {
                case "руководитель":
                    foreach(var reportNote in ReportNotes)
                    {
                        if(reportNote.Hours <= 8)
                        {
                            totalPay += (directorsMonthSalary / allUsersHours) * reportNote.Hours;
                        }
                        else
                        {
                            totalPay += (directorsMonthSalary / allUsersHours) * reportNote.Hours + (directorsMonthBonus / allUsersHours) * workingHoursPerDay;
                        }
                    }
                    return totalPay;
                case "сотрудник":
                    foreach (var reportNote in ReportNotes)
                    {
                        if(totalPay > allUsersHours)
                        {
                            totalPay += (employeeMonthSalary / allUsersHours) * reportNote.Hours * 2;
                        }
                        else
                        {
                            totalPay += (employeeMonthSalary / allUsersHours) * reportNote.Hours;
                        }
                    }
                    return totalPay;
                case "фрилансер":
                    foreach(var reportNote in ReportNotes)
                    {
                        totalPay += freelancerSalaryPerHour * reportNote.Hours;
                    }
                    return totalPay;
                default:
                    return -1;
            }
        }
    }
}
