using DoctorService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DoctorService.Data
{
    public class DoctorDbContext: DbContext
    {
        public DoctorDbContext(DbContextOptions<DoctorDbContext> options)
        : base(options)
        {
        }

        public DbSet<Doctor> Doctors => Set<Doctor>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Doctor>().ToTable("doctors");

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("doctors");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.FirstName).HasColumnName("firstname");
                entity.Property(e => e.LastName).HasColumnName("lastname");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Phone).HasColumnName("phone");
                entity.Property(e => e.Specialization).HasColumnName("specialization");
                entity.Property(e => e.ConsultationFee).HasColumnName("consultationfee");
                entity.Property(e => e.StartTime).HasColumnName("starttime");
                entity.Property(e => e.EndTime).HasColumnName("endtime");
                entity.Property(e => e.IsActive).HasColumnName("isactive");
                entity.Property(e => e.CreatedAt).HasColumnName("createdat");
                entity.Property(e => e.UpdatedAt).HasColumnName("updatedat");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
