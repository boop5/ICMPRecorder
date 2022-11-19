using InfluxDB.Client.Core;

namespace ICMPRecorder.Domain;

[Measurement("ping")]
public class PingRecord
{
    [Column("Address", IsTag = true)]
    public string Address { get; init; }

    [Column("Host", IsTag = true)]
    public string Host { get; init; }

    [Column("RoundTripTime")]
    public long RoundTripTime { get; init; }

    [Column("Success")]
    public bool Success { get; init; }

    [Column("Status")]
    public string Status { get; init; }

    [Column("TTL")]
    public int TTL { get; init; }
}