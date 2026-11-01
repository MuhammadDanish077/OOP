using System;
using PMS.Model.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Model.Models;

namespace PMS.Model.Service
{
    public class BillingService
    {
        private readonly BillRepository _billRepository;
        private readonly MedicineService medicineService;
        public BillingService(MedicineService medicineService)
        {
            _billRepository = new BillRepository();
            this.medicineService = medicineService;
        }
        public Bill StartBill(string customerName, string phone)
        {
            if (customerName == "")
            {
                customerName = "Walk-in";
            }

            Bill bill = new Bill(new Customer(customerName, phone));
            bill.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            return bill;
        }

        public string AddItem(Bill bill, string medicineName, int quantity)
        {
            Medicine m = medicineService.FindMedicine(medicineName);
            if (m == null)
            {
                return "Medicine not found.";
            }
            if (quantity <= 0)
            {
                return "Quantity must be greater than 0.";

            }
            DateTime now = DateTime.Now;
            if (medicineService.IsExpired(m, now.Month, now.Year))
            {
                return "Medicine Expired.Cant sell";
            }
            int alreadyInBill = QuantityInBill(bill, m.Name);
            if (alreadyInBill + quantity > m.Quantity)
            {
                return "Insufficient stock! Available: " + m.Quantity + ", already in this bill: " + alreadyInBill;
            }

            bill.Items.Add(new BillItem(m, quantity));
            return "Item added.";
        }
        public double CalculateTotal(Bill bill)
        {
            double total = 0;
            foreach (BillItem item in bill.Items)
            {
                total = total + item.Subtotal;
            }
            return total;
        }
        public string FinishBill(Bill bill)
        {
            if (bill.Items.Count == 0)
            {
                return "Bill empty. Nothing to save";
            }
            foreach (BillItem item in bill.Items)
            {
                medicineService.ReduceStock(item.medicine.Name, item.Quantity);
            }
            bill.GrandTotal = CalculateTotal(bill);
            _billRepository.Save(bill);
            return "Bill completed. Grand total: " + bill.GrandTotal;
        }
        private int QuantityInBill(Bill bill, string medicineName)
        {
            int total = 0;
            foreach (BillItem item in bill.Items)
            {
                if (item.medicine.Name == medicineName)
                {
                    total = total + item.Quantity;
                }
            }
            return total;
        }





    }
}
