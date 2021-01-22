using BusinessLogic.Models;
using Data;
using PayrollApp.Controllers;
using PayrollApp.Views;
using System;

namespace PayrollApp
{
    class Program
    {
        static void Start(string msg)
        {
            Console.Clear();
            if (msg != null) Console.WriteLine(msg + '\n');
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            RepositoryService repositoryService = new RepositoryService();
            User user = repositoryService.GetUserByName(name);
            if (user != null)
            {
                switch (user.Role)
                {
                    case "руководитель":
                        DirectorsController directorsController = new DirectorsController(new DirectorsView(user), repositoryService);
                        directorsController.Run(null);
                        break;
                    case "сотрудник":
                        EmployeesController employeesController = new EmployeesController(new EmployeesView(user), repositoryService, user);
                        employeesController.Run(null);
                        break;
                    case "фрилансер":
                        FreelancersController freelancersController = new FreelancersController(new FreelancersView(user), repositoryService, user);
                        freelancersController.Run(null);
                        break;
                }
            }
            else
            {
                Start("Пользователя с таким именем не существует");
            }
        }
        static void Main(string[] args)
        {
            Start(null);
        }
    }
}
