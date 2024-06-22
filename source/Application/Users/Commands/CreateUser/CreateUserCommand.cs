namespace LeagueBoss.Application.Users.Commands.CreateUser;

using Domain.Users;
using MediatR;
using Results;

public record CreateUserCommand(string FullName, string? Alias, string EmailAddress) : IRequest<Result<UserId>>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserId>>
{
    private readonly IUsersDbContext _usersDbContext;

    public CreateUserCommandHandler(IUsersDbContext usersDbContext)
    {
        _usersDbContext = usersDbContext;
    }
    
    public async Task<Result<UserId>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userName = UserName.Create(request.FullName, request.Alias);

        if (!EmailAddress.TryCreate(request.EmailAddress, out var emailAddress))
        {
            return new InvalidEmailAddressException(request.EmailAddress);
        }

        if (await _usersDbContext.EmailExists(emailAddress))
        {
            return new AlreadyRegisteredException(emailAddress);
        }

        var user = User.Create(userName, emailAddress);
        
        await _usersDbContext.Users.AddAsync(user, cancellationToken);
        await _usersDbContext.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}