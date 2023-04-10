using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using JF.Utils.Infrastructure.Persistence;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;

        public StudentService(IUnitOfWork context)
        { 
            _studentRepository = context.Repository<Student>()!; 
        }
        public async Task<bool> AddStudent(Student student)
        {
            await _studentRepository.AddAsync(student);
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
            return _studentRepository.GetAllPaged(page, pagesize);
        }

        public async Task<Student?> GetStudentById(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateStudent(int id, Student student)
        {
            if (id != student.Id) return false;
            _studentRepository.Update(id, student);
            return await _studentRepository.UnitOfWork.SaveChangesAsync() >0;

        }
    }
}
