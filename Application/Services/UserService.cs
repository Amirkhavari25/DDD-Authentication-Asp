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
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUser DTO)
        {
            //check if user already exist
            var ExistUser = _userRepository.GetUserByEmail(DTO.Email);
            if (ExistUser != null)
            {
                return RegisterUserResponse.Failure("User already exists");
            }
            var User = _mapper.Map<User>(DTO);
            //hash password

            //create token 

            //map to resposne and fill datas
            await _userRepository.AddUserAsync(User);
            return RegisterUserResponse.Success();
        }
    }
}
