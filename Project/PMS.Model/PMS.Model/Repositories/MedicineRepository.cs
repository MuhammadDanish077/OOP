using PMS.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Repositories
{
    public class MedicineRepository
    {
        private readonly string _filePath = "medicines.txt";

        public void Save(Medicine medicine)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, true))
            {
                writer.WriteLine(medicine.ToString());
            }
        }

        public void SaveAll(List<Medicine> medicines)
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                foreach (Medicine m in medicines)
                {
                    writer.WriteLine(m.ToString());
                }
            }
        }

        public void ClearAll()
        {
            File.WriteAllText(_filePath, string.Empty);
        }

        public List<Medicine> GetAll()
        {
            List<Medicine> medicines = new List<Medicine>();

            if (!File.Exists(_filePath))
            {
                return medicines;
            }

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != "")
                    {
                        string[] parts = line.Split('|');

                        Medicine m = new Medicine(
                            parts[0],
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            double.Parse(parts[4]),
                            int.Parse(parts[5]),
                            int.Parse(parts[6]));

                        medicines.Add(m);
                    }
                }
            }
            return medicines;
        }
    }

}
