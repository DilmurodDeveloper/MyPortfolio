using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Services.ContactMessages;
using MyPortfolio.Shared.DTOs.ContactMessages;
using RESTFulSense.Controllers;

namespace MyPortfolio.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactMessagesController : RESTFulController
{
    private readonly IContactMessageService service;

    public ContactMessagesController(IContactMessageService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContactMessageDto dto)
    {
        var id = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id }, null);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var messages = await service.GetAllAsync();
        return Ok(messages);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await service.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}
