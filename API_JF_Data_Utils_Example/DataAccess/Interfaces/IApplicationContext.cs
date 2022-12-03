using API_JF_Data_Utils_Example.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace API_JF_Data_Utils_Example.DataAccess.Interfaces
{
    public interface IApplicationContext
    {
        public DbSet<Student> Students { get; }
        public DbSet<Teacher> Teachers { get; }
        public DbSet<Course> Courses { get; }
        public DbSet<Salon> Classes { get; }
    }
}
