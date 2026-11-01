using PMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Service
{
    public class ReturnService
    {
        private readonly MedicineService medicineService;
        public ReturnService(MedicineService med)
        {
            medicineService = med;
        }
        public string CheckReturn(string medicineName, int quantity)
        {
            if(medicineService.FindMedicine(medicineName) == null)
            {
                return "Medicine not found.";
            }
            if (quantity <= 0)
            {
                return "Quantity must be greater than 0.";
            }
            return "";
        }
        public ReturnRecord ProcessReturn(Customer customer, string medicineName, int quantity, string reason)
        {
            if (CheckReturn(medicineName, quantity) != "")
            {
                return null;
            }

            Medicine m = medicineService.FindMedicine(medicineName);

            if (reason == "")
            {
                reason = "No Reasson";
            }

            medicineService.AddStock(m.Name, quantity);
            return new ReturnRecord(customer, m, quantity, reason);
        }
    }
}
