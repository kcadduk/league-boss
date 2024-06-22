namespace LeagueBoss.Application.Users;

using Domain.Users;
using Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static async Task<bool> EmailExists(this IUsersDbContext usersDbContext, EmailAddress emailAddress)
    {
        return await usersDbContext.Users.AnyAsync(x => x.EmailAddress == emailAddress);
    }
}