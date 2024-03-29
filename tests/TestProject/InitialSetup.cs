﻿using JF.Utils.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    [SetUpFixture]
    internal sealed class InitialSetup
    {
        internal static ApplicationContext GetAppContext
        {
            get
            {
                ApplicationContext appContext;
                DbContextOptionsBuilder<JFContext> options = new DbContextOptionsBuilder<JFContext>();
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_APIExample;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                appContext = new ApplicationContext(options.Options);
                return appContext;
            }
        }
        internal static ApplicationContext GetAppContextSalon
        {
            get
            {
                ApplicationContext appContext;
                DbContextOptionsBuilder<JFContext> options = new DbContextOptionsBuilder<JFContext>();
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test_APIExample_Salon;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                appContext = new ApplicationContext(options.Options);
                return appContext;
            }
        }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            ApplicationContext appContext = GetAppContext;
            appContext.Database.EnsureDeleted();
            appContext.Database.EnsureCreated();
            appContext.Dispose();

            ApplicationContext appContextSalon = GetAppContextSalon;
            appContextSalon.Database.EnsureDeleted();
            appContextSalon.Database.EnsureCreated();

            appContextSalon.Courses.Add(new Course() { Name = "Test1", Summary = "Summary 1" });
            appContextSalon.Courses.Add(new Course() { Name = "Test2", Summary = "Summary 2" });
            appContextSalon.Courses.Add(new Course() { Name = "Test3", Summary = "Summary 3" });

            appContextSalon.Teachers.Add(new Teacher() { Name = "Test1", LastName = "Test 1" });
            appContextSalon.Teachers.Add(new Teacher() { Name = "Test2", LastName = "Test 2" });
            appContextSalon.Teachers.Add(new Teacher() { Name = "Test3", LastName = "Test 3" });

            appContextSalon.SaveChanges();
            appContextSalon.Dispose();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            // Do logout here
        }
    }
}
