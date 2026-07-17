using AppointmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentService.Data
{
    public class AppointmentDbContext:DbContext
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Appointment> Appointments => Set<Appointment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Status)
                    .HasMaxLength(30)
                    .IsRequired();
            });
        }
    }
}
