namespace LeagueBoss.Infrastructure.Persistence.Users.Migrations.Users;

using FluentMigrator;

[Migration(202406220010)]
public class CreateUserSchema : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Schema("users");
    }
}