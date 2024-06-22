namespace LeagueBoss.Application.Users;

using DbContext;
using Domain.Authentication;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

public interface IUsersDbContext : IDbContext
{
    DbSet<User> Users { get; }
    DbSet<AuthenticationRefreshTransactionToken> AuthenticationRefreshTransactionTokens { get; }
}