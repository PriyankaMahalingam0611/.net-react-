namespace SmartCourierApp.DeliveryCalculators{
    public class StandardDeliveryCalculator : IDeliveryChargeCalculator{
        public string DeliveryType => "Standard";

        public double CalculateCharge(double weight){
            return weight * 50;
        }
    }
}