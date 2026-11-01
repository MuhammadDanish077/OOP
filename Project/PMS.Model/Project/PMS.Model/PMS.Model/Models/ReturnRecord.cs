using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Models
{
    public class ReturnRecord
    {
        public Customer customer;
        public Medicine Medicine;
        public int Quantity;
        public string Reason;
        public double RefundAmount;

        public ReturnRecord() { }

        public ReturnRecord(Customer customer, Medicine medicine, int quantity, string reason)
        {
            this.customer = customer;
            Medicine = medicine;
            Quantity = quantity;
            Reason = reason;
            RefundAmount = quantity * medicine.Price;
        }
        public override string ToString()
        {
            return $"Customer: {customer}| Medicine: {Medicine}| Quantity: {Quantity}| Reason: {Reason}| Refund Amount: {RefundAmount}";
        }
    }
}
