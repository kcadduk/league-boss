namespace LeagueBoss.Infrastructure.Persistence.Users.Configurations;

using Domain.Authentication;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.ToTable("Users", "users");
        
        builder.OwnsOne(x => x.Name, onb =>
        {
            onb.Property(x => x.FullName).HasColumnName("FullName");
            onb.Property(x => x.Alias).HasColumnName("Alias");
        });

        builder.Property(x => x.EmailAddress)
            .HasConversion<string>(x => x.Address, s => EmailAddress.Create(s));

        builder.HasOne(x => x.PasswordAuthentication)
            .WithOne(x => x.User)
            .HasPrincipalKey<PasswordAuthentication>(x => x.Id);
    }
}