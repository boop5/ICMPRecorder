using Coravel;
using ICMPRecorder.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIcmpRecorder(this IServiceCollection services)
    {
        services.AddSingleton<InfluxDBService>();
        services.AddSingleton<IIcmpRecordWriter, InfluxDBRecordWriter>();
        services.AddScheduler();

        return services;
    }

    public static void UseIcmpRecorder(this IServiceProvider provider, IEnumerable<string> ips)
    {
        provider.UseScheduler(scheduler =>
        {
            ips.ToList().ForEach(ip => scheduler.ScheduleWithParams<ICMPRecordInvocable>(ip).EverySecond());
        });
    }
}