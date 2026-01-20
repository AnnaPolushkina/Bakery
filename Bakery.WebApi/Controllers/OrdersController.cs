using Bakery.Core.Services;
using Bakery.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.WebApi.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IClientService _clientService;
    public OrdersController(
    IOrderService orderService,
    IClientService clientService)
    {
        _orderService = orderService;
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var clientExists = await _clientService.ExistsAsync(request.ClientId);

        if (!clientExists)
            return BadRequest(new { message = "Client does not exist." });

        var order = await _orderService.CreateAsync(request.ClientId);

        return CreatedAtAction(
            nameof(GetOrderById),
            new { orderId = order.Id },
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

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);

        if (order == null)
            return NotFound(new { message = "Order not found." });

        var dto = new OrderDto
        {
            Id = order.Id,
            ClientId = order.ClientId,
            CreatedAt = order.CreatedAt,
            Status = order.Status.ToString(),
            TotalAmount = order.GetTotalAmount(),
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Price = i.Price,
                Quantity = i.Quantity
            })
        };

        return Ok(dto);
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
