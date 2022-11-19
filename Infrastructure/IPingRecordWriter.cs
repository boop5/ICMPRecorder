using ICMPRecorder.Domain;

namespace ICMPRecorder.Infrastructure
{
    public interface IIcmpRecordWriter
    {
        void Write(PingRecord record);
    }
}
