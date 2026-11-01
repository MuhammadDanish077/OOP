using PMS.Model.Models;
using PMS.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Service
{
    public class ReservationService
    {
        private readonly MedicineService medicineService;
        private readonly ReservationRepository _repository;
        private List<Reservation> reservations;

        public ReservationService(MedicineService medicineService)
        {
            this.medicineService = medicineService;
            _repository = new ReservationRepository();
            reservations = _repository.GetAll();
        }

        public string CheckReservation(Customer customer, string medicineName, int quantity)
        {
            if (customer.Phone == "")
            {
                return "Phone number is required for a reservation.";
            }

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
                return "Cannot reserve: this medicine is expired.";
            }
            if (FindReservation(customer.Phone, m.Name) != null)
            {
                return "This customer already has a reservation for this medicine.";
            }
            if (quantity > m.Quantity)
            {
                return "Only " + m.Quantity + " units available.";
            }
            return "";
        }

        public Reservation Reserve(Customer customer, string medicineName, int quantity)
        {
            if (CheckReservation(customer, medicineName, quantity) != "")
            {
                return null;
            }

            Medicine m = medicineService.FindMedicine(medicineName);
            medicineService.ReduceStock(m.Name, quantity);

            Reservation r = new Reservation(customer, m, quantity);
            reservations.Add(r);
            _repository.Save(r);
            return r;
        }

        public List<Reservation> GetAllReservations()
        {
            return reservations;
        }

        public Reservation FindReservation(string phone, string medicineName)
        {
            foreach (Reservation r in reservations)
            {
                if (r.customer.Phone == phone && r.medicine.Name.ToLower() == medicineName.ToLower())
                {
                    return r;
                }
            }
            return null;
        }
        public string CancelReservation(string phone, string medicineName)
        {
            Reservation r = FindReservation(phone, medicineName);
            if (r == null)
            {
                return "Reservation not found.";
            }

            medicineService.AddStock(r.medicine.Name, r.Quantity);
            reservations.Remove(r);
            _repository.SaveAll(reservations);
            return "Reservation cancelled. Stock returned.";
        }
        public string CompletePickup(string phone, string medicineName)
        {
            Reservation r = FindReservation(phone, medicineName);
            if (r == null)
            {
                return "Reservation not found.";
            }

            reservations.Remove(r);
            _repository.SaveAll(reservations);
            return "Pickup completed. Amount to collect: " + r.TotalPrice;
        }
    }
}
