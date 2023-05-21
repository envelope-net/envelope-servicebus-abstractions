using Envelope.ServiceBus.Jobs;
using Envelope.ServiceBus.Queries;

namespace Envelope.ServiceBus.Serializers.JsonConverters.Model;

internal class DeserializedJob : IDbJob
{
	public Guid JobInstanceId { get; set; }
	public Guid HostInstanceId { get; set; }
	public string HostName { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public bool Disabled { get; set; }
	public int Mode { get; set; }
	public TimeSpan? DelayedStart { get; set; }
	public TimeSpan? IdleTimeout { get; set; }
	public string? CronExpression { get; set; }
	public bool CronExpressionIncludeSeconds { get; set; }
	public DateTime? NextExecutionRunUtc { get; set; }
	public int Status { get; set; }
	public IReadOnlyDictionary<int, string>? JobExecutionOperations { get; set; }
	public IReadOnlyList<int>? AssociatedJobMessageTypes { get; set; }
	public int CurrentExecuteStatus { get; set; }
	public int ExecutionEstimatedTimeInSeconds { get; set; }
	public int DeclaringAsOfflineAfterMinutesOfInactivity { get; set; }
	public DateTime LastUpdateUtc { get; set; }
	public DateTime? LastExecutionStartedUtc { get; set; }

	public string ToJson()
#if NETSTANDARD2_0 || NETSTANDARD2_1
		=> Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif NET6_0_OR_GREATER
		=> System.Text.Json.JsonSerializer.Serialize(this);
#endif

	public JobStatus GetJobActivityStatus()
		=> JobStatusHelper.GetJobActivityStatus(this);

	public override string ToString()
		=> $"{Name} | {GetJobActivityStatus()}";
}
