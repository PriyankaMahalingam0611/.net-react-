namespace SmartCourierApp.DeliveryCalculators{
    public class ExpressDeliveryCalculator : IDeliveryChargeCalculator{
        public string DeliveryType => "Express";

        public double CalculateCharge(double weight){
            return (weight * 80) + 100;
        }
    }
}