using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using JF.Utils.Data.Interfaces;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface ISalonService
    {
        IEnumerable<Salon> GetAllSalons(int page, int pagesize);
        Salon? GetSalonById(int id);
        bool AddSalon(Salon salon);
        bool UpdateSalon(int id, Salon salon);
        bool DeleteSalon(int id);
    }
}
