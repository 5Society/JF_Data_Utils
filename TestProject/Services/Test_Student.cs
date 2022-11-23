

using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TestProject.Services
{
    public class Test_Student
    {
        private StudentService _service;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder<JFContext> options = new DbContextOptionsBuilder<JFContext>();
            options.UseInMemoryDatabase("Test");
            ApplicationContext context = new ApplicationContext(options.Options);
            StudentRepository repository = new StudentRepository(context);
            _service = new StudentService(repository);
        }

        [TestCase("lastName", "name", true)]
        [TestCase("Pepito", "Perez", true)]
        [TestCase("", "name", false)]
        [TestCase("lastName", "", false)]
        [TestCase("Pepito", null, true)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddStudent(string lastName, string name, bool resultExpexted)
        {
            Student student = new Student() { LastName=lastName, Name=name };
            var result = _service.AddStudent(student).Result;
            Assert.That(result, Is.EqualTo(resultExpexted));
            if (result)
            {
                //Valida que no esté borrado
                Assert.False(student.IsDeleted);
                Assert.IsNull(student.DeletedBy);
                //Valida que no esté modificado
                Assert.IsNull(student.LastModifiedBy);
                Assert.IsNull(student.LastModifiedDate);
                //Valida que tenga la información de creación
                Assert.IsNotNull(student.CreatedBy);
                Assert.IsNotNull(student.CreatedDate);
            }
        }

        [Test, Order(2)]
        public void GetAllStudents()
        {
            var result = _service.GetAllStudents();
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [Test, Order(2)]
        public void GetStudentById(int id, bool resultExpexted)
        {
            var result = _service.GetStudentById(id).Result;
            Assert.That(result != null, Is.EqualTo(resultExpexted));
        }
        
        [TestCase(1, 1, "lastName1 ", "name 1", true)]
        [TestCase(2, 2, "Pepito 1", "Pepito 2", true)]
        [TestCase(1, 1, "lastName1 ", "", false)]
        [TestCase(2, 2, "", "Pepito 2", false)]
        [TestCase(1, 10, "lastName1 ", "name 1", false)]
        [TestCase(2, 20, "Pepito 1", "Pepito 2", false)]
        [TestCase(10, 10, "lastName1 ", "name 1", false)]
        [TestCase(20, 20, "Pepito 1", "Pepito 2", false)]
        [Test, Order(2)]
        public void UpdateStudent(int id, int studentId, string lastName, string name, bool resultExpexted)
        {
            Student student = new Student() { Id= studentId, LastName = lastName, Name = name };
            var result = _service.UpdateStudent(id, student).Result;
            Assert.That(result, Is.EqualTo(resultExpexted));
            if (result)
            {
                //Valida que no esté borrado
                Assert.False(student.IsDeleted);
                Assert.IsNull(student.DeletedBy);
                //Valida que esté modificado
                Assert.IsNotNull(student.LastModifiedBy);
                Assert.IsNotNull(student.LastModifiedDate);
                //Valida que tenga la información de creación
                Assert.IsNotNull(student.CreatedBy);
                Assert.IsNotNull(student.CreatedDate);
            }
        }

        [NonParallelizable]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(10, false)]
        [TestCase(20, false)]
        [Test, Order(3)]
        public void DeleteStudent(int id, bool resultExpexted)
        {
            Student? student = _service.GetStudentById(id).Result;
            var result = _service.DeleteStudent(id).Result;
            Assert.That(result, Is.EqualTo(resultExpexted));
            if (student!=null)
            {
                //Valida que esté borrado
                Assert.True(student.IsDeleted);
                Assert.IsNotNull(student.DeletedBy);
                //Valida que tenga la información de creación
                Assert.IsNotNull(student.CreatedBy);
                Assert.IsNotNull(student.CreatedDate);
            }
        }
        
    }
}
