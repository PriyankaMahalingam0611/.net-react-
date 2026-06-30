using System;
using SmartCourierApp.Models;

namespace SmartCourierApp.Notifications{
    public class EmailNotificationService : INotificationService{
        public string NotificationType => "Email";

        public void SendNotification(Customer customer, string message){
            Console.WriteLine($"[Email Sent to {customer.Email}]: {message}");
        }
    }
}