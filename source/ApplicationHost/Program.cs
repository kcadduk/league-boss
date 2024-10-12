var builder = DistributedApplication.CreateBuilder(args);

var cosmos = builder.AddAzureCosmosDB("CosmoDb");
var cosmosdb = cosmos.AddDatabase("LeagueBossDb")
    .RunAsEmulator(o =>
    {
        o.WithHttpEndpoint(port: 8081, targetPort: 8081);
    });

var api = builder.AddProject<Projects.Api>("api")
    .WithReference(cosmosdb);

var node = builder.AddNpmApp("react", "../web-ui")
    .WithReference(api)
    .WithEnvironment("BROWSER", "none")
    .WithHttpEndpoint(port: 5173, env: "VITE_PORT", targetPort: 5174)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();


builder.Build().Run();