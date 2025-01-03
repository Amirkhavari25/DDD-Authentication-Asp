using Application.DTOs;
using Application.Interface;
using AutoMapper;
using Core.Interface;
using Core.Models;
using Core.Services;
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
        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordEncryptionService passwordEncryptionService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUser DTO)
        {
            //check if user already exist
            var ExistUser = await _userRepository.GetUserByEmail(DTO.Email);
            if (ExistUser != null)
            {
                return RegisterUserResponse.Failure("User already exists");
            }
            var User = _mapper.Map<User>(DTO);
            //hash password
            User.Password = await _passwordEncryptionService.EncryptPasswordAsync(DTO.Password);
            //create token 

            //map to resposne and fill datas
            await _userRepository.AddUserAsync(User);
            return RegisterUserResponse.Success();
        }
    }
}
