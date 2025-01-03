using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IPasswordEncryptionService
    {
        Task<string> EncryptPasswordAsync(string Password);
        Task<bool> CheckPasswordAsync(string InputPassword, string UserPassword);
    }
}
