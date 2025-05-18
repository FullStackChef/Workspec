namespace Workspec.Domain.Modules.Projects.Enum;

/// <summary>
/// Enumerates the different classes of risks that might affect a project.
/// </summary>
public enum RiskClass
{
    Solution = 1,      // Risks related to the solution being implemented
    Timeline = 2,      // Risks affecting project timelines
    Budget = 3,        // Financial risks affecting the project budget
    Legislative = 4,   // Risks related to changes in laws or regulations
    Security = 5,      // Risks related to data security and physical security
    Resources = 6,     // Risks due to resource availability and capability
    Scope = 7          // Risks involving changes to project scope
                       // You can add additional risk classes if there are other specific areas of concern
}
