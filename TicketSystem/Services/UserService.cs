using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Entities;
using TicketSystem.Helpers;

namespace TicketSystem.Services
{
    // 定義介面合約
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    // 繼承就要實作
    public class UserService : IUserService
    {
        private readonly string _connectionString;
        private readonly AppSettings _appSettings;

        //DI
        public UserService(IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _connectionString = configuration.GetConnectionString("DefaultConn");
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            var sqlQuery = "SELECT * FROM [User] where [Username] = @Username and [Password] = @Password";
            User user =new User();
            using (var connection = new SqlConnection(_connectionString))
            {
                user = connection.Query<User>(sqlQuery, new {
                    Username = username,
                    Password = password
                }).FirstOrDefault();
            }
            // 如果找不到使用者
            if (user == null)
                return null;

            // 驗證成功, 產生 jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),  // 放入 id
                    new Claim(ClaimTypes.Role, user.Role)   // 放入角色
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();

            //throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            var sqlQuery = "SELECT * FROM [User]";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<User>(sqlQuery);
            }
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            var sqlQuery = "SELECT * FROM [User] where [Id] = @Id";
            User user = new User();
            using (var connection = new SqlConnection(_connectionString))
            {
                return user = connection.Query<User>(sqlQuery, new
                {
                    Id = id
                }).FirstOrDefault();
            }
        }
    }
}
