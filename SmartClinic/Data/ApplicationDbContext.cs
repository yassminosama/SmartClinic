using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Models;


namespace SmartClinic.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for each table

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medication> Medications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Fluent API configuration

            // Configure precision and scale for decimal properties
            modelBuilder.Entity<Bill>()
                .Property(b => b.Amount)
                .HasPrecision(18, 2); // Precision: 18, Scale: 2

            modelBuilder.Entity<Receptionist>()
                .Property(r => r.Salary)
                .HasPrecision(18, 2); // Precision: 18, Scale: 2


            ///////////////////////
            //modelBuilder.Entity<Receptionist>()
            //   .HasOne(r => r.Doctor)
            //   .WithMany(d => ((Doctor)d).Receptionists)
            //   .HasForeignKey(r => r.DoctorId)
            //   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receptionist>()
                 .HasOne(r => r.Doctor)
                 .WithMany(d => d.Receptionists)
                 .HasForeignKey(r => r.Id)
                 .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                 .HasOne(a => a.Bill)
                 .WithOne(b => b.Appointment)
                 .HasForeignKey<Bill>(b => b.AppointmentId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.Bills)
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Patient)
                .WithMany(p => p.Reports)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Patient)
                .WithMany(d => d.Reports)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Medication>()
                .HasOne(m => m.Prescription)
                .WithMany(p => p.Medications)
                .HasForeignKey(m => m.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);







            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole
               {
                   Id = "08315c1a-38fe-4c63-b1db-24bd4e171c46",
                   ConcurrencyStamp = "cb0bf9cc-76b2-4a01-8e66-378b16165d88",
                   Name = "Receptionist",
                   NormalizedName = "RECEPTIONIST"
               },
               new IdentityRole
               {
                   Id = "756d924b-cec7-4005-b796-8b91efd141cc",
                   ConcurrencyStamp = "3a68da19-f24e-4967-afaa-fe06f99e8fe9",
                   Name = "Admin",
                   NormalizedName = "ADMIN"
               },
               new IdentityRole
               {
                   Id = "a81dc085-b544-42ff-b0b1-1f6bdfe29e69",
                   ConcurrencyStamp = "296e52b6-a3d3-446e-81a9-14bab21686ca",
                   Name = "Doctor",
                   NormalizedName = "DOCTOR"
               },
               new IdentityRole
               {
                   Id = "fd1569ea-50da-4ff7-95d7-9a14d12830a4",
                   ConcurrencyStamp = "dea9bd91-b91b-4486-858f-ca80ae5305d3",
                   Name = "Patient",
                   NormalizedName = "PATIENT"
               }
                );
          

            base.OnModelCreating(modelBuilder);
        }
    }
}




