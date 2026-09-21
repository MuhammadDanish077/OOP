using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace C__TASKS
{
    internal class Program
    {
        //TASK 1:
        static int add(int a, int b)
        {
            return a + b;
        }


        static void Main(string[] args)
        {

            // TASK 1:

            int a;
            int b;
            Console.Write("Enter 1st Number : ");
            a = int.Parse(Console.ReadLine());
            Console.Write("Enter 2nd Number : ");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine("Total is : " + add(a, b));


        }
    }
}