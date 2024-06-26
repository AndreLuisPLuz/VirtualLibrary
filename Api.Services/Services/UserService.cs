using Api.Data.Repository;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services;
using Api.Domain.DataTransfer.Answer;
using Api.Domain.DataTransfer.Payload;
using AutoMapper;

namespace Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _repository = new BaseRepository<User>(new AppDbContext());
            _mapper = mapper;
        }

        public async Task<IDataTransfer<User>?> CreateAsync(User user)
        {
            try
            {
                User? newUser =  await _repository.CreateAsync(user);
                if (newUser is not null)
                {
                    return UserDataTransfer.BuildFromEntity(newUser);
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

        public async Task<IDataTransfer<User>?> UpdateAsync(Guid id, UserPayload payload)
        {
            try
            {
                User? currentUser = await _repository.FetchAsync(id);
                if (currentUser == null)
                {
                    return null;
                }

                _mapper.Map(payload, currentUser);

                User? updatedUser = await _repository.UpdateAsync(id, currentUser);
                if (updatedUser is not null)
                {
                    return UserDataTransfer.BuildFromEntity(updatedUser);
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

        public async Task<IDataTransfer<User>?> FetchAsync(Guid id)
        {
            try
            {
                User? fetchedUser = await _repository.FetchAsync(id);
                if (fetchedUser is not null)
                {
                    return UserDataTransfer.BuildFromEntity(fetchedUser);
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

        public async Task<ICollection<IDataTransfer<User>>> FetchAllAsync()
        {
            try
            {
                var allUsers = await _repository.FetchAllAsync();
                return allUsers.Select(user =>
                    UserDataTransfer.BuildFromEntity(user)).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
