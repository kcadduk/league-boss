namespace LeagueBoss.Infrastructure.Persistence.Users;

using Application.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

internal class UsersDbContext : DbContext, IUsersDbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
    
    public DbSet<User> Users { get; init; }
}