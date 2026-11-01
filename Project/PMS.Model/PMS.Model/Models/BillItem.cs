using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{
    public class BillItem
    {
        public Medicine medicine;
        public int Quantity;
        public double Subtotal;
        public BillItem() { }
        public BillItem(Medicine medicine, int quantity)
        {
            this.medicine = medicine;
            Quantity = quantity;
            Subtotal = quantity * medicine.Price;
        }
        public override string ToString()
        {
            return $"Medicine: {medicine}| Quantity: {Quantity}| Subtotal: {Subtotal}";
        }
    }
}
