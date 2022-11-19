using Coravel.Invocable;
using System.Net.NetworkInformation;
using ICMPRecorder.Domain;

namespace ICMPRecorder.Infrastructure;

public class ICMPRecordInvocable : IInvocable
{
    private readonly ILogger<ICMPRecordInvocable> _log;
    private readonly IIcmpRecordWriter _writer;
    private readonly string _address;
    private readonly Ping _ping;

    public ICMPRecordInvocable(ILogger<ICMPRecordInvocable> log, IIcmpRecordWriter writer, string address)
    {
        _log = log;
        _writer = writer;
        _address = address;
        _ping = new Ping();
    }

    public async Task Invoke()
    {
        var result = await _ping.SendPingAsync(_address, 1000);
        var status = Enum.GetName(typeof(IPStatus), result.Status) ?? "unknown";
        var point = new PingRecord
        {
            RoundTripTime = result.RoundtripTime,
            Address = result.Address.ToString(),
            Success = result.Status == IPStatus.Success,
            Status = status,
            Host = Environment.MachineName

        };

        _writer.Write(point);

        if (result.Status != IPStatus.Success)
        {
            _log.LogWarning("Ping failed for {address}: {status}", _address, status);
        }
    }
}
