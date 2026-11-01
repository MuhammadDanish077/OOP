using PMS.Model.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Repositories
{
    public class BillRepository
    {
        private readonly string _filePath = "bills.txt";
        public void Save(Bill bill)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                foreach (BillItem item in bill.Items)
                {
                    writer.WriteLine($"{bill.Date}|{bill.customer.Name}|{bill.customer.Phone}|{item.medicine.Name}|{item.Quantity}|{item.medicine.Price}|{item.Subtotal}");

                }
            }
        }
        public List<string> GetAllLines()
        {
            List<string> lines = new List<string>();
            if (!File.Exists(_filePath))
            {
                return lines;
            }
            using(StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while((line=reader.ReadLine())!= null)
                {
                    if(line!= "")
                    {
                        lines.Add(line);

                    }
                }
            }
            return lines;
        }

    }
}
