using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using JF.Utils.Data;
using JF.Utils.Data.Interfaces;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class StudentService : JFRepositoryBase<Student>, IStudentService
    {
        public StudentService(IUnitOfWork context): base(context)
        {}
    }
}
