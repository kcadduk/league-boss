namespace LeagueBoss.Infrastructure.Persistence.Users;

using System.Reflection;
using Application.Users;
using Domain.Authentication;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

internal class UsersDbContext : LeagueBossDbContext, IUsersDbContext
{
    public UsersDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<AuthenticationRefreshTransactionToken> AuthenticationRefreshTransactionTokens =>
        Set<AuthenticationRefreshTransactionToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            type => type.Namespace == "LeagueBoss.Infrastructure.Persistence.Users.Configurations");
    }
}