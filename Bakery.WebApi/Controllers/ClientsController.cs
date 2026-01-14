using Bakery.Core.Services;
using Bakery.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.WebApi.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _clientService.GetAllAsync();

        var result = clients.Select(c => new ClientDto
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest request)
    {
        try
        {
            var client = await _clientService.CreateAsync(request.Name, request.Email);

            var dto = new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email
            };

            return CreatedAtAction(nameof(GetClients), dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/deactivate")]
    public async Task<IActionResult> DeactivateClient(Guid id)
    {
        try
        {
            await _clientService.DeactivateAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

