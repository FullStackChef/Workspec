namespace Workspec.Contracts.Projects.Enum;

/// <summary>
/// Enumerates the different actions that can be taken to manage risks.
/// </summary>
public enum RiskAction
{
    Avoid = 1,              // Avoid the risk by eliminating its causes
    Exploit = 2,            // Exploit the positive impacts of the risk
    Transfer = 3,           // Transfer or share the risk with another party
    Mitigate = 4,           // Reduce the likelihood or impact of the risk
    AcceptNoAction = 5,     // Accept the risk without taking action
    Monitor = 6             // Continuously monitor the risk for changes
}
