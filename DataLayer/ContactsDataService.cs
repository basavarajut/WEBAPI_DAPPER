using WEBAPI_DAPPER.Interfaces;
using WEBAPI_DAPPER.Models.Domain;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace WEBAPI_DAPPER.DataLayer
{
    public class ContactsDataService : IDataService<Contact>
    {
        private readonly IConfiguration _configuration;

        public ContactsDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<List<Contact>> GetAllById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Contact>> GetAll()
        {
            var storedProc = "GetAllContacts"; 
            try
            {
                using(var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();
                    var contacts = await connection.QueryAsync<Contact>(storedProc, commandType: CommandType.StoredProcedure);
                    return contacts.ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle/Log exception
                throw(new Exception("Error occurred while fetching contacts.", ex));
            }
        }

        private string GetConnectionString()
        {
            return _configuration["ConnectionStrings:DefaultConnection"];
        }


    }
}
