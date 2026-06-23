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

<<<<<<< HEAD
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(desarrollo) });
builder.Services.AddScoped<IRepositorio, Repositorio>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<LineaService>();
builder.Services.AddScoped<OperacionService>();
builder.Services.AddScoped<TipoMaquinaService>();
builder.Services.AddScoped<HabilidadEmpleadoService>();
builder.Services.AddScoped<PrendaService>();
builder.Services.AddScoped<PlanificadorService>();
=======
    builder.Services.AddMudServices();
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(desarrollo) });
    builder.Services.AddScoped<IRepositorio, Repositorio>();
    builder.Services.AddScoped<EmpleadoService>();
    builder.Services.AddScoped<LineaService>();
    builder.Services.AddScoped<OperacionService>();
    builder.Services.AddScoped<TipoMaquinaService>();
>>>>>>> 0714f1903d86d49c9eec2ac01ac00a4d94075168

    await builder.Build().RunAsync();
