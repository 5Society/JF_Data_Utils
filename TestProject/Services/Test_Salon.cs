

using API_JF_Data_Utils_Example.Core.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TestProject.Services
{
    public class Test_Salon
    {
        private SalonService _service;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<JFContext> options = new DbContextOptionsBuilder<JFContext>();
            options.UseInMemoryDatabase("Test_Salon");
            ApplicationContext context = new ApplicationContext(options.Options);
            SalonRepository repository = new SalonRepository(context);
            _service = new SalonService(repository);
            //Create Courses to test
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course() { Name = "Test1", Summary = "Summary 1" });
                context.Courses.Add(new Course() { Name = "Test2", Summary = "Summary 2" });
                context.Courses.Add(new Course() { Name = "Test3", Summary = "Summary 3" });
                context.SaveChanges();
            }
            //Creates Tearchers to test
            if (!context.Teachers.Any())
            {
                context.Teachers.Add(new Teacher() { Name = "Test1", LastName = "Test 1" });
                context.Teachers.Add(new Teacher() { Name = "Test2", LastName = "Test 2" });
                context.Teachers.Add(new Teacher() { Name = "Test3", LastName = "Test 3" });
                context.SaveChanges();
            }
            
        }

        [TestCase("name", 1, 1,  true)]
        [TestCase("Asignature 1", 2, 1, true)]
        [TestCase("Asignature 1", 2, 50, false)]
        [TestCase("name", 1, null, true)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddSalon_Ok(string name, int idCourse, int? idTeacher, bool resultExpexted)
        {
            Salon salon = new Salon() { Name = name, CourseId= idCourse, TeacherId = idTeacher};
            salon.CourseId = idCourse;
            bool result = _service.AddSalon(salon);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no esté borrado
                    Assert.That(salon.IsDeleted, Is.False);
                    Assert.That(salon.DeletedBy, Is.Null);
                }
            });
        }

        [TestCase("name", 5, 1)]
        [TestCase("", 1, 1)]
        [TestCase(null, 1, 1)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddSalon_Exception(string name, int idCourse, int? idTeacher)
        {

            Salon salon = new Salon() { Name = name, CourseId = idCourse, TeacherId = idTeacher };
            Assert.That(() => _service.AddSalon(salon), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [TestCase(1, 100, 3)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(4, 10, 0)]
        [Test, Order(2)]
        public void GetAllSalons(int page, int pagesize, int resultExpexted)
        {
            var result = _service.GetAllSalons(page, pagesize);
            Assert.That(result.Count(), Is.EqualTo(resultExpexted));
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        [Test, Order(2)]
        public void GetSalonById(int id, bool resultExpexted)
        {
            Salon? result = _service.GetSalonById(id);
            Assert.That(result != null, Is.EqualTo(resultExpexted));
        }

        [TestCase(1, 1, "name1 ", 1, 1, true)]
        [TestCase(1, 1, "name1 ", 2, 1, true)]
        [TestCase(1, 1, "name1 ", 2, 2, true)]
        [TestCase(1, 1, "name1 ", 2, 20, false)]
        [TestCase(2, 2, "name 1", 1, 1, true)]
        [TestCase(1, 10, "test1 ", 1,1, false)]
        [TestCase(2, 20, "test 1", 1,1, false)]
        [TestCase(10, 10, "Name1 ", 1, 1, false)]
        [TestCase(20, 20, "test 1", 1, 1, false)]
        [Test, Order(2)]
        public void UpdateSalon_OK(int id, int SalonId, string name, int idCourse, int idTeacher, bool resultExpexted)
        {
            Salon salon = new Salon() { Id = SalonId, Name = name, CourseId = idCourse, TeacherId = idTeacher };
            bool result = _service.UpdateSalon(id, salon);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(salon.IsDeleted, Is.False);
                    Assert.That(salon.DeletedBy, Is.Null);
                }
            });
        }

        [TestCase(1, 1, "Salon 1", 10,1)]
        [TestCase(2, 2, "", 1,1)]
        [TestCase(1, 1, "salon 1", 0,1)]
        [TestCase(2, 2, null, 1,1)]
        [Test, Order(2)]
        public void UpdateSalon_Exception(int id, int SalonId, string name, int idCourse, int idTeacher)
        {
            Salon salon = new Salon() { Id = SalonId, Name = name, CourseId= idCourse, TeacherId= idTeacher };
            Assert.That(() => _service.UpdateSalon(id, salon), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [NonParallelizable]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(10, false)]
        [TestCase(20, false)]
        [Test, Order(3)]
        public void DeleteSalon(int id, bool resultExpexted)
        {
            Salon? salon = _service.GetSalonById(id);
            bool result = _service.DeleteSalon(id);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (salon != null)
                {
                    //Valida que esté borrado
                    Assert.That(salon.IsDeleted, Is.True);
                    Assert.That(salon.DeletedBy, Is.Not.Null);
                }
            });
        }

    }
  
}
