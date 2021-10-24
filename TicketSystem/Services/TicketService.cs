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

        public TicketData Create(TicketData ticketData)
        {
            if (String.IsNullOrEmpty(ticketData.Summary) || String.IsNullOrEmpty(ticketData.Description) || String.IsNullOrEmpty(ticketData.Type)) {
                return null;
            }

            var sqlQuery = "INSERT INTO [TicketData]([Summary], [Description], [Type], [Status]) VALUES (@Summary, @Description, @Type, @Status)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sqlQuery, new
                {
                    Summary = ticketData.Summary,
                    Description = ticketData.Description,
                    Type = ticketData.Type,
                    Status = "Open"
                });

                return ticketData;
            }
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
