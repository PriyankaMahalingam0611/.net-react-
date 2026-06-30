namespace SmartCourierApp.DeliveryCalculators{
    public interface IDeliveryChargeCalculator{
        string DeliveryType { get; }
        double CalculateCharge(double weight);
    }
}