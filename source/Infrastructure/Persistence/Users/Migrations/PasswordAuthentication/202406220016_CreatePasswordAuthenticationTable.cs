namespace LeagueBoss.Infrastructure.Persistence.Users.Migrations.PasswordAuthentication;

using FluentMigrator;

[Migration(202406220016)]
public class CreatePasswordAuthenticationTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("PasswordAuthentication").InSchema("users")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("Password").AsString(100);
    }
}