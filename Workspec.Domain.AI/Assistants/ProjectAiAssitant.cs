using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json;
using Workspec.Domain.AI.Core;
using Workspec.Domain.AI.Models;
using Workspec.Domain.Modules.Projects.Models;

namespace Workspec.Domain.AI.Assistants;

public interface IProjectAiAssitant
{
    ValueTask<ProjectRecord> GetAiAssistedProjectDetails(AiAssistantRequest request, CancellationToken cancellationToken = default);
}

public class ProjectAiAssitant(ChatClient chatClient) : AbstractAiAssistant(chatClient), IProjectAiAssitant
{
    public static readonly ChatTool generateProjectDetails = ChatTool.CreateFunctionTool(
        functionName: nameof(generateProjectDetails),
        functionDescription: "Generate project details",
        functionParameters: new FunctionParameters()
            .WithProperties(
                ("name", new FunctionProperty("string", "The name of the project should be consise but describe the project not just the product")),
                ("path", new FunctionProperty("string", "A short code of the project, should be lowercase alphanumeric only e.g prj1")),
                ("productId", new FunctionProperty("integer", "The product ID of the project")),
                ("description", new FunctionProperty("string", "The description of the project, enriched to provide a fuller description"))
            )
            .WithRequired("name", "path", "productId", "description").ToBinaryData()
    );
    public async ValueTask<ProjectRecord> GetAiAssistedProjectDetails(AiAssistantRequest request, CancellationToken cancellationToken = default)
    {

        ChatCompletionOptions options = BuildChatCompletionOptions(generateProjectDetails);
        ICollection<ChatMessage> messages = [
            new SystemChatMessage("Generate project details with enriched description based on user provided description"),
            new SystemChatMessage("The name of the project should be consise but describe the project not just the product"),
            new SystemChatMessage("The path is a short code of the project, should be lowercase alphanumeric only e.g prj1"),
            new SystemChatMessage($"Product data: {GetProductData()}"),
            new UserChatMessage(request.Message)
        ];

        bool requiresAction;
        do
        {
            requiresAction = false;
            ClientResult<ChatCompletion> result = await chatClient.CompleteChatAsync(messages, options, cancellationToken).ConfigureAwait(false);

            switch (result.Value.FinishReason)
            {
                case ChatFinishReason.ToolCalls:
                    foreach (var toolCall in result.Value.ToolCalls)
                    {
                        switch (toolCall.FunctionName)
                        {
                            case nameof(generateProjectDetails):
                                requiresAction = false;
                                ProjectRecord? projectRecord = JsonSerializer.Deserialize<ProjectRecord>(toolCall.FunctionArguments, new JsonSerializerOptions
                                {
                                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                });
                                return projectRecord ?? throw new InvalidOperationException("Failed to generate project details");
                            default:
                                throw new InvalidOperationException($"Unknown tool call: {toolCall.FunctionName}");
                        }
                    }
                    break;
                case ChatFinishReason.Length:
                    throw new NotImplementedException("Incomplete model output due to MaxTokens parameter or token limit exceeded.");

                case ChatFinishReason.ContentFilter:
                    throw new NotImplementedException("Omitted content due to a content filter flag.");

                case ChatFinishReason.FunctionCall:
                    throw new NotImplementedException("Deprecated in favor of tool calls.");

                default:
                    throw new NotImplementedException(result.Value.FinishReason.ToString());
            }
        }
        while (requiresAction);

        throw new InvalidOperationException("Failed to generate project details");
    }

    private static string GetProductData()
    {
        Dictionary<string, int> toolData = new()
                                {
                                    {"workspec", 1 },
                                    {"addressible health",2 },
                                    {"clutch",3 },
                                };
        string toolResult = JsonSerializer.Serialize(toolData);
        return toolResult;
    }
}
