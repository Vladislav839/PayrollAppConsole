using BusinessLogic.Models;
using BusinessLogic.Services;
using Data;
using PayrollApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollApp.Controllers
{
    class EmployeesController
    {
        public EmployeesView EmployeesView { get; }
        public RepositoryService RepositoryService { get; }
        public User User { get; }
        public EmployeesController(EmployeesView employeesView, RepositoryService repositoryService, User user)
        {
            EmployeesView = employeesView;
            RepositoryService = repositoryService;
            User = user;
        }
        public void Run(string msg)
        {
            string action = EmployeesView.GetStartScreenAction(msg);
            switch (action)
            {
                case "1":
                    GetSelfReport(null);
                    break;
                case "2":
                    AddHours(null);
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Run("Такой опции не существует");
                    break;
            }
        }

        private void GetSelfReport(string msg)
        {
            DirectorsView directorsView = new DirectorsView(User);
            string option = directorsView.GetReportScreen(msg);
            List<ReportNote> reportNotes = RepositoryService.GetAllReportNotes().Where(rn => rn.UserName == User.Name).ToList();
            switch (option)
            {
                case "1":
                    {
                        List<ReportNote> reportNotesPerPeriod = reportNotes.Where(rn => rn.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                        SalaryCalculator salaryCalculator = new SalaryCalculator(User, reportNotesPerPeriod);
                        string response = directorsView.DrawReportPerPerson(DateTime.Now, DateTime.Now, User.Name, reportNotesPerPeriod, salaryCalculator.GetSalaryPerPeriod());
                        switch (response)
                        {
                            case "Д":
                                GetSelfReport(null);
                                break;
                            case "Н":
                                Run(null);
                                break;
                            default:
                                Run("Такая опция не определена\nВы перенаправлены на главный экран");
                                break;
                        }
                        return;
                    }
                case "2":
                    {
                        List<ReportNote> reportNotesPerPeriod = reportNotes.Where(rn => rn.Date >= DateTime.Now.AddDays(-7)).ToList();
                        SalaryCalculator salaryCalculator = new SalaryCalculator(User, reportNotesPerPeriod);
                        string response = directorsView.DrawReportPerPerson(DateTime.Now.AddDays(-7), DateTime.Now, User.Name, reportNotesPerPeriod, salaryCalculator.GetSalaryPerPeriod());
                        switch (response)
                        {
                            case "Д":
                                GetSelfReport(null);
                                break;
                            case "Н":
                                Run(null);
                                break;
                            default:
                                Run("Такая опция не определена\nВы перенаправлены на главный экран");
                                break;
                        }
                        return;
                    }
                case "3":
                    {
                        List<ReportNote> reportNotesPerPeriod = reportNotes.Where(rn => rn.Date >= DateTime.Now.AddMonths(-1)).ToList();
                        SalaryCalculator salaryCalculator = new SalaryCalculator(User, reportNotesPerPeriod);
                        string response = directorsView.DrawReportPerPerson(DateTime.Now.AddDays(-7), DateTime.Now, User.Name, reportNotesPerPeriod, salaryCalculator.GetSalaryPerPeriod());
                        switch (response)
                        {
                            case "Д":
                                GetSelfReport(null);
                                break;
                            case "Н":
                                Run(null);
                                break;
                            default:
                                Run("Такая опция не определена\nВы перенаправлены на главный экран");
                                break;
                        }
                        return;
                    }
                default:
                    GetSelfReport("Такой опции не существует\n");
                    return;

            }

        }
        private void AddHours(string msg)
        {
            DirectorsView directorsView = new DirectorsView(User);
            string option = directorsView.GetAddHoursScreen(msg);
            try
            {
                switch (option)
                {
                    case "1":
                        {
                            ReportNote reportNote = EmployeesView.GetThisDayAddHoursForm(User);
                            User user = RepositoryService.Database.Users.FirstOrDefault(u => u.Name == reportNote.UserName);
                            if (user == null) throw new ArgumentException("Пользователя с таким именем не сущестует.\nДобавьте его сначала в список пользователей\n");
                            RepositoryService.AddReportNote(reportNote, user.Role);
                            Run("Запись успешно добавлена" + '\n');
                            break;
                        }
                    case "2":
                        {
                            ReportNote reportNote = EmployeesView.GetAddHoursForm(User);
                            User user = RepositoryService.Database.Users.FirstOrDefault(u => u.Name == reportNote.UserName);
                            if (user == null) throw new ArgumentException("Пользователя с таким именем не сущестует.\nДобавьте его сначала в список пользователей\n");
                            RepositoryService.AddReportNote(reportNote, user.Role);
                            Run("Запись успешно добавлена" + '\n');
                            break;
                        }
                    default:
                        AddHours("Такой опции не существует\n");
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                AddHours("Неверный ввод даты, введите ее в формате день.месяц.год");
            }
            catch (IndexOutOfRangeException)
            {
                AddHours("Неверный ввод даты, введите ее в формате день.месяц.год");
            }
            catch (FormatException)
            {
                AddHours("Некорректный ввод, кол-во отработаных часов должно быть числом");
            }
            catch (ArgumentException ex)
            {
                AddHours(ex.Message);
            }
        }
    }
}
