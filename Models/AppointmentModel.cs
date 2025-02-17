using System.ComponentModel.DataAnnotations;

namespace WPFMedinova.Models
{
    public class AppointmentModel
    {
        [Key]
        public int Id { get; set; }
        public string ?Patient_Name { get; set; }
        public string ?Specialization { get; set; }
        public string ?Doctor { get; set; }
        public DateTime ?Appointment_Date { get; set; }
        public string ?Status { get; set; }
    }
}
