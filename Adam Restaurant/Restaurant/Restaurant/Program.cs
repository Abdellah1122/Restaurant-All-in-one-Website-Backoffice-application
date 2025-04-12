using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Restaurant;
using Restaurant.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(); 
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>(); 
builder.Services.AddScoped<JwtTokenService>();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7065/") });


//
builder.Services.AddMudServices();
//
builder.Services.AddScoped<CategoriePlatService>();
builder.Services.AddScoped<CategorieFournisseurService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<FournisseurService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<PlatService>();
builder.Services.AddScoped<PlatTableService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<TiquetService>();
builder.Services.AddScoped<TiquetArchiveService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<ClientService>();

//
builder.Services.AddScoped<TableChoisis>();
builder.Services.AddScoped<CommandeState>();
//
builder.Services.AddScoped<CommandeCaissierService>();
builder.Services.AddScoped<CommandeService>();

await builder.Build().RunAsync();
