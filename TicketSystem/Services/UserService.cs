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
        User Create(User user);
        int Update(User user);
        int Delete(int id);
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
        public User Create(User user)
        {
            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Role) 
                || String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.LastName))
            {
                return null;
            }
            var sqlQuery = "INSERT INTO [User] VALUES (@Username, @Password, @Role, @FirstName, @LastName)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sqlQuery, new
                {
                    user.Username,
                    user.Password,
                    user.Role,
                    user.FirstName,
                    user.LastName
                });
                return user;
            }
            throw new NotImplementedException();
        }

        public int Update(User user)
        {
            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Role)
                || String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.LastName) || user.Id ==0)
            {
                return 0;
            }
            var sqlQuery = "UPDATE [User] SET Username=@Username, Password=@Password, Role=@Role, FirstName=@FirstName, LastName=@LastName WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(sqlQuery, new
                {
                    user.Username,
                    user.Password,
                    user.Role,
                    user.FirstName,
                    user.LastName,
                    user.Id
                });
            }
        }

        public int Delete(int id)
        {
            var sqlQuery = "DELETE from [User] WHERE Id=@id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(sqlQuery, new { Id = id });
            }
            throw new NotImplementedException();
        }

    }
}
