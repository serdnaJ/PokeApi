using PokeApiWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registro del cliente HTTP tipado para la PokéAPI.
// IHttpClientFactory gestiona el ciclo de vida de las conexiones y
// le entrega a PokeApiService un HttpClient ya configurado.
builder.Services.AddHttpClient<PokeApiService>(client =>
{
    var baseUrl = builder.Configuration["PokeApi:BaseUrl"] ?? "https://pokeapi.co/api/v2/";
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(15);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
