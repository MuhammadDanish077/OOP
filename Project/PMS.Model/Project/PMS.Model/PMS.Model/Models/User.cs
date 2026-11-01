using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{
    public class User
    {
        public string Username;
        public string Password;
        public string Role;
        public User() { }
        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
        public override string ToString()
        {
            return $"{Username}|{Password}|{Role}";
        }

        public string Details()
        {
            return $"Username: {Username}| Role: {Role}";
        }
    }
}
