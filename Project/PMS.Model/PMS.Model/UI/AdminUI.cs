using PMS.Model.Models;
using PMS.Model.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.UI
{
    public class AdminUI
    {
        private readonly MedicineService medicineService;

        public AdminUI(MedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Admin Menu ===");
                Console.WriteLine("1. Show All Medicines");
                Console.WriteLine("2. Search Medicine");
                Console.WriteLine("3. Update Medicine");
                Console.WriteLine("4. Delete Medicine");
                Console.WriteLine("5. Stock Report");
                Console.WriteLine("6. Add New Medicine");
                Console.WriteLine("7. Low Stock Alert");
                Console.WriteLine("8. Expired Medicines");
                Console.WriteLine("9. Total Inventory Value");
                Console.WriteLine("10. Logout");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ShowAllUI();
                }
                else if (choice == "2")
                {
                    SearchUI();
                }
                else if (choice == "3")
                {
                    UpdateUI();
                }
                else if (choice == "4")
                {
                    DeleteUI();
                }
                else if (choice == "5")
                {
                    StockReportUI();
                }
                else if (choice == "6")
                {
                    AddUI();
                }
                else if (choice == "7")
                {
                    LowStockUI();
                }
                else if (choice == "8")
                {
                    ExpiredUI();
                }
                else if (choice == "9")
                {
                    InventoryValueUI();
                }
                else if (choice == "10")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong option!");
                    Console.ReadKey();
                }
            }
        }

        private void ShowAllUI()
        {
            Console.Clear();
            Console.WriteLine("=== All Medicines ===");
            ShowList(medicineService.GetAllMedicines());
            Console.ReadKey();
        }

        private void SearchUI()
        {
            Console.Clear();
            Console.Write("Enter medicine name: ");
            string name = Console.ReadLine();

            Medicine m = medicineService.FindMedicine(name);
            if (m == null)
            {
                Console.WriteLine("Medicine not found.");
            }
            else
            {
                Console.WriteLine(m.Details());
            }
            Console.ReadKey();
        }

        private void UpdateUI()
        {
            Console.Clear();
            Console.Write("Enter medicine name to update: ");
            string oldName = Console.ReadLine();

            Medicine existing = medicineService.FindMedicine(oldName);
            if (existing == null)
            {
                Console.WriteLine("Medicine not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Old record: " + existing.Details());
            Console.WriteLine("Enter new details:");
            Medicine newData = ReadMedicine();

            Console.WriteLine(medicineService.UpdateMedicine(oldName, newData));
            Console.ReadKey();
        }

        private void DeleteUI()
        {
            Console.Clear();
            Console.Write("Enter medicine name to delete: ");
            string name = Console.ReadLine();

            Console.WriteLine(medicineService.DeleteMedicine(name));
            Console.ReadKey();
        }

        private void StockReportUI()
        {
            Console.Clear();
            Console.WriteLine("=== Stock Report ===");
            Console.WriteLine("Total medicine types: " + medicineService.GetAllMedicines().Count);
            Console.WriteLine("Total units in stock: " + medicineService.GetTotalUnits());
            Console.WriteLine("Total stock value: Rs. " + medicineService.GetTotalInventoryValue());
            Console.ReadKey();
        }

        private void AddUI()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Medicine ===");
            Medicine m = ReadMedicine();

            Console.WriteLine(medicineService.AddMedicine(m));
            Console.ReadKey();
        }

        private void LowStockUI()
        {
            Console.Clear();
            Console.WriteLine("=== Low Stock (below 10 units) ===");
            ShowList(medicineService.GetLowStock(10));
            Console.ReadKey();
        }

        private void ExpiredUI()
        {
            Console.Clear();
            Console.WriteLine("=== Expired Medicines ===");
            DateTime now = DateTime.Now;
            ShowList(medicineService.GetExpired(now.Month, now.Year));
            Console.ReadKey();
        }

        private void InventoryValueUI()
        {
            Console.Clear();
            Console.WriteLine("=== Total Inventory Value ===");

            foreach (Medicine m in medicineService.GetAllMedicines())
            {
                Console.WriteLine(m.Name + ": " + m.Quantity + " x " + m.Price + " = " + (m.Quantity * m.Price));
            }

            Console.WriteLine("TOTAL: Rs. " + medicineService.GetTotalInventoryValue());
            Console.ReadKey();
        }
        private void ShowList(List<Medicine> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("No medicines found.");
                return;
            }

            foreach (Medicine m in list)
            {
                Console.WriteLine(m.Details());
            }
        }
        private Medicine ReadMedicine()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Company: ");
            string company = Console.ReadLine();
            Console.Write("Category: ");
            string category = Console.ReadLine();
            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());
            Console.Write("Expiry month (1-12): ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Expiry year: ");
            int year = int.Parse(Console.ReadLine());

            return new Medicine(name, company, category, quantity, price, month, year);
        }
    }
}
