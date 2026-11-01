using PMS.Model.Models;
using PMS.Model.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.UI
{
    public class MainUI
    {
        private readonly UserService userService;
        private readonly AdminUI adminUI;
        private readonly StaffUI staffUI;

        public MainUI(UserService userService, AdminUI adminUI, StaffUI staffUI)
        {
            this.userService = userService;
            this.adminUI = adminUI;
            this.staffUI = staffUI;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Pharmacy Management System ===");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Staff");
                Console.WriteLine("3. Exit");
                Console.Write("Choose option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    if (Login("Admin"))
                    {
                        adminUI.ShowAdminMenu();
                    }
                }
                else if (choice == "2")
                {
                    if (Login("Staff"))
                    {
                        staffUI.ShowStaffMenu();
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Thank you for using the Pharmacy Management System!");
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong option!");
                    Console.ReadKey();
                }
            }
        }

        private bool Login(string role)
        {
            for (int i = 1; i <= 3; i++)
            {
                Console.Clear();
                Console.WriteLine(role + " Login - Attempt " + i + " of 3");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                User user = userService.Login(username, password, role);
                if (user != null)
                {
                    return true;
                }

                Console.WriteLine("Invalid username or password.");
                Console.ReadKey();
            }

            Console.WriteLine("Too many failed attempts.");
            Console.ReadKey();
            return false;
        }
    }
}
