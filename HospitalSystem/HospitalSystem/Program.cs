using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalSystem
{
    class Program
    {
        static void Main()
        {
            var appointments = new List<Appointment>
            {
                new Appointment { Id = 1, PatientName = "John Doe", AppointmentDate = DateTime.Now.AddDays(2), Department = "Cardiology", ConsultationFee = 600, Status = AppointmentStatus.Scheduled },
                new Appointment { Id = 2, PatientName = "Jane Smith", AppointmentDate = DateTime.Now.AddDays(-1), Department = "Neurology", ConsultationFee = 450, Status = AppointmentStatus.Completed },
                new Appointment { Id = 3, PatientName = "Alice Brown", AppointmentDate = DateTime.Now.AddDays(5), Department = "Cardiology", ConsultationFee = 550, Status = AppointmentStatus.Scheduled },
                new Appointment { Id = 4, PatientName = "Bob White", AppointmentDate = DateTime.Now.AddDays(-5), Department = "Orthopedics", ConsultationFee = 700, Status = AppointmentStatus.Completed },
                new Appointment { Id = 5, PatientName = "Charlie Black", AppointmentDate = DateTime.Now.AddDays(10), Department = "Cardiology", ConsultationFee = 400, Status = AppointmentStatus.Scheduled }
            };

            Console.WriteLine("--- 6. All Appointments ---");
            appointments.ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 7. Scheduled Appointments ---");
            appointments.Where(a => a.Status == AppointmentStatus.Scheduled).ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 8. Completed Appointments ---");
            appointments.Where(a => a.Status == AppointmentStatus.Completed).ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 9. Cardiology Appointments ---");
            appointments.Where(a => a.Department == "Cardiology").ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 10. Fee > 500 ---");
            appointments.Where(a => a.ConsultationFee > 500).ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 11. Sorted by Date ---");
            appointments.OrderBy(a => a.AppointmentDate).ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 12. Search: 'Jane' ---");
            appointments.Where(a => a.PatientName.Contains("Jane", StringComparison.OrdinalIgnoreCase)).ToList().ForEach(a => Console.WriteLine(a));

            Console.WriteLine("\n--- 13. Grouped by Department ---");
            var grouped = appointments.GroupBy(a => a.Department);
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n[{group.Key}]");
                group.ToList().ForEach(a => Console.WriteLine($"  {a}"));
            }

            Console.WriteLine("\n--- 14. Count by Status ---");
            var counts = appointments.GroupBy(a => a.Status).Select(g => new { Status = g.Key, Count = g.Count() });
            foreach (var count in counts)
            {
                Console.WriteLine($"{count.Status}: {count.Count}");
            }

            Console.WriteLine("\n--- 15. Total Revenue (Completed) ---");
            var revenue = appointments.Where(a => a.Status == AppointmentStatus.Completed).Sum(a => a.ConsultationFee);
            Console.WriteLine($"Total Revenue: ${revenue}");

            Console.WriteLine("\n--- 16. Average Consultation Fee ---");
            var avgFee = appointments.Average(a => a.ConsultationFee);
            Console.WriteLine($"Average Fee: ${avgFee:F2}");

            Console.WriteLine("\n--- 17. Upcoming Appointments ---");
            appointments.Where(a => a.AppointmentDate > DateTime.Now).ToList().ForEach(a => Console.WriteLine(a));
        }
    }
}