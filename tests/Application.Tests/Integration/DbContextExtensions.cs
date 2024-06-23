namespace LeagueBoss.Application.Tests.Integration;

using Application.Users;
using Domain.Authentication;
using Domain.Users;

public static class DbContextExtensions
{
    public static async Task<User> CreateUser(this IUsersDbContext usersDbContext, string name = "test", string? alias = null, string email = "exists@localhost")
    {
        var userName = UserName.Create(name, alias);
        var emailAddress = EmailAddress.Create(email);
        var user = User.Create(userName, emailAddress);
        await usersDbContext.AddAsync(user);
        await usersDbContext.SaveChangesAsync();
        return user;
    }

    public static async Task<AuthenticationRefreshTransactionToken> CreateAuthenticationRefreshTransactionToken(
        this IUsersDbContext usersDbContext, User user, DateTime expiry, string? token = null)
    {
        token ??= Guid.NewGuid().ToString();

        var transactionToken = AuthenticationRefreshTransactionToken.Create(user.Id, expiry, token);
        await usersDbContext.AddAsync(transactionToken);
        await usersDbContext.SaveChangesAsync();

        return transactionToken;
    }
}