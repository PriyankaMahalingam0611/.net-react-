namespace SmartCourierApp.DeliveryCalculators{
    public class InternationalDeliveryCalculator : IDeliveryChargeCalculator{
        public string DeliveryType => "International";

        public double CalculateCharge(double weight){
            return (weight * 150) + 500;
        }
    }
}