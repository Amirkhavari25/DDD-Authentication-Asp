using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RegisterUserResponse
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Token { get; set; }
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        protected RegisterUserResponse(bool isSuccess, string errorMessage, string? token = null, string? username = null, string? email = null, string? mobile = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Token = token;
            Username = username;
            Email = email;
            Mobile = mobile;
        }

        public static RegisterUserResponse Success(string Token, User User)
        {
            return new RegisterUserResponse(true, string.Empty, Token, User.Username, User.Email, User.Mobile);
        }

        public static RegisterUserResponse Failure(string errorMessage)
        {
            return new RegisterUserResponse(false, errorMessage);
        }

    }
}
