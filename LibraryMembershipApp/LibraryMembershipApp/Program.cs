using System;
using LibraryMembershipApp.Interfaces;
using LibraryMembershipApp.Services;

namespace LibraryMembershipApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Library Membership App!");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("This application is structured to demonstrate NUnit and Moq testing.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}