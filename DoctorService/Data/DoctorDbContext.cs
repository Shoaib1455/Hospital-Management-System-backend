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
    }
}
