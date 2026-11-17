using Microsoft.EntityFrameworkCore;
using PatientService.Models;

namespace PatientService.Data
{
    public class PatientDbContext:DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<MedicalConsultation> MedicalConsultations => Set<MedicalConsultation>();
        public DbSet<RecommendedTest> RecommendedTests => Set<RecommendedTest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("patients");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                       .HasColumnName("id");
                entity.Property(x => x.FirstName)
                .HasColumnName("firstname")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(x => x.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("lastname")
                    .IsRequired();

                entity.Property(x => x.Email)
                    .HasMaxLength(150)
                .HasColumnName("email");

                entity.Property(x => x.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(x => x.Gender)
                .HasColumnName("gender")
                    .HasMaxLength(20);

                entity.Property(x => x.Address)
                    .HasMaxLength(250)
                .HasColumnName("address");
                entity.Property(x => x.DateOfBirth)
                .HasColumnName("dateofbirth");
                entity.Property(x => x.CreatedAt)
                .HasColumnName("createdat");
                entity.Property(x => x.UpdatedAt)
                .HasColumnName("updatedat");
            });

            modelBuilder.Entity<MedicalConsultation>(entity =>
            {
                entity.ToTable("medical_consultations");
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.AppointmentId).IsUnique();
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.AppointmentId).HasColumnName("appointment_id");
                entity.Property(x => x.PatientId).HasColumnName("patient_id");
                entity.Property(x => x.DoctorId).HasColumnName("doctor_id");
                entity.Property(x => x.Diagnosis).HasColumnName("diagnosis").HasMaxLength(2000);
                entity.Property(x => x.Notes).HasColumnName("notes").HasMaxLength(4000);
                entity.Property(x => x.PrescribedMedicines).HasColumnName("prescribed_medicines").HasMaxLength(4000);
                entity.Property(x => x.CreatedAt).HasColumnName("created_at");
                entity.HasMany(x => x.RecommendedTests).WithOne().HasForeignKey(x => x.ConsultationId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RecommendedTest>(entity =>
            {
                entity.ToTable("recommended_tests");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.ConsultationId).HasColumnName("consultation_id");
                entity.Property(x => x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
                entity.Property(x => x.Instructions).HasColumnName("instructions").HasMaxLength(2000);
                entity.Property(x => x.Status).HasColumnName("status").HasMaxLength(100).IsRequired();
                entity.Property(x => x.Result).HasColumnName("result").HasMaxLength(4000);
                entity.Property(x => x.ReportUrl).HasColumnName("report_url").HasMaxLength(1000);
                entity.Property(x => x.ResultUploadedAt).HasColumnName("result_uploaded_at");
            });
        }
    }
}
