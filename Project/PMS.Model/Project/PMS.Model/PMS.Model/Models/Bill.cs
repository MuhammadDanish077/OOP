using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{
    public class Bill
    {
        public Customer customer;
        public string Date;
        public List<BillItem> Items = new List<BillItem>();
        public double GrandTotal;
        public Bill() { }
        public Bill(Customer customer)
        {
            this.customer = customer;
            
            GrandTotal = 0;
        }
    }
}
