using Envelope.Enums;
using Envelope.Logging;
using Envelope.ServiceBus.Hosts;
using Envelope.ServiceBus.Queries;

namespace Envelope.ServiceBus.Serializers.JsonConverters.Model;

internal class DeserializedHostLog : IDbHostLog
{
	public Guid IdLogMessage { get; set; }
	public ILogMessage LogMessage { get; set; }
	public int IdLogLevel { get; set; }
	public Guid HostId { get; set; }
	public Guid HostInstanceId { get; set; }
	public int HostStatus { get; set; }
	public DateTime CreatedUtc { get; set; }

	public string ToJson()
#if NETSTANDARD2_0 || NETSTANDARD2_1
		=> Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif NET6_0_OR_GREATER
		=> System.Text.Json.JsonSerializer.Serialize(this);
#endif

	public override string ToString()
		=> $"{nameof(HostInstanceId)} = {HostInstanceId} | {EnumHelper.ConvertIntToEnum<HostStatus>(HostStatus)}";
}
