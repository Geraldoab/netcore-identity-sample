using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace UserAPI.Infra.Data.Mapping.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
