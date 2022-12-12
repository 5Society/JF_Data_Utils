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
            if (!_salonRepository.ValidateEntityModel(salon)) return false;
            _salonRepository.Add(salon);
            return (_salonRepository.UnitOfWork.SaveChanges()) > 0;
        }

        public bool DeleteSalon(int id)
        {
            Salon? entity = _salonRepository.GetById(id);

            if (entity is null) return false;

            _salonRepository.Delete(entity);

            return _salonRepository.UnitOfWork.SaveChanges()>0;
        }

        public IEnumerable<Salon> GetAllSalons(int page, int pagesize)
        {
            return _salonRepository.GetAll().GetPaged(page, pagesize).GetResults();
        }

        public Salon? GetSalonById(int id)
        {
            return _salonRepository.GetById(id);
        }

        public bool UpdateSalon(int id, Salon salon)
        {
            if (id != salon.Id) return false;
            if (!_salonRepository.ValidateEntityModel(salon)) return false;
            _salonRepository.Update(salon);
            return _salonRepository.UnitOfWork.SaveChanges() >0;
        }

        
    }
}
