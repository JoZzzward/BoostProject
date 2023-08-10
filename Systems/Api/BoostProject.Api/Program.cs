using BoostProject.Api;
using BoostProject.Api.Configuration;
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

services.AddAppCors();

services.AddAppDbContext(settings);

services.AddAppHealthChecks();
services.AddAppVersioning();

services.AddAppSwagger(settings);

services.AddAppAutoMapper();

services.AddAppIdentity(settings, ApiResources.BoostProjectAPI);

services.AddAppControllers();

services.AddAppAuth(settings);

services.AddValidator();

services.RegisterAppServices();

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

app.UseAppCors();

app.UseHealthChecks();

app.UseAppAuth();
app.UseAppSwagger(settings);

app.UseControllers();

app.UseAppMiddlewares();

await DbInitializer.Execute(app.Services);

app.Run();

