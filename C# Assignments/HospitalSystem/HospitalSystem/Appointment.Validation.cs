namespace HospitalSystem
{
    public partial class Appointment
    {
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(PatientName)) return false;
            if (string.IsNullOrWhiteSpace(Department)) return false;
            if (ConsultationFee < 0) return false;

            return true;
        }
    }
}