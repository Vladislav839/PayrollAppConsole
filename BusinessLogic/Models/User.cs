using System;

namespace BusinessLogic.Models
{
    public class User
    {
        public User(string name, string role)
        {
            Name = name;
            Role = role;
        }
        public string Name { get; }

        public string Role { get; }

        public override string ToString()
        {
            return $"{Name},{Role}";
        }
    }
}
