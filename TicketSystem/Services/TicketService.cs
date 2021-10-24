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
    public interface ITicketService
    {
        IEnumerable<TicketData> Get();
        TicketData Get(int id);
        TicketData Create(TicketData book);
        int Update(TicketData book);
        int Delete(int id);
    }
    public class TicketService : ITicketService
    {
        private readonly string _connectionString;
        public TicketService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConn");

        }

        public TicketData Create(TicketData book)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TicketData> Get()
        {
            var sqlQuery = "SELECT * FROM [TicketData]";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TicketData>(sqlQuery);
            }
            throw new NotImplementedException();
        }

        public TicketData Get(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(TicketData book)
        {
            throw new NotImplementedException();
        }
    }
}
