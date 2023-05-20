using Envelope.ServiceBus.Hosts;
using Envelope.ServiceBus.Queries;

namespace Envelope.ServiceBus.Serializers.JsonConverters.Model;

internal class DeserializedHost : IDbHost
{
	public Guid HostId { get; set; }
	public IHostInfo HostInfo { get; set; }
	public int HostStatus { get; set; }
	public DateTime LastUpdateUtc { get; set; }

	public string ToJson()
#if NETSTANDARD2_0 || NETSTANDARD2_1
		=> Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif NET6_0_OR_GREATER
		=> System.Text.Json.JsonSerializer.Serialize(this);
#endif

	public override string? ToString()
		=> HostInfo?.HostName ?? base.ToString();
}
