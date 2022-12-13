using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;
using API_JF_Data_Utils_Example.DataAccess.Repositories;
using JF.Utils.Data.Extensions;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;

        public SalonService(ISalonRepository salonRepository)
        { 
            _salonRepository = salonRepository;
        }
        public bool AddSalon(Salon salon)
        {
            return _salonRepository.AddAndSave(salon);
        }

        public bool DeleteSalon(int id)
        {
            return _salonRepository.DeleteAndSave(id);
        }

        public IEnumerable<Salon> GetAllSalons(int page, int pagesize)
        {
            return _salonRepository.GetAllPaged(page, pagesize);
        }

        public Salon? GetSalonById(int id)
        {
            return _salonRepository.GetById(id);
        }

        public bool UpdateSalon(int id, Salon salon)
        {
            return _salonRepository.UpdateAndSave(id, salon);
        }

        
    }
}
