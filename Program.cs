var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddIcmpRecorder();
var app = builder.Build();

var configuration = app.Services.GetRequiredService<IConfiguration>();
app.Services.UseIcmpRecorder(configuration.GetSection("IPs").Get<string[]>());

app.Run();
