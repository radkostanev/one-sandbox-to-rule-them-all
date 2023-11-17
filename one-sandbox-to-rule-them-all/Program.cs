using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using one_sandbox_to_rule_them_all.Client.Pages;
using one_sandbox_to_rule_them_all.Components;
using Playground.Shared.DataAccess;
using Playground.Shared.DbServices;
using Playground.Shared.RemoteServices;
using Playground.Shared.Services.FileManager;
using Playground.Shared.Services;
using Telerik.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(o => o.MaximumReceiveMessageSize = 4 * 1024 * 1024)
    .AddInteractiveWebAssemblyComponents();

var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{builder.Environment.ContentRootPath}/adventure-works.db" };
var connectionString = connectionStringBuilder.ToString();
var connection = new SqliteConnection(connectionString);
builder.Services.AddDbContext<AdventureContext>(options => options.UseSqlite(connection));

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<AppointmentService>();
builder.Services.AddSingleton<RecurringAppointmentService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddTransient<PersonService>();
builder.Services.AddSingleton<TreeListService>();
builder.Services.AddScoped<DbProductService>();
builder.Services.AddSingleton<ResourceService>();
builder.Services.AddSingleton<AppData>();
builder.Services.AddScoped<LocalStorage>();
builder.Services.AddSingleton<IUploadService, UploadService>();
builder.Services.AddSingleton<FlatFileService>();
builder.Services.AddSingleton<HierarchicalFileService>();
builder.Services.AddSingleton<BaseAppUrlsService>();
builder.Services.AddSingleton<VirtualColumnDataService>();
builder.Services.AddSingleton<StockDataService>();
builder.Services.AddTransient<TreeViewFlatDataService>();
builder.Services.AddTransient<TreeViewHierarchicalDataService>();
builder.Services.AddTransient<TreeViewObservableFlatDataService>();
builder.Services.AddTransient<TreeViewObservableHierarchicalDataService>();

builder.Services.AddTelerikBlazor();

builder.Services.AddSingleton(typeof(ITelerikStringLocalizer), typeof(CustomStringLocalizer));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseWebAssemblyDebugging();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSession();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Client_Page).Assembly);

app.Run();
