using PMS.Model.Models;
using PMS.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Service
{
    public class MedicineService
    {
        private readonly MedicineRepository _repository;
        private List<Medicine> _medicines;

        public MedicineService()
        {
            _repository = new MedicineRepository();
            _medicines = _repository.GetAll();
        }

        public string AddMedicine(Medicine m)
        {
            string problem = Valid(m);
            if (problem != "")
            {
                return problem;
            }
            if (FindMedicine(m.Name) != null)
            {
                return "Medicine already exists.";
            }

            _medicines.Add(m);
            _repository.Save(m);
            return "Medicine added successfully!";
        }

        public Medicine FindMedicine(string name)
        {
            foreach (Medicine m in _medicines)
            {
                if (m.Name.ToLower() == name.ToLower())
                {
                    return m;
                }
            }
            return null;
        }

        public List<Medicine> GetAllMedicines()
        {
            return _medicines;
        }

        public string UpdateMedicine(string oldName, Medicine newData)
        {
            Medicine existing = FindMedicine(oldName);
            if (existing == null)
            {
                return "Medicine not found.";
            }

            string problem = Valid(newData);
            if (problem != "")
            {
                return problem;
            }

            Medicine other = FindMedicine(newData.Name);
            if (other != null && other != existing)
            {
                return "Another medicine already has that name.";
            }

            existing.Name = newData.Name;
            existing.Company = newData.Company;
            existing.Category = newData.Category;
            existing.Quantity = newData.Quantity;
            existing.Price = newData.Price;
            existing.ExpMonth = newData.ExpMonth;
            existing.ExpYear = newData.ExpYear;

            _repository.SaveAll(_medicines);
            return "Medicine updated successfully!";
        }

        public string DeleteMedicine(string name)
        {
            Medicine m = FindMedicine(name);
            if (m == null)
            {
                return "Medicine not found.";
            }

            _medicines.Remove(m);
            _repository.SaveAll(_medicines);
            return "Medicine deleted.";
        }

        public void ClearAllMedicines()
        {
            _medicines.Clear();
            _repository.ClearAll();
        }
        public List<Medicine> GetByCategory(string category)
        {
            List<Medicine> result = new List<Medicine>();
            foreach (Medicine m in _medicines)
            {
                if (m.Category.ToLower() == category.ToLower())
                {
                    result.Add(m);
                }
            }
            return result;
        }

        public List<Medicine> GetLowStock(int limit)
        {
            List<Medicine> result = new List<Medicine>();
            foreach (Medicine m in _medicines)
            {
                if (m.Quantity < limit)
                {
                    result.Add(m);
                }
            }
            return result;
        }

        public List<Medicine> GetExpired(int month, int year)
        {
            List<Medicine> result = new List<Medicine>();
            foreach (Medicine m in _medicines)
            {
                if (IsExpired(m, month, year))
                {
                    result.Add(m);
                }
            }
            return result;
        }

        public string AddStock(string name, int quantity)
        {
            Medicine m = FindMedicine(name);
            if (m == null)
            {
                return "Medicine not found.";
            }
            if (quantity <= 0)
            {
                return "Quantity must be greater than 0.";
            }

            m.Quantity = m.Quantity + quantity;
            _repository.SaveAll(_medicines);
            return "Stock added. New quantity: " + m.Quantity;
        }

        public string ReduceStock(string name, int quantity)
        {
            Medicine m = FindMedicine(name);
            if (m == null)
            {
                return "Medicine not found.";
            }
            if (quantity <= 0)
            {
                return "Quantity must be greater than 0.";
            }
            if (quantity > m.Quantity)
            {
                return "Cannot reduce more than available stock (" + m.Quantity + ").";
            }

            m.Quantity = m.Quantity - quantity;
            _repository.SaveAll(_medicines);
            return "Stock reduced. New quantity: " + m.Quantity;
        }

        public string ReplaceExpiredBatch(string name, int newQuantity, int newMonth, int newYear)
        {
            Medicine m = FindMedicine(name);
            if (m == null)
            {
                return "Medicine not found.";
            }
            if (newQuantity < 0)
            {
                return "Quantity cannot be negative.";
            }
            if (newMonth < 1 || newMonth > 12)
            {
                return "Expiry month must be between 1 and 12.";
            }

            m.Quantity = newQuantity;
            m.ExpMonth = newMonth;
            m.ExpYear = newYear;

            _repository.SaveAll(_medicines);
            return "Batch replaced successfully!";
        }

        public int GetTotalUnits()
        {
            int total = 0;
            foreach (Medicine m in _medicines)
            {
                total = total + m.Quantity;
            }
            return total;
        }

        public double GetTotalInventoryValue()
        {
            double total = 0;
            foreach (Medicine m in _medicines)
            {
                total = total + m.Quantity * m.Price;
            }
            return total;
        }
        private string Valid(Medicine m)
        {
            if (m.Name == "")
            {
                return "Name cannot be empty.";
            }
            if (m.Quantity < 0)
            {
                return "Quantity cannot be negative.";
            }
            if (m.Price <= 0)
            {
                return "Price must be greater than 0.";
            }
            if (m.ExpMonth < 1 || m.ExpMonth > 12)
            {
                return "Expiry month must be between 1 and 12.";
            }
            return "";
        }

        public bool IsExpired(Medicine m, int month, int year)
        {
            if (m.ExpYear < year)
            {
                return true;
            }
            if (m.ExpYear == year && m.ExpMonth < month)
            {
                return true;
            }
            return false;
        }
    }
}

