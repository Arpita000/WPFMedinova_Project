using Microsoft.EntityFrameworkCore;

namespace WPFMedinova.Models
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options):base (options)
        {

        }

        public DbSet<LoginModel> Login_Table { get; set; }
        public DbSet<RegisterModel> Register_Table { get; set; } //for Patients (User) 
        public DbSet<Create_Doctor_Model> Doctor_Table { get; set; } //for Doctors
        public DbSet<AppointmentModel> Appointment_Table { get; set; } //for patients' appointment after registration
        public DbSet<Specialization> specializations { get; set; }     //for cascading dropdown
    }
}
