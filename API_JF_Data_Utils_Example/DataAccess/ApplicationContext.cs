using API_JF_Data_Utils_Example.Core.Models;
using JF.Utils.Data.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace API_JF_Data_Utils_Example.DataAccess
{
    public class ApplicationContext : JFContext
    {
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Salon> Salons => Set<Salon>();

        public ApplicationContext(DbContextOptions<JFContext> options) : base(options) { }

    }
}
