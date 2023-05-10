using AutoMapper;
using InternshipTest.Domain.UserAggregate;
using IntertnshipTest.DAL.Entities;

namespace InternshipTest.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserGroupEntity, UserGroup>().ReverseMap();
            CreateMap<UserStateEntity, UserState>().ReverseMap();
            CreateMap<UserEntity, User>();
        }
    }
}
