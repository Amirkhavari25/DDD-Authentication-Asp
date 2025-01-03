﻿using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private readonly string _publicKeyPath;
        private readonly string _privateKeyPath;

        public PasswordEncryptionService()
        {
            _publicKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Src/Infrastructure/RSAKeys/passwordPublic.key");
            _privateKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Src/Infrastructure/RSAKeys/passwordPrivate.key");
        }

        public async Task<string> EncryptPasswordAsync(string Password)
        {
            var PrivateKey = await File.ReadAllTextAsync(_privateKeyPath);
            using (var rsa = RSA.Create())
            {
                rsa.ImportFromPem(PrivateKey.ToCharArray());
                //make password bytes
                var PasswordBytes = Encoding.UTF8.GetBytes(Password);
                //encrypye bytes password
                var EncryptedBytes = rsa.Encrypt(PasswordBytes, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(EncryptedBytes);
            }
        }

        public async Task<bool> CheckPasswordAsync(string InputPassword, string UserPassword)
        {
            try
            {
                //read public key
                var PublicKey = await File.ReadAllTextAsync(_publicKeyPath);
                using (var rsa = RSA.Create())
                {
                    rsa.ImportFromPem(PublicKey.ToCharArray());
                    //convert user password from database to bytes
                    var encryptedBytes = Convert.FromBase64String(UserPassword);
                    //decode password saved in database
                    var decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
                    // convert decoded byte to string
                    var decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
                    // check input pass with pass saved in database
                    return InputPassword == decryptedPassword;
                }
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Cryptographic error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error: {ex.Message}");
                return false;
            }
        }
    }
}
