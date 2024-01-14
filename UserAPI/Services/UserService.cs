﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserAPI.Data;
using UserAPI.Data.Dtos;
using UserAPI.Domain.Interfaces;
using UserAPI.Models;

namespace UserAPI.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailMessengerService _emailService;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager,
            TokenService tokenService, ILogger<UserService> logger, IEmailMessengerService emailService)
        {
            this._mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<CustomResult> AddAsync(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            
            if (!result.Succeeded)
            {
                return new CustomResult(result.Succeeded, errors: result.Errors.Select(s => new { s.Code, s.Description }));
            }

            _logger.LogInformation($"New user created: {dto.UserName}");

            _emailService.SendMessage(new Email()
            {
                From = dto.Email,
                To = "another@gmail.com",
                Subject = "Messenger Broker e-mail test",
                Body = "Your credentials were created successfully!"
            });

            return new CustomResult(result.Succeeded);
        }

        public async Task<CustomResult> LoginAsync(UserLoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if(!result.Succeeded)
            {
                return new CustomResult(result.Succeeded, new List<string>() { "Cannot authenticate the user" });
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.UserName.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return new CustomResult(result.Succeeded, token);
        }
    }
}
