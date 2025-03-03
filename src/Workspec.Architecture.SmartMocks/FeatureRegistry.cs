namespace Workspec.Architecture.SmartMocks;

/// <summary>
/// Central repository for storing all registered features.
/// </summary>
public class FeatureRegistry
{
    private readonly List<FeatureBuilder.FeatureVersionBuilder> _features = new();

    /// <summary>
    /// Registers a new feature version.
    /// </summary>
    /// <param name="feature">The feature version to register.</param>
    public void RegisterFeature(FeatureBuilder.FeatureVersionBuilder feature)
    {
        _features.Add(feature);
    }

    /// <summary>
    /// Retrieves all registered features.
    /// </summary>
    public IReadOnlyList<FeatureBuilder.FeatureVersionBuilder> Features => _features.AsReadOnly();
}
