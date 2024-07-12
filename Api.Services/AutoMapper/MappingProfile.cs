using Api.Domain.DataTransfer.Payload.Author;
using Api.Domain.DataTransfer.Payload.Gender;
using Api.Domain.DataTransfer.Payload.UserPayloads;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.Services.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserPayload, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<GenderPayload, Gender>();

            CreateMap<AuthorPayload, Author>();
        }
    }
}
