namespace LeagueBoss.Application.Users;

using DbContext;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

public interface IUsersDbContext : IDbContext
{
    DbSet<User> Users { get; }
}