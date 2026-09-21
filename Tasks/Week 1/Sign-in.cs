using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C__TASKS
{
    internal class Program
    {
        static string file = "Users.txt";

        static void SignUp()
        {
            Console.Write("Enter new username: ");
            string name = Console.ReadLine();

            Console.Write("Enter new password: ");
            string pass = Console.ReadLine();

            StreamWriter writer = new StreamWriter(file, true);

            writer.WriteLine(name + "," + pass);
            writer.Flush();
            writer.Close();

            Console.WriteLine("Sign Up successful!");
        }

        static void SignIn()
        {
            Console.Write("Enter username: ");
            string name = Console.ReadLine();

            Console.Write("Enter password: ");
            string pass = Console.ReadLine();

            bool check = false;

            if (File.Exists(file))
            {
                StreamReader reader = new StreamReader(file);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');

                    if (data[0] == name && data[1] == pass)
                    {
                        check = true;
                    }
                }

                reader.Close();
            }

            if (check)
            {
                Console.WriteLine("Sign In successful! Welcome " + name);
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        static void Main(string[] args)
        {
            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\n1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("0. Exit");

                Console.Write("Enter choice: ");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    SignIn();
                }
                else if (choice == 2)
                {
                    SignUp();
                }
                else if (choice != 0)
                {
                    Console.WriteLine("Invalid choice, try again.");
                }
            }

            Console.ReadKey();
        }
    }
}