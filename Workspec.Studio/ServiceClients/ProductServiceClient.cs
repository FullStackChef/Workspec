using System.Reactive.Subjects;
using Workspec.Contracts.Queries;

namespace Workspec.Studio.ServiceClients;

public interface IProductServiceClient
{
    IObservable<IReadOnlyList<ProductQueryModel>> Products { get; }

    Task<IReadOnlyList<ProductQueryModel>> RefreshAsync();
    Task<ProductQueryModel?> GetByCodeAsync(string code);
    //Task<ProductRecord> CreateAsync(CreateProductRecord record);
    //Task UpdateAsync(int productId, ProductUpdateRecord record);
}
public class ProductServiceClient(HttpClient http) : IProductServiceClient
{
    private readonly BehaviorSubject<IReadOnlyList<ProductQueryModel>> _products = new([]);
    public IObservable<IReadOnlyList<ProductQueryModel>> Products => _products;

    public async Task<IReadOnlyList<ProductQueryModel>> RefreshAsync()
    {
        var result = await http.GetFromJsonAsync<OdataQueryResponse<ProductQueryModel>>(
            "/odata/Products?$select=Id,Name,Code");

        _products.OnNext(result?.Value ?? []);

        return result?.Value ?? [];
    }

    public async Task<ProductQueryModel?> GetByCodeAsync(string code)
    {
        var result = await http.GetFromJsonAsync<OdataQueryResponse<ProductQueryModel>>(
            $"/odata/Products(Code='{code}')?$select=Id,Name,Code");

        return result?.Value.FirstOrDefault();
    }

    //public async Task<ProductRecord> CreateAsync(CreateProductRecord record)
    //{
    //    var response = await http.PostAsJsonAsync("/products", record);
    //    response.EnsureSuccessStatusCode();

    //    var created = await response.Content.ReadFromJsonAsync<ProductRecord>()
    //        ?? throw new InvalidOperationException("Empty result from product create.");

    //    await RefreshAsync(); // update observable list
    //    return created;
    //}

    //public async Task UpdateAsync(int productId, ProductUpdateRecord record)
    //{
    //    var response = await http.PutAsJsonAsync($"/products/{productId}", record);
    //    response.EnsureSuccessStatusCode();

    //    await RefreshAsync();
    //}
}