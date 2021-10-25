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
        TicketData Create(TicketData book);
        int Update(TicketData book);
        int Delete(int id);
        int Resolve(int id);
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

        public IEnumerable<TicketData> Get()
        {
            var sqlQuery = "SELECT * FROM [TicketData]";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TicketData>(sqlQuery);
            }
            throw new NotImplementedException();
        }



        public int Update(TicketData ticketData)
        {
            if (String.IsNullOrEmpty(ticketData.Summary) || String.IsNullOrEmpty(ticketData.Description) || ticketData.Id ==0)
            {
                return 0;
            }
            var sqlQuery = "UPDATE [TicketData] SET Summary=@Summary, Description=@Description WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(sqlQuery, new
                {

                    ticketData.Summary,
                    ticketData.Description,
                    ticketData.Id,
                });
            }
            throw new NotImplementedException();
        }
        public int Delete(int id)
        {
            var sqlQuery = "DELETE from [TicketData] WHERE Id=@id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(sqlQuery, new { Id = id });
            }
            throw new NotImplementedException();
        }

        public int Resolve(int id)
        {
            if (id == 0)
            {
                return 0;
            }
            var sqlQuery = "UPDATE [TicketData] SET Status='Resolve' WHERE Id=@Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(sqlQuery, new{ id });
            }
            throw new NotImplementedException();
        }
    }
}
