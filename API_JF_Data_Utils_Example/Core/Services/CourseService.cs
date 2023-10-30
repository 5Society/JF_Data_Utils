using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using JF.Utils.Application.Persistence;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;

        public CourseService(IUnitOfWork context)
        {
            _courseRepository = context.Repository<Course>()!;
        }
        public async Task<bool> AddCourse(Course course)
        {
            await _courseRepository.AddAsync(course);
            return (await _courseRepository.UnitOfWork.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteCourse(int id)
        {
            Course? entity = _courseRepository.GetById(id);

            if (entity is null) return false;

            _courseRepository.Delete(entity);

            return (await _courseRepository.UnitOfWork.SaveChangesAsync())>0;
        }

        public IEnumerable<Course> GetAllCourses(int page, int pagesize)
        {
            return _courseRepository.GetAllPaged(page, pagesize);
        }

        public Course? GetCourseById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public async Task<bool> UpdateCourse(int id, Course course)
        {
            if (id != course.Id) return false;
            _courseRepository.Update(id, course);
            return (await _courseRepository.UnitOfWork.SaveChangesAsync()) >0;

        }
    }
}
