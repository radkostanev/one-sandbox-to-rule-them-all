using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Playground.Shared.Services;
using Playground.Shared.Services.FileManager;
using Telerik.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddTelerikBlazor()
    // inject localization service after the call to .AddTelerikBlazor()
    .AddSingleton(typeof(ITelerikStringLocalizer), typeof(CustomStringLocalizer))
    .AddSingleton<WeatherForecastService>()
    .AddSingleton<AppointmentService>()
    .AddSingleton<RecurringAppointmentService>()
    .AddSingleton<ProductService>()
    .AddSingleton<TreeListService>()
    .AddSingleton<ResourceService>()
    .AddSingleton<AppData>()
    .AddSingleton<VirtualColumnDataService>()
    .AddSingleton<StockDataService>()
    .AddScoped<LocalStorage>()
    .AddTransient<PersonService>()
    .AddTransient<TreeViewFlatDataService>()
    .AddTransient<TreeViewHierarchicalDataService>()
    .AddTransient<TreeViewObservableFlatDataService>()
    .AddTransient<TreeViewObservableHierarchicalDataService>()
    .AddTransient<FlatFileService>()
    .AddTransient<HierarchicalFileService>();

await builder.Build().RunAsync();
