var builder = DistributedApplication.CreateBuilder(args);

var workspecDb = builder.AddConnectionString("WorkspecDb");

var cae = builder.AddAzureContainerAppEnvironment("cae");

var api = builder.AddProject<Projects.Workspec_Studio_Api>("workspec-studio-api")
                 .WithReference(workspecDb)
                 .WithExternalHttpEndpoints();

builder.AddProject<Projects.Workspec_Studio>("workspec-studio")
       .WithReference(api)
       .WithExternalHttpEndpoints();

builder.Build().Run();
