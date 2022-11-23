using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using API_JF_Data_Utils_Example.DataAccess.Repositories;
using JF.Utils.Data;
using JF.Utils.Data.Interfaces;

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

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateStudent(int id, Student student)
        {
            _studentRepository.Update(student);
            return await _studentRepository.UnitOfWork.SaveChangesAsync() >0;

        }
    }
}
