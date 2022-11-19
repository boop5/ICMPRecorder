var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddIcmpRecorder();
var app = builder.Build();

var configuration = app.Services.GetRequiredService<IConfiguration>();
app.Services.UseIcmpRecorder(configuration.GetSection("IPs").Get<string[]>());

app.Run();


public class InfluxDBConfiguration
{
    public string Url { get; init; }
    public string Token { get; init; }
    public string Organisation { get; init; }
    public string Bucket { get; init; }
}