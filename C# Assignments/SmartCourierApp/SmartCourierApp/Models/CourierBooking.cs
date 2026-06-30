namespace SmartCourierApp.Models{
    public class CourierBooking{
        public Customer Customer { get; set; }
        public Parcel Parcel { get; set; }
        public string DeliveryType { get; set; }
        public string NotificationType { get; set; }

        public CourierBooking(Customer customer, Parcel parcel, string deliveryType, string notificationType){
            Customer = customer;
            Parcel = parcel;
            DeliveryType = deliveryType;
            NotificationType = notificationType;
        }
    }
}