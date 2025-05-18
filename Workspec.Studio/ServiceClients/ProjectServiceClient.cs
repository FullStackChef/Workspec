using Workspec.Contracts.Projects.Models;
using Workspec.Contracts.Queries;
using System.Reactive.Subjects;

namespace Workspec.Studio.ServiceClients;

public interface IProjectServiceClient
{
    IObservable<IReadOnlyList<ProjectQueryModel>> Projects { get; }

    Task<IReadOnlyList<ProjectQueryModel>> RefreshAsync();
    Task<ProjectQueryModel?> GetByPathAsync(string path);
    Task<ProjectRecord> CreateAsync(CreateProjectRecord record);
    Task UpdateAsync(int projectId, ProjectUpdateRecord record);
}
public class ProjectServiceClient(HttpClient http) : IProjectServiceClient
{
    private readonly BehaviorSubject<IReadOnlyList<ProjectQueryModel>> _projects = new([]);
    public IObservable<IReadOnlyList<ProjectQueryModel>> Projects => _projects;

    public async Task<IReadOnlyList<ProjectQueryModel>> RefreshAsync()
    {
        var result = await http.GetFromJsonAsync<OdataQueryResponse<ProjectQueryModel>>(
            "/odata/Projects?$select=Id,Name,Path");
        _projects.OnNext(result?.Value ?? []);
        return result?.Value ?? [];
    }

    public async Task<ProjectQueryModel?> GetByPathAsync(string path)
    {
        var expandQuery = "$expand=Product($expand=Modules($expand=Features($expand=UserRequirements)))";
        var result = await http.GetFromJsonAsync<ProjectQueryModel>(
            $"/odata/Projects(Path='{path}')?{expandQuery}");

        return result;
    }


    public async Task<ProjectRecord> CreateAsync(CreateProjectRecord record)
    {
        var response = await http.PostAsJsonAsync("/projects", record);
        response.EnsureSuccessStatusCode();

        var created = await response.Content.ReadFromJsonAsync<ProjectRecord>()
            ?? throw new InvalidOperationException("Empty result from project create.");

        await RefreshAsync(); // keep the observable in sync

        return created;
    }

    public async Task UpdateAsync(int projectId, ProjectUpdateRecord record)
    {
        var response = await http.PutAsJsonAsync($"/projects/{projectId}", record);
        response.EnsureSuccessStatusCode();

        await RefreshAsync(); // rehydrate
    }
}
