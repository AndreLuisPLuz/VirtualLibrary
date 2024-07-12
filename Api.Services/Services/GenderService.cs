using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.DataTransfer.Answer;
using Api.Domain.DataTransfer.Payload.Gender;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Representations;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Services.Services
{
    public class GenderService : IGenderService
    {
        private readonly BaseRepository<Gender> _repository;
        private readonly IMapper _mapper;

        public GenderService(IMapper mapper)
        {
            _repository = new BaseRepository<Gender>(new AppDbContext());
            _mapper = mapper;
        }

        async Task<IDataTransfer<Gender>?> IGenderService.CreateAsync(GenderPayload payload)
        {
            Gender? gender = new();
            _mapper.Map(payload, gender);

            if (gender is null)
                return null;

            try
            {
                Gender? newGender = await _repository.CreateAsync(gender);

                if (newGender is not null)
                    return GenderAnswer.BuildFromEntity(newGender);
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        async Task<ICollection<IDataTransfer<Gender>>> IGenderService.GetAsync()
        {
            try
            {
                var allGenders = await _repository.FetchAllAsync();
                return allGenders.Select(GenderAnswer.BuildFromEntity).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<IDataTransfer<Gender>> IGenderService.UpdateAsync(Guid id, GenderPayload payload)
        {
            try
            {
                Gender? currentGender = await _repository.FetchAsync(id);
                if (currentGender == null)
                {
                    return null;
                }

                _mapper.Map(payload, currentGender);

                Gender? updatedGender = await _repository.UpdateAsync(id, currentGender);
                if (updatedGender is not null)
                {
                    return GenderAnswer.BuildFromEntity(updatedGender);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
