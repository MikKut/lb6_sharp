using AutoMapper;
using lb6_server.Models.Dto;
using lb6_server.Models.Requests;

namespace lb6_server.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, UserDto>();
        }
    }
}
