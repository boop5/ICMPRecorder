using Coravel.Invocable;
using ICMPRecorder.Domain;
using System.Net.NetworkInformation;

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
        var record = new PingRecord
        {
            RoundTripTime = result.RoundtripTime,
            Address = result.Address.ToString(),
            Success = result.Status == IPStatus.Success,
            Status = status,
            Host = Environment.MachineName
        };

        _writer.Write(record);

        if (result.Status == IPStatus.Success)
        {
            _log.LogDebug("[{timestamp}] RTT {ip}: {rtt}ms", DateTime.Now.ToString("HH:mm:ss"), record.Address, record.RoundTripTime);
        }
        else
        {
            _log.LogWarning("Ping failed for {address}: {status}", _address, status);
        }
    }
}
