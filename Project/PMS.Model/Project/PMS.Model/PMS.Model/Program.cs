using PMS.Model.Models;
using PMS.Model.Service;
using PMS.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    internal class Program
    {


        static void Main(string[] args)
        {
            MedicineService medicineService = new MedicineService();
            UserService userService = new UserService();
            BillingService billingService = new BillingService(medicineService);
            ReturnService returnService = new ReturnService(medicineService);
            ReservationService reservationService = new ReservationService(medicineService);

            AdminUI adminUI = new AdminUI(medicineService);
            StaffUI staffUI = new StaffUI(medicineService, billingService, returnService, reservationService);
            MainUI mainUI = new MainUI(userService, adminUI, staffUI);

            mainUI.ShowMainMenu();
        }
    }
}

