using Envelope.Enums;
using Envelope.ServiceBus.Jobs;
using Envelope.ServiceBus.Queries;

namespace Envelope.ServiceBus.Serializers.JsonConverters.Model;

internal class DeserializedJobExecution : IDbJobExecution
{
	public Guid ExecutionId { get; set; }
	public string JobName { get; set; }
	public Guid JobInstanceId { get; set; }
	public int ExecuteStatus { get; set; }
	public DateTime StartedUtc { get; set; }
	public DateTime? FinishedUtc { get; set; }

	public string ToJson()
#if NETSTANDARD2_0 || NETSTANDARD2_1
		=> Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif NET6_0_OR_GREATER
		=> System.Text.Json.JsonSerializer.Serialize(this);
#endif

	public override string ToString()
		=> $"{JobName} | {EnumHelper.ConvertIntToEnum<JobExecuteStatus>(ExecuteStatus)}";
}
