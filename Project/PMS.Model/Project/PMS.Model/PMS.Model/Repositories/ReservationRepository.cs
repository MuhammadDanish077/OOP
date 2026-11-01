using PMS.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Repositories
{
    public class ReservationRepository
    {
        private readonly string _filePath = "reservations.txt";

        public void Save(Reservation reservation)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine(reservation.ToString());
            }
        }

        public void SaveAll(List<Reservation> reservations)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                foreach (Reservation r in reservations)
                {
                    writer.WriteLine(r.ToString());
                }
            }
        }

        public List<Reservation> GetAll()
        {
            List<Reservation> reservations = new List<Reservation>();

            if (!File.Exists(_filePath))
            {
                return reservations;
            }

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] parts = line.Split('|');

                        Customer customer = new Customer(parts[0], parts[1]);
                        Medicine medicine = new Medicine(parts[2], "", "", 0, double.Parse(parts[4]), 0, 0);

                        reservations.Add(new Reservation(customer, medicine, int.Parse(parts[3])));
                    }
                }
            }
            return reservations;
        }

    }
}
