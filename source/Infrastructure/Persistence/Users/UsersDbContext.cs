namespace LeagueBoss.Infrastructure.Persistence.Users;

using Application.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

internal class UsersDbContext : DbContext, IUsersDbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }
}