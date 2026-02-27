using System;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose one of the options");
            Console.WriteLine("1. Teacher");
            Console.WriteLine("2. Student");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("Teacher Menu");
                // You can add teacher-specific functionality here
            }
            else if (choice == "2")
            {
                Console.WriteLine("Student menu");
                // You can add student-specific functionality here
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose either 1 or 2.");
            }

        }
    }
}