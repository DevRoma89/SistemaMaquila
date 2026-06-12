    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using MudBlazor.Services;
    using SistemaMaquila.Client;
    using SistemaMaquila.Client.Extensiones;
    using SistemaMaquila.Client.Servicios;

    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");

    string desarrollo = "https://localhost:7041"; 

    builder.Services.AddMudServices();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(desarrollo) });
    builder.Services.AddScoped<IRepositorio, Repositorio>();
    builder.Services.AddScoped<EmpleadoService>();
    builder.Services.AddScoped<LineaService>();
    builder.Services.AddScoped<OperacionService>();
    builder.Services.AddScoped<TipoMaquinaService>();

    await builder.Build().RunAsync();
