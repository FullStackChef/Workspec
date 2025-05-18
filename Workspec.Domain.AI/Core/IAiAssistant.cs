namespace Workspec.Domain.AI.Core;

public interface IAiAssistant
{
    public Task HandleUserRequest(string userId, string message);
}
