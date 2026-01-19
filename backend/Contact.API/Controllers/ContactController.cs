using Contact.Application.Contracts;
using Contact.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<IActionResult> GetAll() => 
            Ok(await _contactService.GetAll());

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] int id) =>
            Ok(await _contactService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactRequest request) =>
            Ok(await _contactService.Create(request));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactRequest request) => 
            Ok(await _contactService.Update(request));

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _contactService.Delete(id);

            return Ok();
        }
    }
}
