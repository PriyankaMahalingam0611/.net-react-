using System;
using SmartCourierApp.Models;

namespace SmartCourierApp.Notifications{
    public class SmsNotificationService : INotificationService{
        public string NotificationType => "SMS";

        public void SendNotification(Customer customer, string message){
            Console.WriteLine($"[SMS Sent to {customer.MobileNumber}]: {message}");
        }
    }
}