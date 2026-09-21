using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIZZA
{
    internal class Program
    {
        static void PizzaPoints(int minOrders, int minPrice)
        {
            string file = "Customers.txt";

            if (!File.Exists(file))
            {
                Console.WriteLine("Customers.txt not found!");
                return;
            }

            StreamReader reader = new StreamReader(file);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');
                string name = parts[0];

                string prices = parts[2].Replace("(", "").Replace(")", "");
                string[] numbers = prices.Split(',');

                int count = 0;

                for (int i = 0; i < numbers.Length; i++)
                {
                    int price = int.Parse(numbers[i]);

                    if (price >= minPrice)
                    {
                        count++;
                    }
                }

                if (count >= minOrders)
                {
                    Console.WriteLine(name);
                }
            }

            reader.Close();
        }

        static void Main(string[] args)
        {
            Console.Write("Enter minimum orders: ");
            int orders = int.Parse(Console.ReadLine());

            Console.Write("Enter minimum price: ");
            int price = int.Parse(Console.ReadLine());

            PizzaPoints(orders, price);

            Console.ReadKey();
        }
    }
}