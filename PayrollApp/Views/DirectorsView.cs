using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayrollApp.Views
{
    class DirectorsView
    {
        public User User { get; }

        public DirectorsView(User user)
        {
            User = user;
        }
        public string GetStartScreenAction(string message)
        {
            Console.Clear();
            if(message != null)
            {
                Console.WriteLine(message);
                Console.WriteLine();
            }
            Console.WriteLine($"Здравствуйте, {User.Name}!");
            Console.WriteLine($"Ваша роль: {User.Role}");
            Console.WriteLine("Выберите желаемое действие:");
            Console.WriteLine("(1). Добавить сотрудника");
            Console.WriteLine("(2). Просмотреть отчет по всем сотрудникам");
            Console.WriteLine("(3). Просмотреть отчет по конкретному сотруднику");
            Console.WriteLine("(4). Добавить часы работы");
            Console.WriteLine("(5). Выход из программы");
            return Console.ReadLine();
        }

        public User GetAddUserActionScreen(string errorMessage)
        {
            Console.Clear();
            if(errorMessage != null)
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            if (name == string.Empty) throw new ArgumentException("Имя не может быть пустым");
            Console.WriteLine("Введите роль:");
            string role = Console.ReadLine();
            if(!AppConfig.Roles.Contains(role))
            {
                throw new ArgumentException("Некорректная роль\nролью может быть только\n \"сотрудник\", \"руководитель\", \"фрилансер\"");
            }
            return new User(name, role);
        }

        public string GetReportScreen(string msg)
        {
            Console.Clear();
            if (msg != null) Console.WriteLine(msg + "\n");
            Console.WriteLine("За какой приод показать отчет");
            Console.WriteLine("(1) один день");
            Console.WriteLine("(2) неделя");
            Console.WriteLine("(3) месяц");
            return Console.ReadLine();
        }
        public User AddHoursScreen()
        {
            Console.Clear();
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите роль:");
            string role = Console.ReadLine();
            return new User(name, role);
        }

        public string GetReportByNameScreen(string msg)
        {
            Console.Clear();
            if(msg != null) Console.WriteLine(msg + '\n');
            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();
            return name;
        }
        public string DrawReportPerPerson(DateTime start, DateTime end, string name, List<ReportNote> reportNotes, decimal total)
        {
            Console.Clear();
            Console.WriteLine($"Отчет по сотруднику: {name} за период с {start.ToShortDateString()} по {end.ToShortDateString()}");
            reportNotes.ForEach((reportNote) => { Console.WriteLine(reportNote.ToString()); });
            Console.WriteLine($"Итого: {reportNotes.Sum(rn => rn.Hours)} часов, заработано: {total} руб");
            Console.WriteLine("Желаете посмотреть еще один отчет? Нажмите (Д)а для продолжения. (Н)ет для выхода на главный экран программы.");
            return Console.ReadLine();
        }
        public string DrawReport(DateTime start, DateTime end, List<AllUsersReportNote> reportNotes)
        {
            Console.Clear();
            Console.WriteLine($"Отчет за период с {start.ToShortDateString()} по {end.ToShortDateString()}");
            reportNotes.ForEach((reportNote) => { Console.WriteLine(reportNote.ToString()); });
            Console.WriteLine($"Всего часов отработано за период {reportNotes.Sum(rn => rn.Hours)}, сумма к выплате {reportNotes.Sum(rn => rn.Salary)}");
            Console.WriteLine("Желаете посмотреть еще один отчет? Нажмите (Д)а для продолжения. (Н)ет для выхода на главный экран программы.");
            return Console.ReadLine();
        }
        public string GetAddHoursScreen(string msg)
        {
            Console.Clear();
            if (msg != null) Console.WriteLine(msg + '\n');
            Console.WriteLine("Выберете опцию добавдения часов");
            Console.WriteLine("(1) За текущий день");
            Console.WriteLine("(2) Задним числом");
            return Console.ReadLine();
        }
        public ReportNote GetThisDayAddHoursForm()
        {
            Console.Clear();
            Console.WriteLine("Введите имя пользователя:");
            string name = Console.ReadLine();
            if (name == string.Empty) throw new ArgumentException("Имя не может быть пустым");
            Console.WriteLine("Введите количество отработанных часов:");
            string number = Console.ReadLine();
            Console.WriteLine("Введите комментарий");
            string comment = Console.ReadLine();
            if (comment == string.Empty) throw new ArgumentException("Комментарий не может быть пустым");
            return new ReportNote(name, int.Parse(number), comment);
        }
        public ReportNote GetAddHoursForm()
        {
            Console.Clear();
            Console.WriteLine("Введите имя пользователя:");
            string name = Console.ReadLine();
            if (name == string.Empty) throw new ArgumentException("Имя не может быть пустым");
            Console.WriteLine("Введите количество отработанных часов:");
            string number = Console.ReadLine();
            Console.WriteLine("Введите дату в формате день.месяц.год:");
            string[] attr = Console.ReadLine().Split('.');
            DateTime date = new DateTime(int.Parse(attr[2]), int.Parse(attr[1]), int.Parse(attr[0]));
            Console.WriteLine("Введите комментарий");
            string comment = Console.ReadLine();
            if (comment == string.Empty) throw new ArgumentException("Комментарий не может быть пустым");
            return new ReportNote(date, name, int.Parse(number), comment);
        }
    }
}
