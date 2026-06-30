using System;
using SmartCourierApp.Models;

namespace SmartCourierApp.Notifications{
    public class WhatsAppNotificationService : INotificationService{
        public string NotificationType => "WhatsApp";

        public void SendNotification(Customer customer, string message){
            Console.WriteLine($"[WhatsApp Message Sent to {customer.MobileNumber}]: {message}");
        }
    }
}