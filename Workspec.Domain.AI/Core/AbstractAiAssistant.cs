using Microsoft.AspNetCore.SignalR;
using OpenAI.Chat;

namespace Workspec.Domain.AI.Core;

public abstract class AbstractAiAssistant(ChatClient chatClient)
{
    protected ChatClient chatClient = chatClient;

    protected ChatCompletionOptions BuildChatCompletionOptions(params ChatTool[] tools)
    {
        ChatCompletionOptions chatCompletionOptions = new();
        foreach (var tool in tools)
        {
            chatCompletionOptions.Tools.Add(tool);
        }
        return chatCompletionOptions;
    }
}
