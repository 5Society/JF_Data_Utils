using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using JF.Utils.Data.Application.Repositories;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepositoryBase<Course> _courseRepository;

        public CourseService(IUnitOfWork context)
        {
            _courseRepository = context.Repository<Course>()!;
        }
        public bool AddCourse(Course course)
        {
            _courseRepository.Add(course);
            return (_courseRepository.UnitOfWork.SaveChanges()) > 0;
        }

        public bool DeleteCourse(int id)
        {
            Course? entity = _courseRepository.GetById(id);

            if (entity is null) return false;

            _courseRepository.Delete(entity);

            return _courseRepository.UnitOfWork.SaveChanges()>0;
        }

        public IEnumerable<Course> GetAllCourses(int page, int pagesize)
        {
            return _courseRepository.GetAllPaged(page, pagesize);
        }

        public Course? GetCourseById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public bool UpdateCourse(int id, Course course)
        {
            if (id != course.Id) return false;
            _courseRepository.Update(course);
            return _courseRepository.UnitOfWork.SaveChanges() >0;

        }
    }
}
