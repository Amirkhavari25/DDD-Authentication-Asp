using Core.Interface;
using Core.Models;
using Dapper;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _context;

        public UserRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            var sql = @"INSERT INTO Users (Username,Email,EmailVerify,Mobile,MobileVerify,Password,PassError,BlockCount,StatusId,CreateDate,LastModifyDate)
                        VALUES (@Username,@Email,@EmailVerify,@Mobile,@MobileVerify,@Password,@PassError,@BlockCount,1,@CreateDate,@LastModifyDate)";
            var parameters = new
            {
                user.Username,
                user.Email,
                user.EmailVerify,
                user.Mobile,
                user.MobileVerify,
                user.Password,
                user.PassError,
                user.BlockCount,
                CreateDate = DateTime.Now,
                LastModifyDate = DateTime.Now
            };
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(sql, parameters);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("IX_Users_Email"))
                {
                    throw new Exception("The email provided is already in use.");
                }
                else if (ex.Message.Contains("IX_Users_Username"))
                {
                    throw new Exception("The username provided is already in use.");
                }
                else if (ex.Message.Contains("IX_Users_Mobile"))
                {
                    throw new Exception("The mobile provided is already exist.");
                }
                else
                {
                    throw new Exception("Something went wrong by server.");
                }
            }
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            //get user from DB
            var query = "SELECT * FROM Users WHERE Email = @Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email });
            }

        }
    }
}
