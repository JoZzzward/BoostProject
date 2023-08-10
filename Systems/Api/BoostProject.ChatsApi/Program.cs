using BoostProject.ChatsApi.Configuration;
using BoostProject.ChatsApi.Hubs;
using BoostProject.Common.Security;
using BoostProject.Data.Context;
using BoostProject.Settings;
using BoostProject.Settings.Settings;
using BoostProject.Settings.Source;
using BoostProject.Systems.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

var settings = new AppSettings(new SettingSource());

services.AddSettings();

services.AddAppLocalization();

services.AddHttpContextAccessor();

services.AddAppAutoMapper();

services.AddAppDbContext(settings);

services.AddAppHealthChecks();
services.AddAppVersioning();

services.AddAppIdentity(settings, ApiResources.BoostProjectChatsAPI);

services.AddAppSwagger(settings);

services.AddAppSignalR();

services.AddAppCors();

services.AddAppControllers();

services.AddAppAuth(settings);

services.AddValidator();

var app = builder.Build();

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
    app.UseDeveloperExceptionPage();

app.UseAppLocalization();

app.UseRouting();

app.MapHub<ChatHub>("/chathub");

app.UseAppCors();

app.UseHealthChecks();

app.UseAppAuth();
app.UseAppSwagger(settings);

app.UseControllers();

app.UseAppMiddlewares();

await DbInitializer.Execute(app.Services);

app.Run();

