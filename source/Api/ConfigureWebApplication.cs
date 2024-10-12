namespace LeagueBoss.Api;

using FastEndpoints;
using FastEndpoints.Swagger;

public static class ConfigureWebApplication
{
    public static WebApplication ConfigureMiddleware(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseFastEndpoints();
        app.UseSwaggerGen();
        
        return app;
    }
}