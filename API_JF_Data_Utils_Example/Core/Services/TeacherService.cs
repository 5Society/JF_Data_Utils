using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using JF.Utils.Infrastructure.Persistence;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepository<Teacher> _teacherRepository;

        public TeacherService(IUnitOfWork context)
        { 
            _teacherRepository = context.Repository<Teacher>()!;
        }
        public async Task<bool> AddTeacher(Teacher teacher)
        {
            await _teacherRepository.AddAsync(teacher);
            return (await _teacherRepository.UnitOfWork.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            Teacher? entity = _teacherRepository.GetById(id);

            if (entity is null) return false;

            _teacherRepository.Delete(entity);

            return (await _teacherRepository.UnitOfWork.SaveChangesAsync())>0;
        }

        public IEnumerable<Teacher> GetAllTeachers(int page, int pagesize)
        {
            return _teacherRepository.GetAllPaged(page, pagesize);
        }

        public Teacher? GetTeacherById(int id)
        {
            return _teacherRepository.GetById(id);
        }

        public async Task<bool> UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id) return false;
            _teacherRepository.Update(id, teacher);
            return (await _teacherRepository.UnitOfWork.SaveChangesAsync()) >0;

        }
    }
}
