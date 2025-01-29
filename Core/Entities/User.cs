namespace Core.Entities;

public sealed class User : BaseEntity
{
    public string? Email {  get; set; }
    public string? Name { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}
