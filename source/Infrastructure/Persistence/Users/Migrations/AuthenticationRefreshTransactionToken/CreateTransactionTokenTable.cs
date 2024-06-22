namespace LeagueBoss.Infrastructure.Persistence.Users.Migrations.AuthenticationRefreshTransactionToken;

using FluentMigrator;

[Migration(202406222327)]
public class CreateTransactionTokenTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("TransactionTokens").InSchema("users")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("UserId").AsGuid().NotNullable().ForeignKey(string.Empty, "users", "Users", "Id")
            .WithColumn("Token").AsString(64).NotNullable()
            .WithColumn("Expires").AsDateTime()
            .WithColumn("TokenType").AsString(55);
        
    }
}