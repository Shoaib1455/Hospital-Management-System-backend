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
        }
    }
}
