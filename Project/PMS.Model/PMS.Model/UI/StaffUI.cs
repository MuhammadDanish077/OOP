using PMS.Model.Models;
using PMS.Model.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.UI
{
    public class StaffUI
    {
        private readonly MedicineService medicineService;
        private readonly BillingService billingService;
        private readonly ReturnService returnService;
        private readonly ReservationService reservationService;

        public StaffUI(MedicineService medicineService, BillingService billingService,
                       ReturnService returnService, ReservationService reservationService)
        {
            this.medicineService = medicineService;
            this.billingService = billingService;
            this.returnService = returnService;
            this.reservationService = reservationService;
        }

        public void ShowStaffMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Staff Menu ===");
                Console.WriteLine("1. Generate Customer Bill");
                Console.WriteLine("2. Return Medicine");
                Console.WriteLine("3. Check Medicine Availability");
                Console.WriteLine("4. Search by Category");
                Console.WriteLine("5. Quick Add or Reduce Stock");
                Console.WriteLine("6. Reserve Medicine");
                Console.WriteLine("7. Check Medicine Price");
                Console.WriteLine("8. Replace Expired Medicine");
                Console.WriteLine("9. Pick Up or Cancel Reservation");
                Console.WriteLine("10. Logout");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    BillUI();
                }
                else if (choice == "2")
                {
                    ReturnUI();
                }
                else if (choice == "3")
                {
                    AvailabilityUI();
                }
                else if (choice == "4")
                {
                    CategoryUI();
                }
                else if (choice == "5")
                {
                    StockUI();
                }
                else if (choice == "6")
                {
                    ReserveUI();
                }
                else if (choice == "7")
                {
                    PriceUI();
                }
                else if (choice == "8")
                {
                    ReplaceExpiredUI();
                }
                else if (choice == "9")
                {
                    ReservationsUI();
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

        private void BillUI()
        {
            Console.Clear();
            Console.WriteLine("=== Customer Bill ===");
            Console.Write("Customer name (press Enter to skip): ");
            string customerName = Console.ReadLine();
            Console.Write("Customer phone (press Enter to skip): ");
            string phone = Console.ReadLine();

            Bill bill = billingService.StartBill(customerName, phone);

            string addMore = "y";
            while (addMore == "y")
            {
                Console.Write("Enter medicine name: ");
                string medicineName = Console.ReadLine();
                Console.Write("Enter quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.WriteLine(billingService.AddItem(bill, medicineName, quantity));

                Console.Write("Add more medicine? (y/n): ");
                addMore = Console.ReadLine().ToLower();
            }

            if (bill.Items.Count == 0)
            {
                Console.WriteLine("No items added. Bill cancelled.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n=== Receipt ===");
            Console.WriteLine("Customer: " + bill.customer.Name);
            foreach (BillItem item in bill.Items)
            {
                Console.WriteLine(item.medicine.Name + " x " + item.Quantity + " = " + item.Subtotal);
            }
            Console.WriteLine("TOTAL: Rs. " + billingService.CalculateTotal(bill));

            Console.Write("\nFinish this bill? (y/n): ");
            string finish = Console.ReadLine().ToLower();
            if (finish == "y")
            {
                Console.WriteLine(billingService.FinishBill(bill));
            }
            else
            {
                Console.WriteLine("Bill cancelled. Stock was not changed.");
            }
            Console.ReadKey();
        }

        private void ReturnUI()
        {
            Console.Clear();
            Console.WriteLine("=== Return Medicine ===");
            Console.Write("Customer name: ");
            string customerName = Console.ReadLine();
            Console.Write("Customer phone (press Enter to skip): ");
            string phone = Console.ReadLine();
            Console.Write("Medicine name to return: ");
            string medicineName = Console.ReadLine();
            Console.Write("Quantity to return: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Reason for return: ");
            string reason = Console.ReadLine();

            string problem = returnService.CheckReturn(medicineName, quantity);
            if (problem != "")
            {
                Console.WriteLine(problem);
                Console.ReadKey();
                return;
            }

            Customer customer = new Customer(customerName, phone);
            ReturnRecord record = returnService.ProcessReturn(customer, medicineName, quantity, reason);
            Medicine m = medicineService.FindMedicine(medicineName);

            Console.WriteLine("\n--- Return Processed ---");
            Console.WriteLine("Customer : " + record.customer.Name);
            Console.WriteLine("Medicine : " + record.Medicine.Name);
            Console.WriteLine("Quantity : " + record.Quantity);
            Console.WriteLine("Refund   : Rs. " + record.RefundAmount);
            Console.WriteLine("Reason   : " + record.Reason);
            Console.WriteLine("New stock: " + m.Quantity);
            Console.ReadKey();
        }

        private void AvailabilityUI()
        {
            Console.Clear();
            Console.WriteLine("=== Check Availability ===");
            Console.Write("Enter medicine name: ");
            string name = Console.ReadLine();

            Medicine m = medicineService.FindMedicine(name);
            if (m == null)
            {
                Console.WriteLine("Medicine " + name + " is NOT available.");
            }
            else
            {
                Console.WriteLine("Medicine AVAILABLE");
                Console.WriteLine(m.Details());
                if (m.Quantity < 10)
                {
                    Console.WriteLine("WARNING: Low stock!");
                }
            }
            Console.ReadKey();
        }

        private void CategoryUI()
        {
            Console.Clear();
            Console.WriteLine("=== Search by Category ===");
            Console.Write("Enter category : ");
            string category = Console.ReadLine();

            List<Medicine> list = medicineService.GetByCategory(category);
            if (list.Count == 0)
            {
                Console.WriteLine("No medicines found in this category.");
            }
            else
            {
                foreach (Medicine m in list)
                {
                    Console.WriteLine(m.Details());
                }
            }
            Console.ReadKey();
        }

        private void StockUI()
        {
            Console.Clear();
            Console.WriteLine("=== Quick Add or Reduce Stock ===");
            Console.Write("Enter medicine name: ");
            string name = Console.ReadLine();

            Medicine m = medicineService.FindMedicine(name);
            if (m == null)
            {
                Console.WriteLine("Medicine not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Current stock: " + m.Quantity + " units");
            Console.WriteLine("1. Add stock");
            Console.WriteLine("2. Reduce stock");
            Console.Write("Choose option: ");
            string choice = Console.ReadLine();

            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Invalid option!");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            if (choice == "1")
            {
                Console.WriteLine(medicineService.AddStock(m.Name, quantity));
            }
            else
            {
                Console.WriteLine(medicineService.ReduceStock(m.Name, quantity));
            }
            Console.ReadKey();
        }

        private void ReserveUI()
        {
            Console.Clear();
            Console.WriteLine("=== Reserve Medicine ===");
            Console.Write("Customer name: ");
            string customerName = Console.ReadLine();
            Console.Write("Customer phone: ");
            string phone = Console.ReadLine();
            Console.Write("Medicine name to reserve: ");
            string medicineName = Console.ReadLine();
            Console.Write("Quantity to reserve: ");
            int quantity = int.Parse(Console.ReadLine());

            Customer customer = new Customer(customerName, phone);

            string problem = reservationService.CheckReservation(customer, medicineName, quantity);
            if (problem != "")
            {
                Console.WriteLine(problem);
                Console.ReadKey();
                return;
            }

            Reservation r = reservationService.Reserve(customer, medicineName, quantity);
            Console.WriteLine("\n--- RESERVATION CONFIRMED ---");
            Console.WriteLine(r.Details());
            Console.WriteLine("Status: Reserved (pickup within 24 hours)");
            Console.ReadKey();
        }

        private void PriceUI()
        {
            Console.Clear();
            Console.WriteLine("=== Check Price ===");
            Console.Write("Enter medicine name: ");
            string name = Console.ReadLine();

            Medicine m = medicineService.FindMedicine(name);
            if (m == null)
            {
                Console.WriteLine("Medicine not found.");
            }
            else
            {
                Console.WriteLine("Medicine: " + m.Name);
                Console.WriteLine("Company : " + m.Company);
                Console.WriteLine("Price   : Rs. " + m.Price + " per unit");
                Console.WriteLine("Stock   : " + m.Quantity + " units");
            }
            Console.ReadKey();
        }

        private void ReplaceExpiredUI()
        {
            Console.Clear();
            Console.WriteLine("=== Replace Expired Medicine ===");
            Console.Write("Enter medicine name: ");
            string name = Console.ReadLine();

            Medicine m = medicineService.FindMedicine(name);
            if (m == null)
            {
                Console.WriteLine("Medicine not found.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Old quantity: " + m.Quantity);
            Console.WriteLine("Old expiry  : " + m.ExpMonth + "/" + m.ExpYear);

            Console.Write("New batch quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("New expiry month (1-12): ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("New expiry year: ");
            int year = int.Parse(Console.ReadLine());

            Console.WriteLine(medicineService.ReplaceExpiredBatch(m.Name, quantity, month, year));
            Console.ReadKey();
        }

        private void ReservationsUI()
        {
            Console.Clear();
            Console.WriteLine("=== Reservations ===");

            List<Reservation> list = reservationService.GetAllReservations();
            if (list.Count == 0)
            {
                Console.WriteLine("No reservations right now.");
                Console.ReadKey();
                return;
            }

            foreach (Reservation r in list)
            {
                Console.WriteLine(r.Details());
            }

            Console.Write("\nCustomer phone: ");
            string phone = Console.ReadLine();
            Console.Write("Medicine name: ");
            string medicineName = Console.ReadLine();
            Console.Write("1 = Customer here for pick up,\n 2 = Cancel reservation: ");
            string action = Console.ReadLine();

            if (action == "1")
            {
                Console.WriteLine(reservationService.CompletePickup(phone, medicineName));
            }
            else if (action == "2")
            {
                Console.WriteLine(reservationService.CancelReservation(phone, medicineName));
            }
            else
            {
                Console.WriteLine("Invalid option!");
            }
            Console.ReadKey();
        }
    }
}
