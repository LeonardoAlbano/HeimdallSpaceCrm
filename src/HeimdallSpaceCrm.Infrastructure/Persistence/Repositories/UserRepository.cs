using HeimdallSpaceCrm.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HeimdallSpaceCrm.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HeimdallDbContext _db;
    
    public UserRepository(HeimdallDbContext db)
    {
        _db = db; 
    }
    
    public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task AddAsync(User user, CancellationToken ct = default)
    {
        _db.Users.Add(user);
        return Task.CompletedTask;
    }
    
    public Task SaveChangesAsync(CancellationToken ct = default)
     => _db.SaveChangesAsync(ct);
}