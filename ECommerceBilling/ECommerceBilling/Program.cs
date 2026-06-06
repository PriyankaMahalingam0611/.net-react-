using System;
using ECommerceBilling.Models;
using ECommerceBilling.Services;

namespace ECommerceBilling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== E-Commerce Billing System ===");
            Console.WriteLine("---------------------------------");

            OrderBillingService billingService = new OrderBillingService();

            Order myOrder = new Order
            {
                ProductName = "Wireless Headphones",
                ProductPrice = 1200m,
                Quantity = 2
            };

            Console.WriteLine($"Processing Order: {myOrder.Quantity}x {myOrder.ProductName} @ {myOrder.ProductPrice:C} each");
            Console.WriteLine("---------------------------------\n");

            try
            {
                decimal subTotal = billingService.CalculateSubTotal(myOrder.ProductPrice, myOrder.Quantity);
                decimal discount = billingService.CalculateDiscount(subTotal);
                decimal amountAfterDiscount = subTotal - discount;
                decimal deliveryCharge = billingService.CalculateDeliveryCharge(amountAfterDiscount);
                decimal finalAmount = billingService.CalculateFinalAmount(myOrder.ProductPrice, myOrder.Quantity);

                Console.WriteLine("RECEIPT:");
                Console.WriteLine($"Subtotal:           {subTotal,10:C}");
                Console.WriteLine($"Discount Applied:  -{discount,10:C}");
                Console.WriteLine($"Delivery Charge:    {deliveryCharge,10:C}");
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"FINAL TOTAL:        {finalAmount,10:C}");
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error Processing Order: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}