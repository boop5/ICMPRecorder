using InfluxDB.Client.Core;

namespace ICMPRecorder.Domain;

[Measurement("ping")]
public class PingRecord
{
    [Column("Address", IsTag = true)]
    public string Address { get; set; }

    [Column("Host", IsTag = true)]
    public string Host { get; set; }

    [Column("RoundTripTime")]
    public long RoundTripTime { get; set; }

    [Column("Success")]
    public bool Success { get; set; }

    [Column("Status")]
    public string Status { get; set; }

    [Column("TTL")]
    public int TTL { get; set; }
}