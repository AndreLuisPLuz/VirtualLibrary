using Api.Domain.DataTransfer.Payload;
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
        }
    }
}
