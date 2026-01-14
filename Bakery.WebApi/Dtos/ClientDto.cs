namespace Bakery.WebApi.Dtos;

public class ClientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
