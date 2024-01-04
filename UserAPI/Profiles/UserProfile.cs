using AutoMapper;
using UserAPI.Data.Dtos;
using UserAPI.Models;

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
