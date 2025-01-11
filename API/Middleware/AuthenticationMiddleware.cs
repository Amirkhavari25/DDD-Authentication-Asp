using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace API.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _publicKeyPath;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
            _publicKeyPath = Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "RSAKeys", "tokenPublic.key");
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last()
                            ?? context.Request.Form["token"].ToString();

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Access denied. No token provided.");
                    return;
                }

                var publicKey = await System.IO.File.ReadAllTextAsync(_publicKeyPath);
                var rsa = RSA.Create();
                rsa.ImportFromPem(publicKey);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new RsaSecurityKey(rsa)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

                context.Items["User"] = principal.Claims.ToDictionary(c => c.Type, c => c.Value);

                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("JWT validation error: " + ex.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid token.");
            }
        }
    }
}
