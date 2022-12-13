

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TestProject.Services
{
    public class Test_Course
    {
        private CourseService _service;

        [SetUp]
        public void Setup()
        {
            CourseRepository repository = new CourseRepository(InitialSetup.GetAppContext);
            _service = new CourseService(repository);
        }

        [TestCase("name", "summary", true)]
        [TestCase("Asignature 1", "This is a test asignature", true)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddCourse_Ok(string name, string summary, bool resultExpexted)
        {
            Course course = new Course() { Name = name, Summary = summary};
            bool result = _service.AddCourse(course);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(course.IsDeleted, Is.False);
                    Assert.That(course.DeletedBy, Is.Null);
                }
            });
        }

        [TestCase("", "summary")]
        [TestCase("name", "")]
        [TestCase("name", null)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddCourse_Exception(string name, string summary)
        {
            Course course = new Course(){ Name = name, Summary = summary};
            Assert.That(() => _service.AddCourse(course), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [TestCase(1, 100, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(3, 1, 0)]
        [TestCase(4, 10, 0)]
        [Test, Order(2)]
        public void GetAllCourses(int page, int pagesize, int resultExpexted)
        {
            var result = _service.GetAllCourses(page, pagesize);
            Assert.That(result.Count(), Is.EqualTo(resultExpexted));
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [Test, Order(2)]
        public void GetCourseById(int id, bool resultExpexted)
        {
            Course? result = _service.GetCourseById(id);
            Assert.That(result != null, Is.EqualTo(resultExpexted));
        }

        [TestCase(1, 1, "name1 ", "sumary 1", true)]
        [TestCase(2, 2, "name 1", "summary 2", true)]
        [TestCase(1, 10, "test1 ", "test 1", false)]
        [TestCase(2, 20, "test 1", "test 2", false)]
        [TestCase(10, 10, "Name1 ", "summary 1", false)]
        [TestCase(20, 20, "test 1", "test 2", false)]
        [Test, Order(2)]
        public void UpdateCourse_OK(int id, int courseId, string name, string summary, bool resultExpexted)
        {
            Course course = new Course() { Id = courseId, Name = name, Summary = summary };
            bool result = _service.UpdateCourse(id, course);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(course.IsDeleted, Is.False);
                    Assert.That(course.DeletedBy, Is.Null);
                }
            });
        }

        [TestCase(1, 1, "lastName1 ", "")]
        [TestCase(2, 2, "", "Pepito 2")]
        [TestCase(1, 1, "lastName1 ", null)]
        [TestCase(2, 2, null, "Pepito 2")]
        [Test, Order(2)]
        public void UpdateCourse_Exception(int id, int courseId, string name, string summary)
        {
            Course course = new Course() { Id = courseId, Name = name, Summary = summary };
            Assert.That(() => _service.UpdateCourse(id, course), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [NonParallelizable]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(10, false)]
        [TestCase(20, false)]
        [Test, Order(3)]
        public void DeleteCourse(int id, bool resultExpexted)
        {
            Course? course = _service.GetCourseById(id);
            bool result = _service.DeleteCourse(id);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (course != null)
                {
                    //Valida que esté borrado
                    Assert.That(course.IsDeleted, Is.True);
                    Assert.That(course.DeletedBy, Is.Not.Null);
                }
            });
        }

    }
  
}
