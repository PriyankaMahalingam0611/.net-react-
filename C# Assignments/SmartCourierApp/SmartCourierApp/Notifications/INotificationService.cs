using SmartCourierApp.Models;

namespace SmartCourierApp.Notifications{
    public interface INotificationService{
        string NotificationType { get; }
        void SendNotification(Customer customer, string message);
    }
}