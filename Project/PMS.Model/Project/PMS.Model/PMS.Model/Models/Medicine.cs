using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{

    public class Medicine
            {
                public string Name;
                public string Company;
                public string Category;
                public int Quantity;
                public double Price;
                public int ExpMonth;
                public int ExpYear;

                public Medicine() { }
                public Medicine(string name, string company, string category, int quantity, double price, int expMonth, int expYear)
                {
                    Name = name;
                    Company = company;
                    Category = category;
                    Quantity = quantity;
                    Price = price;
                    ExpMonth = expMonth;
                    ExpYear = expYear;
                }

                public override string ToString()
                {
                  return $"{Name}|{Company}|{Category}|{Quantity}|{Price}|{ExpMonth}|{ExpYear}";
                }
        public string Details()
        {
            return $"Name: {Name}| Company: {Company}| Category: {Category}| Quantity: {Quantity}| Price: {Price}| Expiration: {ExpMonth}/{ExpYear}";
        }
    }
}
    

