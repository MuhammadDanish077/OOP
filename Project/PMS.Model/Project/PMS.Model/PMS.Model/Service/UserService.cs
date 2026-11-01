using PMS.Model.Repositories;
using PMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Service
{
    public class UserService
    {
        private readonly UserRepository _repository;
            private List<User> users;
        public UserService()
        {
            _repository = new UserRepository();
            users = _repository.GetAll();

            if (users.Count == 0)
            {
                CreateDefaultUsers();
            }
        }
        public User Login(string username,string password,string role)
        {
            foreach(User u in users)
            {
                if(u.Username == username && u.Password == password && u.Role == role)
                {
                    return u;
                }
            }
            return null;
        }
        public string AddUser(User newUser)
        {
            if (newUser.Username == "")
            {
                return "Username cannot be empty.";
            }
            if (newUser.Password.Length < 3)
            {
                return "Password must be at least 3 characters.";
            }
            if (newUser.Role != "Admin" && newUser.Role != "Staff")
            {
                return "Role must be Admin or Staff.";
            }
            if (FindUser(newUser.Username) != null)
            {
                return "Username already exists.";
            }

            users.Add(newUser);
            _repository.SaveAll(users);
            return "User added successfully!";
        }
        public User FindUser(string username)
        {
            foreach (User u in users)
            {
                if (u.Username == username)
                {
                    return u;
                }
            }
            return null;
        }
        public string ChangePassword(string username, string oldPassword,string newPassword)
        {
            User u = FindUser(username);
            if(u == null)
            {
                return "User not found.";
            }
            if (u.Password != oldPassword)
            {
                return "Old password is wwrong.";

            }
            if (newPassword.Length < 3)
            {
                return "Password must be at least 3 characters.";

            }
            u.Password = newPassword;
            _repository.SaveAll(users);
            return "PAsswor changed successfully";
        }
        private void CreateDefaultUsers()
        {
            users.Add(new User("CEO", "123", "Admin"));
            users.Add(new User("DOC", "112", "Staff"));
            _repository.SaveAll(users);
        }

    }
}
