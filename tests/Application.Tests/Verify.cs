namespace LeagueBoss.Application.Tests;

using Argon;

public static class Verify
{
    static Verify()
    {

    }

    public static VerifyTests.VerifySettings Settings => WithDefaultSettings();

    private static VerifyTests.VerifySettings WithDefaultSettings()
    {
        var settings = new VerifyTests.VerifySettings();
        
        settings.UseDirectory("Snapshots");
        settings.AddExtraSettings(s =>
        {
            s.DefaultValueHandling = DefaultValueHandling.Include;
            s.NullValueHandling = NullValueHandling.Include;
        });
        
        return settings;
    }
}