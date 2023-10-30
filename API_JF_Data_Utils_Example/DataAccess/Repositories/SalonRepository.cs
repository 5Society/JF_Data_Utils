using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using JF.Utils.Infrastructure.Persistence;
using JF.Utils.Application.Persistence;


namespace API_JF_Data_Utils_Example.DataAccess.Repositories
{
    public class SalonRepository : JFRepository<Salon>, ISalonRepository
    {
        public SalonRepository(IUnitOfWork context) : base(context)
        {
        }

        public override bool ValidateModel(Salon salon)
        {
            if (salon.TeacherId != null)
            {
                IReadRepository<Teacher> tr = UnitOfWork.ReadRepository<Teacher>()!;
                Teacher? t = tr.GetById(salon.TeacherId.Value);
                if (t == null) return false;
            }
            return true;
        }
    }
}
