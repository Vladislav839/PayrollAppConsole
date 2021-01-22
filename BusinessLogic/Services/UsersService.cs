using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Services
{
    class UsersService
    {
        public static User GetUserByName(List<User> users, string name)
        {
            return users.FirstOrDefault(u => u.Name == name);
        }
    }
}
