using SmartCourierApp.Models;

namespace SmartCourierApp.Invoices{
    public interface IInvoiceGenerator{
        void GenerateInvoice(CourierBooking booking, double finalCharge);
    }
}