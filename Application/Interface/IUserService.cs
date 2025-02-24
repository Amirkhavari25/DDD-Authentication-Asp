using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<RegisterUserResponse> RegisterUser(RegisterUser DTO);
        Task<RegisterUserResponse> LoginByEmail(LoginByEmail DTO);
    }
}
