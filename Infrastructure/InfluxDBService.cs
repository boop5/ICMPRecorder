using InfluxDB.Client;

namespace ICMPRecorder.Infrastructure;

public class InfluxDBService
{
    private readonly InfluxDBConfiguration _configuration;

    public InfluxDBService(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("InfluxDB").Get<InfluxDBConfiguration>();
        configuration.GetSection("InfluxDB").Bind(_configuration);
    }

    private InfluxDBClient CreateClient()
    {
        var options = InfluxDBClientOptions.Builder.CreateNew()
            .Org(_configuration.Organisation)
            .Bucket(_configuration.Bucket)
            .AuthenticateToken(_configuration.Token)
            .Url(_configuration.Url)
            .Build();
        var client = InfluxDBClientFactory.Create(options);

        return client;
    }

    public void Write(Action<WriteApi> action)
    {
        using var client = CreateClient();
        using var write = client.GetWriteApi();
        action(write);
    }

    public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
    {
        using var client = CreateClient();
        var query = client.GetQueryApi();
        return await action(query);
    }
}