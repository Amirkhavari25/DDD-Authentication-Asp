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

        protected RegisterUserResponse(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static RegisterUserResponse Success()
        {
            return new RegisterUserResponse(true, string.Empty);
        }

        public static RegisterUserResponse Failure(string errorMessage)
        {
            return new RegisterUserResponse(false, errorMessage);
        }

    }
}
