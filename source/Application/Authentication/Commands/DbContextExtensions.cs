namespace LeagueBoss.Application.Authentication.Commands;

using Domain.Authentication;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Time;
using Users;

public static class DbContextExtensions
{
    public static async Task<User?> GetUserFromEmailAddress(this IUsersDbContext usersDbContext,
        EmailAddress emailAddress, CancellationToken cancellationToken = default)
    {
        return await usersDbContext.Users
            .Include(x => x.PasswordAuthentication)
            .FirstOrDefaultAsync(x => x.EmailAddress == emailAddress,
                cancellationToken: cancellationToken);
    }
    
    public static async Task PruneExpiredTransactionTokens(this IUsersDbContext dbContext, ITimeProvider timeProvider)
    {
        await dbContext.AuthenticationRefreshTransactionTokens
            .Where(x => x.Expires < timeProvider.Now)
            .ExecuteDeleteAsync();
    }

    public static async Task<AuthenticationRefreshTransactionToken?> GetNonExpiredTransactionToken(
        this IUsersDbContext dbContext, string token,
        ITimeProvider timeProvider)
    {
        return await dbContext.AuthenticationRefreshTransactionTokens
            .FirstOrDefaultAsync(x =>
                x.Token == token && x.Expires > timeProvider.Now);
    }
}