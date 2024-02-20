namespace LeagueBoss.Domain.Tests.Unit;

using Argon;

public static class Verify
{
    static Verify()
    {

    }

    public static VerifySettings Settings => WithDefaultSettings();

    private static VerifySettings WithDefaultSettings()
    {
        var settings = new VerifySettings();
        
        settings.UseDirectory("Snapshots");
        settings.AddExtraSettings(s =>
        {
            s.DefaultValueHandling = DefaultValueHandling.Include;
        });
        
        return settings;
    }
}