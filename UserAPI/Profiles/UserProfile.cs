using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace UserAPI.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
