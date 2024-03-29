﻿using API_JF_Data_Utils_Example.Core.Models;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents(int page, int pagesize);
        Task<Student?> GetStudentById(int id);
        Task<bool> AddStudent(Student student);
        Task<bool> UpdateStudent(int id, Student student);
        Task<bool> DeleteStudent(int id);
    }
}
