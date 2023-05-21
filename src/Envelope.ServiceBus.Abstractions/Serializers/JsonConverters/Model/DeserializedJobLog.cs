using Envelope.Enums;
using Envelope.Logging;
using Envelope.ServiceBus.Jobs;
using Envelope.ServiceBus.Queries;
using Microsoft.Extensions.Logging;

namespace Envelope.ServiceBus.Serializers.JsonConverters.Model;

internal class DeserializedJobLog : IDbJobLog
{
	public Guid IdLogMessage { get; set; }
	public Guid JobInstanceId { get; set; }
	public string? Detail { get; set; }
	public Guid ExecutionId { get; set; }
	public string LogCode { get; set; }
	public ILogMessage LogMessage { get; set; }
	public int IdLogLevel { get; set; }
	public int Status { get; set; }
	public int ExecuteStatus { get; set; }
	public DateTime CreatedUtc { get; set; }
	public Guid? JobMessageId { get; set; }

	public string ToJson()
#if NETSTANDARD2_0 || NETSTANDARD2_1
		=> Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif NET6_0_OR_GREATER
		=> System.Text.Json.JsonSerializer.Serialize(this);
#endif

	public override string ToString()
		=> $" {EnumHelper.ConvertIntToEnum<LogLevel>(IdLogLevel)} | {LogCode} | {EnumHelper.ConvertIntToEnum<JobExecuteStatus>(ExecuteStatus)}";
}
