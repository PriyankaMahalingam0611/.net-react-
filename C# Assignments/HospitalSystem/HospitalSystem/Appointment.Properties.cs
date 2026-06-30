using System;

namespace HospitalSystem
{
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }

    public partial class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Department { get; set; }
        public decimal ConsultationFee { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}