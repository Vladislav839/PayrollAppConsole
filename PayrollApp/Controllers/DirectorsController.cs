using BusinessLogic.Models;
using Data;
using PayrollApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Services;
using System.Text;

namespace PayrollApp.Controllers
{
    class DirectorsController
    {
        public DirectorsView DirectorsView { get; }
        public RepositoryService RepositoryService { get; }
        public DirectorsController(DirectorsView directorsView, RepositoryService repositoryService)
        {
            DirectorsView = directorsView;
            RepositoryService = repositoryService;
        }
        public void Run(string msg)
        {
            string action = DirectorsView.GetStartScreenAction(msg);
            switch (action)
            {
                case "1":
                    AddUser(null);
                    Run("Пользователь успешно добавлен\nВы перенаправлены на главный экран");
                    break;
                case "2":
                    GetFullReport(null);
                    break;
                case "3":
                    GetReportByName(null);
                    break;
                case "4":
                    AddHours(null);
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Run("Такой опции не существует");
                    break;
            }
        }
        private void AddUser(string errorMessage)
        {
            try
            {
                User user = DirectorsView.GetAddUserActionScreen(errorMessage);
                if(RepositoryService.Database.Users.FirstOrDefault(u => u.Name == user.Name) != null)
                {
                    AddUser("Пользователь с таким именем уже существует\n");
                    return;
                }
                RepositoryService.AddUser(user);
            }
            catch (ArgumentException ex)
            {
                AddUser(ex.Message);
            }
        }
        private void GetReportByName(string msg)
        {
            string name = DirectorsView.GetReportByNameScreen(msg);
            User user = RepositoryService.Database.Users.FirstOrDefault(u => u.Name == name);
            if (user == null)
            {
                GetReportByName("Пользователя с таким именем не существует\n");
            }
            string option = DirectorsView.GetReportScreen(msg);
            List<ReportNote> reportNotes = RepositoryService.GetAllReportNotes().Where(rn => rn.UserName == name).ToList();
            switch (option)
            {
                case "1":
                    {
                        List<ReportNote> reportNotesPerPeriod = reportNotes.Where(rn => rn.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                        SalaryCalculator salaryCalculator = new SalaryCalculator(user, reportNotesPerPeriod);
                        string response = DirectorsView.DrawReportPerPerson(DateTime.Now, DateTime.Now, name, reportNotesPerPeriod, salaryCalculator.GetSalaryPerPeriod());
                        switch (response)
                        {
                            case "Д":
                                GetReportByName(null);
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
                        SalaryCalculator salaryCalculator = new SalaryCalculator(user, reportNotesPerPeriod);
                        string response = DirectorsView.DrawReportPerPerson(DateTime.Now.AddDays(-7), DateTime.Now, name, reportNotesPerPeriod, salaryCalculator.GetSalaryPerPeriod());
                        switch (response)
                        {
                            case "Д":
                                GetReportByName(null);
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
                        SalaryCalculator salaryCalculator = new SalaryCalculator(user, reportNotesPerPeriod);
                        string response = DirectorsView.DrawReportPerPerson(DateTime.Now.AddDays(-7), DateTime.Now, name, reportNotesPerPeriod, salaryCalculator.GetSalaryPerPeriod());
                        switch (response)
                        {
                            case "Д":
                                GetReportByName(null);
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
                    GetReportByName("Такой опции не существует\n");
                    return;

            }

        }
        private void GetFullReport(string msg)
        {
            string option = DirectorsView.GetReportScreen(msg);
            List<ReportNote> reportNotes = RepositoryService.GetAllReportNotes();
            switch (option)
            {
                case "1":
                    {
                        List<ReportNote> reportNotesPerPeriod = reportNotes.Where(rn => rn.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                        string response = DirectorsView.DrawReport(DateTime.Now, DateTime.Now, ReportNotesService.GetAllUsersReportNotes(reportNotesPerPeriod, RepositoryService.Database.Users));
                        switch (response)
                        {
                            case "Д":
                                GetFullReport(null);
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
                        string response = DirectorsView.DrawReport(DateTime.Now.AddDays(-7), DateTime.Now, ReportNotesService.GetAllUsersReportNotes(reportNotesPerPeriod, RepositoryService.Database.Users));
                        switch (response)
                        {
                            case "Д":
                                GetFullReport(null);
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
                        string response = DirectorsView.DrawReport(DateTime.Now.AddMonths(-1), DateTime.Now, ReportNotesService.GetAllUsersReportNotes(reportNotesPerPeriod, RepositoryService.Database.Users));
                        switch (response)
                        {
                            case "Д":
                                GetFullReport(null);
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
                    GetFullReport("Такой опции не существует\n");
                    return;

            }
        }
        private void AddHours(string msg)
        {
            string option = DirectorsView.GetAddHoursScreen(msg);
            try
            {
                switch (option)
                {
                    case "1":
                        {
                            ReportNote reportNote = DirectorsView.GetThisDayAddHoursForm();
                            User user = RepositoryService.Database.Users.FirstOrDefault(u => u.Name == reportNote.UserName);
                            if (user == null) throw new ArgumentException("Пользователя с таким именем не сущестует.\nДобавьте его сначала в список пользователей\n");
                            RepositoryService.AddReportNote(reportNote, user.Role);
                            Run("Запись успешно добавлена" + '\n');
                            break;
                        }
                    case "2":
                        {
                            ReportNote reportNote = DirectorsView.GetAddHoursForm();
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
            catch(ArgumentException ex)
            {
                AddHours(ex.Message);
            }
        }

    }
}
