namespace Bakery.Core.Entities;

public class Client
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }

    public Client(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Client name is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Client email is required.");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        IsActive = true;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new InvalidOperationException("Client is already deactivated.");

        IsActive = false;
    }
}

