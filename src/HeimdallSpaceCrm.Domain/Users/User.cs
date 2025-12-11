namespace HeimdallSpaceCrm.Domain.Users;

public class User
{
    private User() { }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public static User Create(Guid id, string name, string email, string passwordHash)
    {
        return new User
        {
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
        };
    }
    
    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
}