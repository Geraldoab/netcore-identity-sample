using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserAPI.Domain.Interfaces.Services;

namespace UserAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailMessengerService _emailService;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager,
            ITokenService tokenService, ILogger<UserService> logger, IEmailMessengerService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<CustomResultDto> AddAsync(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            
            if (!result.Succeeded)
            {
                return new CustomResultDto(result.Succeeded, errors: result.Errors.Select(s => new { s.Code, s.Description }));
            }

            _logger.LogInformation($"New user created: {dto.UserName}");

            _emailService.SendMessage(new Email()
            {
                From = dto.Email,
                To = "another@gmail.com",
                Subject = "Messenger Broker e-mail test",
                Body = "Your credentials were created successfully!"
            });

            return new CustomResultDto(result.Succeeded);
        }

        public async Task<CustomResultDto> LoginAsync(UserLoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if(!result.Succeeded)
            {
                return new CustomResultDto(result.Succeeded, new List<string>() { "Cannot authenticate the user" });
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return new CustomResultDto(result.Succeeded, token);
        }
    }
}
