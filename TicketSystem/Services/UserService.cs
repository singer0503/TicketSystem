using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Entities;

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
        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConn");
        }
        public User Authenticate(string username, string password)
        {

            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
