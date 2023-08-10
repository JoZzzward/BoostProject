using BoostProject.AuthorizationServer;
using BoostProject.AuthorizationServer.Clients;
using BoostProject.AuthorizationServer.Configuration;
using BoostProject.Data.Context;
using BoostProject.Settings;
using BoostProject.Settings.Settings;
using BoostProject.Settings.Source;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

var settings = new AppSettings(new SettingSource());

services.AddSettings();

services.AddAppHealthChecks();

services.AddAppDbContext(settings);

services.AddAppIdentity(settings);

services.AddRazorPages();
services.AddControllers();

services.AddAppAuth();

services.AddTransient<AuthService>();
services.AddTransient<ClientsSeeder>();
services.AddHttpClient();

services.AddAppCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ClientsSeeder>();
    seeder.AddClients(settings).GetAwaiter().GetResult();
    seeder.AddScopes().GetAwaiter().GetResult();
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAppCors();
app.UseAppHealthChecks();
app.UseAppSerilog();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

await DbInitializer.Execute(app.Services);

app.Run();
