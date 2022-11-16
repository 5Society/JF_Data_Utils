using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.DataAccess;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationContext _context;
        public StudentService(ApplicationContext context)
        {
            _context = context;
        }
    }
}
