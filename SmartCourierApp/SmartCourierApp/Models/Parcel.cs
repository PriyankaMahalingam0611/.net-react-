namespace SmartCourierApp.Models{
    public class Parcel{
        public double Weight { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }

        public Parcel(double weight, string sourceCity, string destinationCity){
            Weight = weight;
            SourceCity = sourceCity;
            DestinationCity = destinationCity;
        }
    }
}