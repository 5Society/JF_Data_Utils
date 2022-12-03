using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using JF.Utils.Data.Interfaces;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<Teacher> GetAllTeachers(int page, int pagesize);
        Teacher? GetTeacherById(int id);
        bool AddTeacher(Teacher teacher);
        bool UpdateTeacher(int id, Teacher teacher);
        bool DeleteTeacher(int id);
    }
}
