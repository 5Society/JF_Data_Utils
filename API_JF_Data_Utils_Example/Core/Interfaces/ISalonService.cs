using API_JF_Data_Utils_Example.Core.Models;

namespace API_JF_Data_Utils_Example.Core.Interfaces
{
    public interface ISalonService
    {
        IEnumerable<Salon> GetAllSalons(int page, int pagesize);
        Salon? GetSalonById(int id);
        Task<bool> AddSalon(Salon salon);
        Task<bool> UpdateSalon(int id, Salon salon);
        Task<bool> DeleteSalon(int id);
    }
}
