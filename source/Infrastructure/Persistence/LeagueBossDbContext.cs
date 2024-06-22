namespace LeagueBoss.Infrastructure.Persistence;

using System.Reflection;
using Microsoft.EntityFrameworkCore;

internal class LeagueBossDbContext : DbContext
{
    private static IEnumerable<Type> ConverterTypes { get; } = (from t in Assembly.Load("LeagueBoss.Domain").GetTypes()
        where t.Name == "EfCoreValueConverter" && t.DeclaringType is not null
        select t);
    
    private protected LeagueBossDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) {}

    protected sealed override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        foreach (var converterType in ConverterTypes)
        {
            configurationBuilder.Properties(converterType.DeclaringType!)
                .HaveConversion(converterType);

            configurationBuilder.DefaultTypeMapping(converterType.DeclaringType!)
                .HasConversion(converterType);
        }
    }
}