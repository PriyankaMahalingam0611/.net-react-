using System.Net.NetworkInformation;

namespace HospitalSystem
{
    public partial class Appointment
    {
        public void MarkAsCompleted()
        {
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Completed;
            }
        }

        public override string ToString()
        {
            return $"{PatientName,-15} | {Department,-15} | {AppointmentDate:yyyy-MM-dd HH:mm} | {Status,-10} | ${ConsultationFee}";
        }
    }
}