using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Models;
using MyPortfolio.API.Services;
using MyPortfolio.Shared.DTOs;
using RESTFulSense.Controllers;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : RESTFulController
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactMessageDto>> GetAll()
        {
            var messages = contactService.GetAll();

            var result = messages.Select(m => new ContactMessageDto
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Subject = m.Subject,
                Message = m.Message,
                SentAt = m.SentAt
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ContactMessageDto> GetById(int id)
        {
            var message = contactService.GetById(id);
            if (message == null) return NotFound();

            var result = new ContactMessageDto
            {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Subject = message.Subject,
                Message = message.Message,
                SentAt = message.SentAt
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(ContactRequestDto requestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var message = new ContactMessage
            {
                Name = requestDto.Name,
                Email = requestDto.Email,
                Subject = requestDto.Subject,
                Message = requestDto.Message,
                SentAt = DateTime.UtcNow
            };

            contactService.Add(message);

            var responseDto = new ContactMessageDto
            {
                Id = message.Id,
                Name = message.Name,
                Email = message.Email,
                Subject = message.Subject,
                Message = message.Message,
                SentAt = message.SentAt
            };

            return CreatedAtAction(nameof(GetById), new { id = message.Id }, responseDto);
        }

    }
}
