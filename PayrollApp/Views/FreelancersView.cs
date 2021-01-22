using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollApp.Views
{
    public class FreelancersView
    {
        public User User { get; }

        public FreelancersView(User user)
        {
            User = user;
        }

        public string GetStartScreenAction(string message)
        {
            Console.Clear();
            if (message != null)
            {
                Console.WriteLine(message);
                Console.WriteLine();
            }
            Console.WriteLine($"Здравствуйте, {User.Name}!");
            Console.WriteLine($"Ваша роль: {User.Role}");
            Console.WriteLine("Выберите желаемое действие:");
            Console.WriteLine("(1). Просмотреть отчет по себе");
            Console.WriteLine("(2). Добавить часы работы");
            Console.WriteLine("(3). Выход из программы");
            return Console.ReadLine();
        }
        public ReportNote GetThisDayAddHoursForm(User user)
        {
            Console.Clear();
            Console.WriteLine("Введите количество отработанных часов:");
            string number = Console.ReadLine();
            Console.WriteLine("Введите комментарий");
            string comment = Console.ReadLine();
            if (comment == string.Empty) throw new ArgumentException("Комментарий не может быть пустым");
            return new ReportNote(user.Name, int.Parse(number), comment);
        }
        public ReportNote GetAddHoursForm(User user)
        {
            Console.Clear();
            Console.WriteLine("Введите количество отработанных часов:");
            string number = Console.ReadLine();
            Console.WriteLine("Введите дату в формате день.месяц.год:");
            string[] attr = Console.ReadLine().Split('.');
            DateTime date = new DateTime(int.Parse(attr[2]), int.Parse(attr[1]), int.Parse(attr[0]));
            if (date < DateTime.Now.AddDays(-2)) throw new ArgumentException("Нельзя добавить новую запись.\nПрошло уже два дня.");
            Console.WriteLine("Введите комментарий");
            string comment = Console.ReadLine();
            if (comment == string.Empty) throw new ArgumentException("Комментарий не может быть пустым");
            return new ReportNote(date, user.Name, int.Parse(number), comment);
        }

    }
}
