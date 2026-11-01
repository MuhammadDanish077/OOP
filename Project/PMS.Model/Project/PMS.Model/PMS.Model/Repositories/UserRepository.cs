using System;
using System.Collections.Generic;
using System.IO;
using PMS.Model.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Repositories
{
    public class UserRepository
    {
        private readonly string _filePath="users.txt";
        public void SaveAll(List<User> users)
        {
            using (StreamWriter writer= new StreamWriter(_filePath, false))
            {
                foreach(User u in users)
                {
                    writer.WriteLine(u.ToString());
                }
            }
        }
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            if (!File.Exists(_filePath))
            {
                return users;
            }
            using(StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while((line=reader.ReadLine()) != null)
                {
                    if(line!= "")
                    {
                        string[] parts = line.Split('|');
                        users.Add(new User(parts[0], parts[1], parts[2]));
                           
                    }
                }
            }
            return users;
        }
    }
}
