
namespace TestProject.Services
{
    public class TestStudent
    {
        private StudentService _service;

        [SetUp]
        public void Setup()
        {
            _service = new StudentService(InitialSetup.GetAppContext);
        }

        [TestCase("lastName", "name", true)]
        [TestCase("Pepito", "Perez", true)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddStudentOk(string lastName, string name, bool resultExpexted)
        {
            Student student = new() { LastName = lastName, Name = name };
            var result = _service.AddStudent(student).Result;

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(student.IsDeleted, Is.False);
                    Assert.That(student.DeletedBy, Is.Null);
                    //Valida que no est� modificad  o
                    Assert.That(student.LastModifiedBy, Is.Null);
                    Assert.That(student.LastModifiedDate, Is.Null);
                    //Valida que tenga la informaci�n de creaci�n
                    NUnit.Framework.Constraints.NullConstraint @null = Is.Not.Null;
                    Assert.That(student.CreatedBy, Is.Not.Null);
                    Assert.That(student.CreatedDate, @null);
                }
            });
        }

        [TestCase("", "name")]
        [TestCase("lastName", "")]
        [TestCase("Pepito", null)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddStudentException(string lastName, string? name)
        {
            Student student = new() { LastName = lastName, Name = name! };
            Assert.That(() => _service.AddStudent(student), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [TestCase(1, 100, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(3, 1, 0)]
        [TestCase(4, 10, 0)]
        [Test, Order(2)]
        public void GetAllStudents(int page, int pagesize, int resultExpexted)
        {
            var result = _service.GetAllStudents(page, pagesize);
            Assert.That(result.Count(), Is.EqualTo(resultExpexted));
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
        [TestCase(1, 10, "lastName1 ", "name 1", false)]
        [TestCase(2, 20, "Pepito 1", "Pepito 2", false)]
        [TestCase(10, 10, "lastName1 ", "name 1", false)]
        [TestCase(20, 20, "Pepito 1", "Pepito 2", false)]
        [Test, Order(2)]
        public void UpdateStudentOK(int id, int studentId, string lastName, string name, bool resultExpexted)
        {
            Student student = new() { Id = studentId, LastName = lastName, Name = name };
            var result = _service.UpdateStudent(id, student).Result;
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(student.IsDeleted, Is.False);
                    Assert.That(student.DeletedBy, Is.Null);
                    //Valida que est� modificado
                    Assert.That(student.LastModifiedBy, Is.Not.Null);
                    Assert.That(student.LastModifiedDate, Is.Not.Null);
                    //Valida que tenga la informaci�n de creaci�n
                    NUnit.Framework.Constraints.NullConstraint @null = Is.Not.Null;
                    Assert.That(student.CreatedBy, Is.Not.Null);
                    Assert.That(student.CreatedDate, @null);
                }
            });
        }

        [TestCase(1, 1, "lastName1 ", "")]
        [TestCase(2, 2, "", "Pepito 2")]
        [TestCase(1, 1, "lastName1 ", null)]
        [TestCase(2, 2, null, "Pepito 2")]
        [Test, Order(2)]
        public void UpdateStudentException(int id, int studentId, string? lastName, string? name)
        {
            Student student = new() { Id = studentId, LastName = lastName!, Name = name! };
            Assert.That(() => _service.UpdateStudent(id, student), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
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
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (student != null)
                {
                    //Valida que est� borrado
                    Assert.That(student.IsDeleted, Is.True);
                    Assert.That(student.DeletedBy, Is.Not.Null);
                    //Valida que tenga la informaci�n de creaci�n
                    NUnit.Framework.Constraints.NullConstraint @null = Is.Not.Null;
                    Assert.That(student.CreatedBy, Is.Not.Null);
                    Assert.That(student.CreatedDate, @null);
                }
            });
        }

    }
}
