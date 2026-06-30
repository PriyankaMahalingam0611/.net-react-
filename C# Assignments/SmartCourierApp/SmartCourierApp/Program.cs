using System;
using System.Collections.Generic;
using SmartCourierApp.Models;
using SmartCourierApp.DeliveryCalculators;
using SmartCourierApp.Notifications;
using SmartCourierApp.Invoices;
using SmartCourierApp.Services;

namespace SmartCourierApp{
    class Program{
        public static void Main(string[] args){
            Console.Title = "SmartCourier Delivery Management System";

            var calculators = new List<IDeliveryChargeCalculator>{
                new StandardDeliveryCalculator(),
                new ExpressDeliveryCalculator(),
                new InternationalDeliveryCalculator()
            };

            var notificationServices = new List<INotificationService>{
                new EmailNotificationService(),
                new SmsNotificationService(),
                new WhatsAppNotificationService()
            };

            IInvoiceGenerator invoiceGenerator = new ConsoleInvoiceGenerator();

            var bookingService = new CourierBookingService(calculators, notificationServices, invoiceGenerator);

            Console.WriteLine("--- SmartCourier Booking Console --- \n");

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email Address: ");
            string email = Console.ReadLine();

            Console.Write("Enter Mobile Number: ");
            string mobile = Console.ReadLine();

            Console.Write("Enter Parcel Weight (in kg): ");
            if (!double.TryParse(Console.ReadLine(), out double weight)){
                Console.WriteLine("Invalid weight entered. Defaulting to 1.0 kg.");
                weight = 1.0;
            }

            Console.Write("Enter Source City: ");
            string source = Console.ReadLine();

            Console.Write("Enter Destination City: ");
            string destination = Console.ReadLine();

            Console.WriteLine("\nSelect Delivery Type:");
            Console.WriteLine("1. Standard");
            Console.WriteLine("2. Express");
            Console.WriteLine("3. International");
            Console.Write("Choice (1-3): ");
            string deliveryChoice = Console.ReadLine();
            string deliveryType = deliveryChoice switch{
                "1" => "Standard",
                "2" => "Express",
                "3" => "International",
                _ => "Standard"
            };

            Console.WriteLine("\nSelect Notification Channel:");
            Console.WriteLine("1. Email");
            Console.WriteLine("2. SMS");
            Console.WriteLine("3. WhatsApp");
            Console.Write("Choice (1-3): ");
            string notificationChoice = Console.ReadLine();
            string notificationType = notificationChoice switch{
                "1" => "Email",
                "2" => "SMS",
                "3" => "WhatsApp",
                _ => "Email"
            };

            Customer customer = new Customer(name, email, mobile);
            Parcel parcel = new Parcel(weight, source, destination);
            CourierBooking ongoingBooking = new CourierBooking(customer, parcel, deliveryType, notificationType);

            try{
                bookingService.ProcessBooking(ongoingBooking);
            }
            catch (Exception ex) { 
                Console.WriteLine($"\nAn execution error occurred processing booking: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}