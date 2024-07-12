using Api.Data.Repository;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.DataTransfer.Answer;
using AutoMapper;
using Api.Domain.Interfaces.Representations;
using Api.Domain.DataTransfer.Payload.UserPayloads;

namespace Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _repository = new UserRepository(new AppDbContext());
            _mapper = mapper;
        }

        public async Task<IDataTransfer<User>?> CreateAsync(UserPayload payload)
        {
            try
            {
                User? user = new();
                _mapper.Map(payload, user);

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

        public async Task<User?> FetchByUsernameAsync(String email)
        {
            try
            {
                return await _repository.FetchOneByEmailAsync(email);
            }
            catch { return null; }
        }

        public async Task<ICollection<IDataTransfer<User>>> FetchAllAsync()
        {
            try
            {
                var allUsers = await _repository.FetchAllAsync();
                return allUsers.Select(UserDataTransfer.BuildFromEntity).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
