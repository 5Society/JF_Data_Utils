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
        public bool AddTeacher(Teacher teacher)
        {
            _teacherRepository.Add(teacher);
            return (_teacherRepository.UnitOfWork.SaveChanges()) > 0;
        }

        public bool DeleteTeacher(int id)
        {
            Teacher? entity = _teacherRepository.GetById(id);

            if (entity is null) return false;

            _teacherRepository.Delete(entity);

            return _teacherRepository.UnitOfWork.SaveChanges()>0;
        }

        public IEnumerable<Teacher> GetAllTeachers(int page, int pagesize)
        {
            return _teacherRepository.GetAllPaged(page, pagesize);
        }

        public Teacher? GetTeacherById(int id)
        {
            return _teacherRepository.GetById(id);
        }

        public bool UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id) return false;
            _teacherRepository.Update(teacher);
            return _teacherRepository.UnitOfWork.SaveChanges() >0;

        }
    }
}
