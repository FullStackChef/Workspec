using Newtonsoft.Json.Linq;
using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json;
using Workspec.Domain.AI.Assistants;


string OpenApikey = "";

ChatClient client = new("gpt-4o", new ApiKeyCredential(OpenApikey));

ProjectAiAssitant projectAiAssitant = new(client);

Console.WriteLine("Describe your project, and I'll generate the details for you.");
var userMessage = Console.ReadLine();

var projectDetail = await projectAiAssitant.GetAiAssistedProjectDetails(new Workspec.Domain.AI.Models.AiAssistantRequest { Message = userMessage });

Console.WriteLine("==============================");
Console.WriteLine("Generated Project Details:");
Console.WriteLine("==============================");

Console.WriteLine($"Name: {projectDetail.Name}");
Console.WriteLine($"Path: {projectDetail.Path}");
Console.WriteLine($"Product ID: {projectDetail.ProductId}");
Console.WriteLine($"Description: {projectDetail.Description}");


Console.ReadKey();