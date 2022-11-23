using AutoMapper;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class SportService : ISportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SportService(IUnitOfWork unitOfWork,
                IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sport>> GetAllAsync()
        {
            return await _unitOfWork.SportRepository.GetAllAsync();
        }

        public async Task<Sport?> GetByIdAsync(int id)
        {
            return await _unitOfWork.SportRepository.GetByIdAsync(id);
        }

        public async Task<Sport?> GetByNameAsync(string sport)
        {
            return await _unitOfWork.SportRepository.GetByNameAsync(sport);
        }

        public async Task<bool> CreateSportAsync(CreateSportDTO sportDTO)
        {
            var sportExists = await GetByNameAsync(sportDTO.Name) != null;

            if (sportExists)
            {
                return false;
            }

            var sport = new Sport()
            {
                Name = sportDTO.Name,
                Description = sportDTO.Description
            };

            await _unitOfWork.SportRepository.AddSportAsync(sport);

            return true;
        }

        public async Task<bool> EditSportAsync(CreateSportDTO sportDTO)
        {
            var sport = await _unitOfWork.SportRepository.GetByNameAsync(sportDTO.Name);

            if (sport == null)
            {
                return false;
            }

            sport.Name = sportDTO.Name;
            sport.Description = sportDTO.Description;

            _unitOfWork.SportRepository.UpdateSport(sport);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteSportAsync(int id)
        {
            var removed = await _unitOfWork.SportRepository.DeleteSportAsync(id);
            return removed;
        }
    }
}
