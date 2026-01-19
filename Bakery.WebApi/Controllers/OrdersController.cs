using Bakery.Core.Services;
using Bakery.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.WebApi.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = await _orderService.CreateAsync(request.ClientId);

        return CreatedAtAction(nameof(GetOrdersByClient),
            new { clientId = request.ClientId },
            new { orderId = order.Id });
    }

    [HttpPost("{orderId}/items")]
    public async Task<IActionResult> AddItem(
        Guid orderId,
        [FromBody] AddOrderItemRequest request)
    {
        try
        {
            await _orderService.AddItemAsync(
                orderId,
                request.ProductId,
                request.Price,
                request.Quantity);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{orderId}/confirm")]
    public async Task<IActionResult> Confirm(Guid orderId)
    {
        try
        {
            await _orderService.ConfirmAsync(orderId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("/clients/{clientId}/orders")]
    public async Task<IActionResult> GetOrdersByClient(Guid clientId)
    {
        var orders = await _orderService.GetByClientAsync(clientId);

        var result = orders.Select(o => new OrderDto
        {
            Id = o.Id,
            ClientId = o.ClientId,
            CreatedAt = o.CreatedAt,
            Status = o.Status.ToString(),
            TotalAmount = o.GetTotalAmount(),
            Items = o.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Price = i.Price,
                Quantity = i.Quantity
            })
        });

        return Ok(result);
    }
}
