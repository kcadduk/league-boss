namespace LeagueBoss.Infrastructure.Persistence.Users.Migrations.Users;

using System.Data;
using FluentMigrator;

[Migration(2024060220018)]
public class CreateUsersTable : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Users").InSchema("users")
            .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("FullName").AsString(100)
            .WithColumn("Alias").AsString(50)
            .WithColumn("EmailAddress").AsString(256).Unique()
            .WithColumn("PasswordAuthenticationId").AsGuid()
                .ForeignKey(string.Empty, "users", "PasswordAuthentication", "Id")
                .OnDelete(Rule.Cascade)
                .Nullable()
            .WithColumn("CreatedAt").AsDateTimeOffset()
            .WithDefault(SystemMethods.CurrentUTCDateTime)
            .NotNullable()
            .WithColumn("UpdatedAt").AsDateTimeOffset()
            .Nullable();
    }
}