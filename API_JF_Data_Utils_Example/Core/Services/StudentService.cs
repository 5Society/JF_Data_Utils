using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using JF.Utils.Data.Extensions;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        { 
            _studentRepository = studentRepository;
        }
        public async Task<bool> AddStudent(Student student)
        {
            _studentRepository.Add(student);
            return (await _studentRepository.UnitOfWork.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            Student? entity = await _studentRepository.GetByIdAsync(id);

            if (entity is null) return false;

            _studentRepository.Delete(entity);

            return await _studentRepository.UnitOfWork.SaveChangesAsync()>0;
        }

        public IEnumerable<Student> GetAllStudents(int page, int pagesize)
        {
            return _studentRepository.GetAll().GetPaged(page, pagesize).Results;
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateStudent(int id, Student student)
        {
            if (id != student.Id) return false;
            _studentRepository.Update(student);
            return await _studentRepository.UnitOfWork.SaveChangesAsync() >0;

        }
    }
}
