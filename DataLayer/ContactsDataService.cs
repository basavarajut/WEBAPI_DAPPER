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

        public async Task<Contact> PostContact(Contact contact)
        {
            var storedProc = "dbo.usp_SaveContact";
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                using (var cmd = new SqlCommand(storedProc, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, size: 50).Value = contact.Name;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, size: 50).Value = contact.Email;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, size: 20).Value = contact.Phone;
                    cmd.Parameters.Add("@Favorite", SqlDbType.Bit).Value = contact.Favorite;

                    connection.Open();
                    var result = await cmd.ExecuteNonQueryAsync();

                    return contact;
                } 
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occurred while saving contact.", ex));
            }
        }

        public async Task<Contact> UpdateContact(Contact contact)
        {
            var storedProc = "dbo.usp_UpdateContact";
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                using (var cmd = new SqlCommand(storedProc, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = contact.Id;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, size: 50).Value = contact.Name;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, size: 50).Value = contact.Email;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, size: 20).Value = contact.Phone;
                    cmd.Parameters.Add("@Favorite", SqlDbType.Bit).Value = contact.Favorite;
                    connection.Open();
                    var result = await cmd.ExecuteNonQueryAsync();
                    return contact;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occurred while updating contact.", ex));
            }
        }

        public Task<bool> DeleteContact(Guid id)
        {
            var storedProc = "dbo.usp_DeleteContact";
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                using (var cmd = new SqlCommand(storedProc, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;
                    connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Error occurred while deleting contact.", ex));
            }
        }


        private string GetConnectionString()
        {
            return _configuration["ConnectionStrings:DefaultConnection"];
        }

       
    }
}
