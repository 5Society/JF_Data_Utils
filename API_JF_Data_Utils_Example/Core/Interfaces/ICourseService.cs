using API_JF_Data_Utils_Example.Core.Models;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses(int page, int pagesize);
        Course? GetCourseById(int id);
        Task<bool> AddCourse(Course course);
        Task<bool> UpdateCourse(int id, Course course);
        Task<bool> DeleteCourse(int id);
    }
}
