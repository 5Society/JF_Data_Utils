using API_JF_Data_Utils_Example.Core.Models;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<Teacher> GetAllTeachers(int page, int pagesize);
        Teacher? GetTeacherById(int id);
        Task<bool> AddTeacher(Teacher teacher);
        Task<bool> UpdateTeacher(int id, Teacher teacher);
        Task<bool> DeleteTeacher(int id);
    }
}
