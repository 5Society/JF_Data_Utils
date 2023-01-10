using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using JF.Utils.Data;
using JF.Utils.Data.Interfaces;

namespace API_JF_Data_Utils_Example.DataAccess.Repositories
{
    public class SalonRepository : JFRepositoryBase<Salon>, ISalonRepository
    {
        public SalonRepository(IUnitOfWork context) : base(context)
        {
        }

        public override bool ValidateEntityModel(Salon salon)
        {
            if (salon.TeacherId != null)
            {
                IReadRepositoryBase<Teacher> tr = UnitOfWork.ReadRepository<Teacher>()!;
                Teacher? t = tr.GetById(salon.TeacherId.Value);
                if (t == null) return false;
            }
            return true;
        }
    }
}
