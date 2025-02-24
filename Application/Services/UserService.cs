using Application.DTOs;
using Application.Interface;
using AutoMapper;
using Core.Interface;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordEncryptionService passwordEncryptionService, ITokenService tokenService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordEncryptionService = passwordEncryptionService;
            _tokenService = tokenService;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUser DTO)
        {
            //check if user already exist
            if (await _userRepository.GetUserByEmail(DTO.Email) != null)
            {
                return RegisterUserResponse.Failure("User already exists");
            }
            var User = _mapper.Map<User>(DTO);
            //hash password
            User.Password = await _passwordEncryptionService.EncryptPasswordAsync(DTO.Password);
            //create token
            var Payload = new TokenPayload
            {
                CreateDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(30),
                Username = DTO.Username
            };
            var Token = await _tokenService.CreateToken(Payload);
            //save user to database
            try
            {
                await _userRepository.AddUserAsync(User);
            }
            catch (Exception ex)
            {
                return RegisterUserResponse.Failure(ex.Message);
            }
            return RegisterUserResponse.Success(Token, User);
        }
        public async Task<RegisterUserResponse> LoginByEmail(LoginByEmail DTO)
        {
            var User = await _userRepository.GetUserByEmail(DTO.Email);
            if (User == null)
            {
                return RegisterUserResponse.Failure("User not found, Try to register first");
            }
            //check password 
            var CheckedPass = await _passwordEncryptionService.CheckPasswordAsync(DTO.Password, User.Password);
            if (!CheckedPass)
            {
                return RegisterUserResponse.Failure("Wrong password");
            }
            //create token 
            var Payload = new TokenPayload
            {
                CreateDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(30),
                Username = User.Username
            };
            var Token = await _tokenService.CreateToken(Payload);
            //return res
            return RegisterUserResponse.Success(Token, User);
        }
    }
}
