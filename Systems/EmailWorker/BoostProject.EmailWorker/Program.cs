using BoostProject.Api.Configuration;
using BoostProject.Data.Context;
using BoostProject.EmailWorker;
using BoostProject.EmailWorker.Configuration;
using BoostProject.EmailWorker.EmailTask;
using BoostProject.Settings;
using BoostProject.Settings.Settings;
using BoostProject.Settings.Source;

var builder = WebApplication.CreateBuilder(args);

var settings = new AppSettings(new SettingSource());

builder.AddLogger();

var services = builder.Services;

services.AddSettings();

services.AddHttpContextAccessor();
services.AddAppHealthChecks();

services.AddAppDbContext(settings);

services.AddValidator();

services.RegisterAppServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHealthChecks();

app.Services.GetRequiredService<ITaskEmailSender>().Start();

app.Run();