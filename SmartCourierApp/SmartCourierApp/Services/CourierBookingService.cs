using System;
using System.Collections.Generic;
using System.Linq;
using SmartCourierApp.Models;
using SmartCourierApp.DeliveryCalculators;
using SmartCourierApp.Notifications;
using SmartCourierApp.Invoices;

namespace SmartCourierApp.Services{
    public class CourierBookingService{
        private readonly IEnumerable<IDeliveryChargeCalculator> _calculators;
        private readonly IEnumerable<INotificationService> _notificationServices;
        private readonly IInvoiceGenerator _invoiceGenerator;

        public CourierBookingService(IEnumerable<IDeliveryChargeCalculator> calculators, IEnumerable<INotificationService> notificationServices, IInvoiceGenerator invoiceGenerator){
            _calculators = calculators;
            _notificationServices = notificationServices;
            _invoiceGenerator = invoiceGenerator;
        }

        public void ProcessBooking(CourierBooking booking){
            // 1. Locate the correct calculator strategy
            var calculator = _calculators.FirstOrDefault(c => c.DeliveryType.Equals(booking.DeliveryType, StringComparison.OrdinalIgnoreCase));

            if (calculator == null){
                throw new NotSupportedException($"Delivery type '{booking.DeliveryType}' is not supported.");
            }

            double totalCharge = calculator.CalculateCharge(booking.Parcel.Weight);

            // 2. Generate Invoice
            _invoiceGenerator.GenerateInvoice(booking, totalCharge);

            // 3. Match and route to the correct notification type
            var notifier = _notificationServices.FirstOrDefault(n => n.NotificationType.Equals(booking.NotificationType, StringComparison.OrdinalIgnoreCase));

            if (notifier != null){
                string message = $"Your parcel from {booking.Parcel.SourceCity} to {booking.Parcel.DestinationCity} has been successfully booked via {booking.DeliveryType} Delivery! Total Paid: ${totalCharge:N2}.";
                notifier.SendNotification(booking.Customer, message);
            }
            else{
                Console.WriteLine($"[Warning]: Notification type '{booking.NotificationType}' not supported. Dispatch notification skipped.");
            }
        }
    }
}