var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Api>("api");

var node = builder.AddNpmApp("react", "../web-ui")
    .WithReference(api)
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(port: 5173, env: "VITE_PORT", targetPort: 5174)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();