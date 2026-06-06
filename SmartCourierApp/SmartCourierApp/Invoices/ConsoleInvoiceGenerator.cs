using System;
using SmartCourierApp.Models;

namespace SmartCourierApp.Invoices{
    public class ConsoleInvoiceGenerator : IInvoiceGenerator{
        public void GenerateInvoice(CourierBooking booking, double finalCharge){
            Console.WriteLine("\n==========================================");
            Console.WriteLine("             COURIER INVOICE              ");
            Console.WriteLine("==========================================");
            Console.WriteLine($"Customer Name    : {booking.Customer.Name}");
            Console.WriteLine($"Source City      : {booking.Parcel.SourceCity}");
            Console.WriteLine($"Destination City : {booking.Parcel.DestinationCity}");
            Console.WriteLine($"Parcel Weight    : {booking.Parcel.Weight} kg");
            Console.WriteLine($"Delivery Type    : {booking.DeliveryType}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Total Charge     : ${finalCharge:N2}");
            Console.WriteLine("==========================================\n");
        }
    }
}