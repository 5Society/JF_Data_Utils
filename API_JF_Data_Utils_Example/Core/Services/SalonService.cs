using API_JF_Data_Utils_Example.Core.Interfaces;
using API_JF_Data_Utils_Example.Core.Models;
using API_JF_Data_Utils_Example.DataAccess.Interfaces;

namespace API_JF_Data_Utils_Example.Core.Services
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;

        public SalonService(ISalonRepository salonRepository)
        {
            salonRepository.UnitOfWork.AddRepository<Salon>(salonRepository);
            _salonRepository = (ISalonRepository)salonRepository.UnitOfWork.Repository<Salon>()!;
        }
        public async Task<bool> AddSalon(Salon salon)
        {
            return await (_salonRepository.AddAndSaveAsync(salon)) != null;
        }

        public async Task<bool> DeleteSalon(int id)
        {
            return await _salonRepository.DeleteAndSaveAsync(id);
        }

        public IEnumerable<Salon> GetAllSalons(int page, int pagesize)
        {
            return _salonRepository.GetAllPaged(page, pagesize);
        }

        public Salon? GetSalonById(int id)
        {
            return _salonRepository.GetById(id);
        }

        public async Task<bool> UpdateSalon(int id, Salon salon)
        {
            return (await _salonRepository.UpdateAndSaveAsync(id, salon))>0;
        }

        
    }
}
