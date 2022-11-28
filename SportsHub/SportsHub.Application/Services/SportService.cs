using AutoMapper;
using SportsHub.Api.Exceptions.CustomExceptionModels;
using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Constants;
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
            return await _unitOfWork.SportRepository.GetByIdAsync(id) ??
                throw new NotFoundException(ValidationMessages.SportNotFound); 
        }

        public async Task<Sport?> GetByNameAsync(string sport)
        {
            return await _unitOfWork.SportRepository.GetByNameAsync(sport) ??
                throw new NotFoundException(ValidationMessages.SportNotFound);
        }

        public async Task CreateSportAsync(CreateSportDTO sportDTO)
        {
            var sportExists = await _unitOfWork.SportRepository.GetByNameAsync(sportDTO.Name) != null;

            if (sportExists)
            {
                throw new BusinessLogicException(409, ValidationMessages.SportExists);
            }

            var sport = new Sport()
            {
                Name = sportDTO.Name,
                Description = sportDTO.Description
            };

            await _unitOfWork.SportRepository.AddSportAsync(sport);
        }

        public async Task EditSportAsync(CreateSportDTO sportDTO)
        {
            var sport = await _unitOfWork.SportRepository.GetByNameAsync(sportDTO.Name) ??
                throw new NotFoundException(ValidationMessages.SportNotFound);

            sport.Name = sportDTO.Name;
            sport.Description = sportDTO.Description;

            _unitOfWork.SportRepository.UpdateSport(sport);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSportAsync(int id)
        {
            var sport = await _unitOfWork.SportRepository.GetByIdAsync(id) ??
                throw new NotFoundException(ValidationMessages.SportNotFound);

            _unitOfWork.SportRepository.DeleteSport(sport);
        }
    }
}
