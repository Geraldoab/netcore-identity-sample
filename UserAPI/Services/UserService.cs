using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserAPI.Data.Dtos;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            this._mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task AddAsync(CreateUserDto dto)
        {
             User user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            
            if (!result.Succeeded)
            {
                throw new ApplicationException("Error");
            }
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if(!result.Succeeded)
            {
                throw new ApplicationException("Cannot authenticate the user");
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
