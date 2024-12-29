using Core.Interface;
using Core.Models;
using Dapper;
using Infrastructure.Data;
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
            var sql = @"INSERT INTO Users (Username,Email,Mobile,Password,StatusId,CreateDate,LastModifyDate)
                        VALUES (@Username,@Email,@Mobile,@Password,1,@CreateDate,@LastModifyDate";
            var parameters = new
            {
                user.Username,
                user.Email,
                user.Mobile,
                user.Password,
                CreateDate=DateTime.Now,
                LastModifyDate = DateTime.Now,
            };
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sql,parameters);
            }
        }

        public async Task<User?> GetUserByEmail(string Email)
        {
            //get user from DB
            var query = "SELECT * FROM Users WHERE Email = @Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync(query, new { Email });
            }

        }
    }
}
