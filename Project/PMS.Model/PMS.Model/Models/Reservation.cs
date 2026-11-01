using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{
    public class Reservation
    {
        public Customer customer;
        public Medicine medicine;
        public int Quantity;
        public double TotalPrice;
        public Reservation() { }
        public Reservation(Customer customer, Medicine medicine, int quantity)
        {
            this.customer = customer;
            this.medicine = medicine;
            Quantity = quantity;
            TotalPrice = quantity * medicine.Price;
        }
        public override string ToString()
        {
            return $"{customer.Name}|{customer.Phone}|{medicine.Name}|{Quantity}|{medicine.Price}";

        }
        public string Details()
        {
            return $"Customer: {customer.Details()}| Medicine: {medicine.Name}| Quantity: {Quantity}| Total Price: {TotalPrice}";
        }
    }
}
