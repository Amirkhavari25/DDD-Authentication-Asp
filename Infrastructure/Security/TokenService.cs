using Application.DTOs;
using Application.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class TokenService : ITokenService
    {
        private readonly string _publicKeyPath;
        private readonly string _privateKeyPath;

        public TokenService()
        {
            _publicKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Src/Infrastructure/RSAKeys/tokenPublic.key");
            _privateKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Src/Infrastructure/RSAKeys/tokenPrivate.key");
        }
        public Task<string> CreateToken(TokenPayload payload)
        {
            // Load the RSA private key
            var rsa = RSA.Create();
            var privateKey = File.ReadAllText(_privateKeyPath);
            rsa.ImportFromPem(privateKey);

            // Create signing credentials using the RSA private key
            var signingCredentials = new SigningCredentials(
                new RsaSecurityKey(rsa),
                SecurityAlgorithms.RsaSha256
            );

            // Create a JwtSecurityTokenHandler instance
            var tokenHandler = new JwtSecurityTokenHandler();

            // Define the claims for the payload
            var claims = new[]
            {
              new System.Security.Claims.Claim("Name", payload.Name),
              new System.Security.Claims.Claim("Lastname", payload.Lastname),
              new System.Security.Claims.Claim("Username", payload.Username),
              new System.Security.Claims.Claim("Createdate", payload.CreateDate.ToString("o")),
              new System.Security.Claims.Claim("Expiredate", payload.ExpireDate.ToString("o"))
            };

            // Create a security token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = payload.ExpireDate,
                SigningCredentials = signingCredentials
            };

            // Create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serialize the token to a string
            var tokenString = tokenHandler.WriteToken(token);

            // Return the token as a Task result
            return Task.FromResult(tokenString);
        }


    }
}
