using InfluxDB.Client.Api.Domain;
using ICMPRecorder.Domain;

namespace ICMPRecorder.Infrastructure
{
    public sealed class InfluxDBRecordWriter : IIcmpRecordWriter
    {
        private readonly ILogger<InfluxDBRecordWriter> _log;
        private readonly InfluxDBService _influx;

        public InfluxDBRecordWriter(ILogger<InfluxDBRecordWriter> log, InfluxDBService influx)
        {
            _log = log;
            _influx = influx;
        }

        public void Write(PingRecord record)
        {
            _influx.Write(x => x.WriteMeasurement(record, WritePrecision.S));
        }
    }
}
