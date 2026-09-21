using System;
using System.IO;

class Program
{
    static void Main()
    {
        string fileName = "data.txt";

        if (File.Exists(fileName))
        {
            StreamReader fileR = new StreamReader(fileName);

            string line;

            while ((line = fileR.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

            fileR.Close();
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }
}