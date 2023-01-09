

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TestProject.Services
{
    public class TestTeacher
    {
        private TeacherService _service;

        [SetUp]
        public void Setup()
        {
            _service = new TeacherService(InitialSetup.GetAppContext);
        }

        [TestCase("lastName", "name", true)]
        [TestCase("Pepito", "Perez", true)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddTeacher_Ok(string lastName, string name, bool resultExpexted)
        {
            Teacher teacher = new Teacher() { LastName = lastName, Name = name };
            var result = _service.AddTeacher(teacher);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(teacher.IsDeleted, Is.False);
                    Assert.That(teacher.DeletedBy, Is.Null);
                    //Valida que no est� modificad  o
                    Assert.That(teacher.LastModifiedBy, Is.Null);
                    Assert.That(teacher.LastModifiedDate, Is.Null);
                    //Valida que tenga la informaci�n de creaci�n
                    NUnit.Framework.Constraints.NullConstraint @null = Is.Not.Null;
                    Assert.That(teacher.CreatedBy, Is.Not.Null);
                    Assert.That(teacher.CreatedDate, @null);
                }
            });
        }

        [TestCase("", "name")]
        [TestCase("lastName", "")]
        [TestCase("Pepito", null)]
        [NonParallelizable]
        [Test, Order(1)]
        public void AddTeacher_Exception(string lastName, string name)
        {
            Teacher teacher = new Teacher() { LastName = lastName, Name = name };
            Assert.That(() => _service.AddTeacher(teacher), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [TestCase(1, 100, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(3, 1, 0)]
        [TestCase(4, 10, 0)]
        [Test, Order(2)]
        public void GetAllTeachers(int page, int pagesize, int resultExpexted)
        {
            var result = _service.GetAllTeachers(page, pagesize);
            Assert.That(result.Count(), Is.EqualTo(resultExpexted));
        }

        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, false)]
        [Test, Order(2)]
        public void GetTeacherById(int id, bool resultExpexted)
        {
            var result = _service.GetTeacherById(id);
            Assert.That(result != null, Is.EqualTo(resultExpexted));
        }

        [TestCase(1, 1, "lastName1 ", "name 1", true)]
        [TestCase(2, 2, "Pepito 1", "Pepito 2", true)]
        [TestCase(1, 10, "lastName1 ", "name 1", false)]
        [TestCase(2, 20, "Pepito 1", "Pepito 2", false)]
        [TestCase(10, 10, "lastName1 ", "name 1", false)]
        [TestCase(20, 20, "Pepito 1", "Pepito 2", false)]
        [Test, Order(2)]
        public void UpdateTeacher_OK(int id, int TeacherId, string lastName, string name, bool resultExpexted)
        {
            Teacher teacher = new Teacher() { Id = TeacherId, LastName = lastName, Name = name };
            var result = _service.UpdateTeacher(id, teacher);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (result)
                {
                    //Valida que no est� borrado
                    Assert.That(teacher.IsDeleted, Is.False);
                    Assert.That(teacher.DeletedBy, Is.Null);
                    //Valida que est� modificado
                    Assert.That(teacher.LastModifiedBy, Is.Not.Null);
                    Assert.That(teacher.LastModifiedDate, Is.Not.Null);
                    //Valida que tenga la informaci�n de creaci�n
                    NUnit.Framework.Constraints.NullConstraint @null = Is.Not.Null;
                    Assert.That(teacher.CreatedBy, Is.Not.Null);
                    Assert.That(teacher.CreatedDate, @null);
                }
            });
        }

        [TestCase(1, 1, "lastName1 ", "")]
        [TestCase(2, 2, "", "Pepito 2")]
        [TestCase(1, 1, "lastName1 ", null)]
        [TestCase(2, 2, null, "Pepito 2")]
        [Test, Order(2)]
        public void UpdateTeacher_Exception(int id, int TeacherId, string lastName, string name)
        {
            Teacher teacher = new Teacher() { Id = TeacherId, LastName = lastName, Name = name };
            Assert.That(() => _service.UpdateTeacher(id, teacher), Throws.TypeOf<System.ComponentModel.DataAnnotations.ValidationException>());
        }

        [NonParallelizable]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(10, false)]
        [TestCase(20, false)]
        [Test, Order(3)]
        public void DeleteTeacher(int id, bool resultExpexted)
        {
            Teacher? teacher = _service.GetTeacherById(id);
            var result = _service.DeleteTeacher(id);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(resultExpexted));
                if (teacher != null)
                {
                    //Valida que est� borrado
                    Assert.That(teacher.IsDeleted, Is.True);
                    Assert.That(teacher.DeletedBy, Is.Not.Null);
                    //Valida que tenga la informaci�n de creaci�n
                    NUnit.Framework.Constraints.NullConstraint @null = Is.Not.Null;
                    Assert.That(teacher.CreatedBy, Is.Not.Null);
                    Assert.That(teacher.CreatedDate, @null);
                }
            });
        }

    }
}
