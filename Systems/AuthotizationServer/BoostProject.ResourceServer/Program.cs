using BoostProject.ResourceServer.Configuration;
using BoostProject.Settings;
using BoostProject.Settings.Settings;
using BoostProject.Settings.Source;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

var services = builder.Services;

var settings = new AppSettings(new SettingSource());

services.AddSettings();

services.AddAppCors();

services.AddAppOpenIddict(settings);

services.AddControllers();

services.AddAppAuth();

services.AddEndpointsApiExplorer();
services.AddAppSwaggerUI(settings);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAppCors();

app.UseAppSwagger(settings);

app.UseHttpsRedirection();

app.MapControllers();
app.UseAppAuth();
app.UseAppSerilog();

app.Run();
