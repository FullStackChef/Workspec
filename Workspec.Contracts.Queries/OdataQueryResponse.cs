namespace Workspec.Contracts.Queries;

public class OdataQueryResponse<T>
{
    public List<T> Value { get; set; }
}
