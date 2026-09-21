using System;
using System.IO;

namespace C__TASKS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "data.txt";

            StreamWriter writer = new StreamWriter(file, true);

            writer.WriteLine("hello");
            writer.Flush();
            writer.Close();

            Console.WriteLine("Data appended successfully!");

            Console.ReadKey();
        }
    }
}