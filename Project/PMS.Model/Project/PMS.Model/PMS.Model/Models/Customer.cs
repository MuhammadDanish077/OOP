using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{
    public class Customer
    {
        
            public string Name;
            public string Phone;

            public Customer() { }
            public Customer(string name, string phone)
            {
                Name = name;
                Phone = phone;

            }
            public override string ToString()
            {
            return $"{Name}|{Phone}";
             }
            public string Details()
            {
                return $"Name: {Name}| Phone: {Phone}";
            }
        }
    }

