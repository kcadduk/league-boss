namespace LeagueBoss.Infrastructure.Persistence.Users.Configurations;

using Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PasswordAuthenticationConfiguration : IEntityTypeConfiguration<PasswordAuthentication>
{
    public void Configure(EntityTypeBuilder<PasswordAuthentication> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql();

        builder.ToTable("PasswordAuthentication", "users");

        builder.Property(x => x.Password)
            .HasConversion<string>(x => x.Value, s => HashedStringWithSalt.Parse(s));
    }
}