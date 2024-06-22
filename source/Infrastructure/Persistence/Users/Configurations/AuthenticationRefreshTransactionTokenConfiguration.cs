namespace LeagueBoss.Infrastructure.Persistence.Users.Configurations;

using Domain.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuthenticationRefreshTransactionTokenConfiguration : IEntityTypeConfiguration<AuthenticationRefreshTransactionToken>
{
    public void Configure(EntityTypeBuilder<AuthenticationRefreshTransactionToken> builder)
    {
        builder.ToTable("TransactionTokens", "users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasDefaultValueSql();
        
        builder.HasDiscriminator<string>("TokenType")
            .HasValue<AuthenticationRefreshTransactionToken>(nameof(AuthenticationRefreshTransactionToken))
            .IsComplete(false);
    }
}