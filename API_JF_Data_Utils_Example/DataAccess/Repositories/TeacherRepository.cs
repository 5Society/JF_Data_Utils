using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using JF.Utils.Data;
using JF.Utils.Data.Interfaces;

namespace API_JF_Data_Utils_Example.DataAccess.Repositories
{
    public class TeacherRepository : JFRepositoryBase<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
