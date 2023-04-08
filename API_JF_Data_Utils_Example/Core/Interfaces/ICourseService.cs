using API_JF_Data_Utils_Example.Core.Models;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses(int page, int pagesize);
        Course? GetCourseById(int id);
        bool AddCourse(Course course);
        bool UpdateCourse(int id, Course course);
        bool DeleteCourse(int id);
    }
}
