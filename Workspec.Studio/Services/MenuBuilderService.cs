using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Workspec.Contracts.Queries;
using Workspec.Studio.ServiceClients;

namespace Workspec.Studio.Services;

public class NavigationContext
{
    public string Area { get; init; } = "home";
    public string? Name { get; init; }
    public string? Path { get; init; }
    public List<MenuItem>? SecondaryItems { get; init; }
    public List<MenuItem>? TertiaryItems { get; init; }
}

public class MenuBuilderService
{
    private readonly List<ProductQueryModel> _productList = [];
    private readonly List<ProjectQueryModel> _projectList = [];
    private readonly BehaviorSubject<List<MenuItem>> _menuItems = new([]);
    private readonly IProductServiceClient _products;
    private readonly IProjectServiceClient _projects;

    public IObservable<List<MenuItem>> MenuItems => _menuItems.AsObservable();
    public NavigationContext CurrentContext { get; private set; } = new();

    public MenuBuilderService(IProductServiceClient products, IProjectServiceClient projects, NavigationManager nav)
    {
        _products = products;
        _products.Products.Subscribe(PopulateProductList);

        _projects = projects;
        _projects.Projects.Subscribe(PopulateProjectList);

        _ = Task.Run(async () =>
        {
            var path = new Uri(nav.Uri).PathAndQuery;
            var context = await ResolveContextFromUrlAsync(path);
            _menuItems.OnNext(BuildMenuFromContext(context));
        });
        nav.LocationChanged += async (s, e) =>
        {
            var path = new Uri(e.Location).PathAndQuery;
            var context = await ResolveContextFromUrlAsync(path);
            _menuItems.OnNext(BuildMenuFromContext(context));
        };
    }



    public async Task<NavigationContext> ResolveContextFromUrlAsync(string url)
    {
        var segments = url.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var area = segments.ElementAtOrDefault(0)?.ToLower() ?? "home";

        return area switch
        {
            "products" => await ResolveProductContext(segments),
            "projects" => await ResolveProjectContext(segments),
            _ => new NavigationContext { Area = area }
        };
    }

    private async ValueTask<NavigationContext> ResolveProductContext(string[] segments)
    {
        PopulateProductList(await _products.RefreshAsync());
        var productCode = segments.ElementAtOrDefault(1)?.ToLower();
        var selectedProduct = _productList.FirstOrDefault(p => p.Code.ToLower() == productCode);

        return CurrentContext = new NavigationContext
        {
            Area = "products",
            Name = selectedProduct?.Name ?? "Dashboard",
            Path = selectedProduct?.Code.ToLower(),
            SecondaryItems = _productList.Select(p => new MenuItem
            {
                Text = p.Name,
                Url = $"/products/{p.Code.ToLower()}"
            }).Append(new MenuItem { Separator = true })
              .Append(new MenuItem { Text = "Add new product", IconCss = "e-icons e-plus" })
              .ToList(),
            TertiaryItems = selectedProduct != null ? GetProductModules(selectedProduct) : null
        };
    }

    private async ValueTask<NavigationContext> ResolveProjectContext(string[] segments)
    {
        PopulateProjectList(await _projects.RefreshAsync());
        var projectPath = segments.ElementAtOrDefault(1)?.ToLower();
        var selectedProject = _projectList.FirstOrDefault(p => p.Path.ToLower() == projectPath);

        return CurrentContext = new NavigationContext
        {
            Area = "projects",
            Name = selectedProject?.Name ?? "Dashboard",
            Path = selectedProject?.Path.ToLower(),
            SecondaryItems = _projectList.Select(p => new MenuItem
            {
                Text = p.Name,
                Url = $"/projects/{p.Path.ToLower()}"
            }).Append(new MenuItem { Separator = true })
              .Append(new MenuItem { Text = "Add new project", IconCss = "e-icons e-plus", Id = "add-project" })
              .ToList(),
            TertiaryItems = selectedProject != null ? GetProjectModules(selectedProject) : null
        };
    }

    public List<MenuItem> BuildMenuFromContext(NavigationContext ctx)
    {
        var menu = new List<MenuItem>
        {
            GetAreaMenu(ctx.Area).FirstOrDefault() ?? new MenuItem { Text = "Home", Url = "/" }
        };

        if (!string.IsNullOrWhiteSpace(ctx.Name) && ctx.SecondaryItems != null)
        {
            menu.Add(new MenuItem
            {
                Text = ctx.Name,
                Items = ctx.SecondaryItems
            });
        }

        if (ctx.TertiaryItems != null && ctx.TertiaryItems.Any())
        {
            menu.AddRange(ctx.TertiaryItems);
        }

        return menu;
    }

    public List<MenuItem> GetAreaMenu(string currentArea)
    {
        var items = new List<MenuItem>
        {
            new() { Text = "Home", IconCss = "", Url = "/", Id = "home" },
            new() { Text = "Products", IconCss = "", Url = "/products", Id = "products" },
            new() { Text = "Projects", IconCss = "", Url = "/projects", Id = "projects" },
            new() { Text = "Systems", IconCss = "", Url = "/systems", Id = "systems" },
            new() { Text = "Settings", IconCss = "", Url = "/settings", Id = "settings" }
        };

        var selected = items.FirstOrDefault(i => i.Id == currentArea);
        if (selected != null)
        {
            selected.Items = items.Where(i => i != selected).ToList();
            return [selected];
        }

        return [items[0]]; // fallback to home
    }

    private List<MenuItem> GetProductModules(ProductQueryModel product) =>
        [
            new() { Text = "Modules", Items =
            [
                new() { Text = "Dashboard" },
                new() { Text = "Roles and Personas" },
                new() { Text = "Modules" },
                new() { Text = "User requirements" },
                new() { Text = "Cost management" },
                new() { Text = "Decision log" },
                new() { Text = "Risk register" },
                new() { Text = "Settings" }
            ]}
        ];

    private List<MenuItem> GetProjectModules(ProjectQueryModel project) =>
        [
            new() { Text = "User requirements", Items =
            [
                new() { Text = "Dashboard" },
                new() { Text = "User requirements", Url = $"/projects/{project.Path}/user-requirements" },
                new() { Text = "Cost management" },
                new() { Text = "Decision log" },
                new() { Text = "Risk register" },
                new() { Separator = true },
                new() { Text = "Settings" }
            ]}
        ];

    private void PopulateProjectList(IReadOnlyList<ProjectQueryModel> p)
    {
        _projectList.Clear();
        _projectList.AddRange(p);
    }

    private void PopulateProductList(IReadOnlyList<ProductQueryModel> p)
    {
        _productList.Clear();
        _productList.AddRange(p);
    }
}
