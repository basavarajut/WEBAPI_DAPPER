using Microsoft.AspNetCore.Mvc;
using WEBAPI_DAPPER.Interfaces;
using WEBAPI_DAPPER.Models.Domain;

namespace WEBAPI_DAPPER.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private IDataService<Contact> _contactsDataService;

        public ContactsController(IDataService<Contact> dataService)
        {
            _contactsDataService = dataService;
        }

        [HttpGet("GetAllContacts")]   
        public async Task<List<Contact>> GetAllContacts()
        {
            var contacts = await _contactsDataService.GetAll();
            return contacts;
        }

        [HttpPost("SaveContact")]
        public async Task<Contact> SaveContact([FromBody] Contact contact)
        {
            var result = await _contactsDataService.PostContact(contact);
            return result;
        }
        
        [HttpPut("UpdateContact")]
        public async Task<Contact> UpdateContact([FromBody] Contact contact)
        {
            var result = await _contactsDataService.UpdateContact(contact);
            return result;
        }

        [HttpDelete("DeleteContact/{id}")]
        public async Task<bool> DeleteContact(Guid id)
        {
            var result = await _contactsDataService.DeleteContact(id);
            return result;
        }
    }
}

