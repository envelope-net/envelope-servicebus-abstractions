using Envelope.Enums;
using Envelope.ServiceBus.Messages;
using Envelope.Trace;

namespace Envelope.ServiceBus.Serializers.JsonConverters.Model;

internal class DeserializedJobMessage : IJobMessage
{
	public Guid Id { get; set; }
	public int JobMessageTypeId { get; set; }
	public DateTime CreatedUtc { get; set; }
	public DateTime LastUpdatedUtc { get; set; }
	public ITraceInfo CreatedTraceInfo { get; set; }
	public ITraceInfo LastUpdatedTraceInfo { get; set; }
	public Guid TaskCorrelationId { get; set; }
	public int Priority { get; set; }
	public DateTime? TimeToLive { get; set; }
	public DateTime? DeletedUtc { get; set; }
	public int RetryCount { get; set; }
	public DateTime? DelayedToUtc { get; set; }
	public TimeSpan? LastDelay { get; set; }
	public int Status { get; set; }
	public DateTime? LastResumedUtc { get; set; }

	public string? EntityName { get; set; }
	public Guid? EntityId { get; set; }
	public Dictionary<string, object?>? Properties { get; set; }
	public string? Detail { get; set; }
	public bool IsDetailJson { get; set; }

	public object? GetProperty(string key, object? defaultValue = default)
	{
		if (Properties == null)
			return defaultValue;

		if (Properties.TryGetValue(key, out var value))
			return value;

		return defaultValue;
	}

	public T? GetProperty<T>(string key, T? defaultValue = default)
	{
		if (Properties == null)
			return defaultValue;

		if (Properties.TryGetValue(key, out var value))
			return (T)value;

		return defaultValue;
	}

	public string ToJson()
#if NETSTANDARD2_0 || NETSTANDARD2_1
		=> Newtonsoft.Json.JsonConvert.SerializeObject(this);
#elif NET6_0_OR_GREATER
		=> System.Text.Json.JsonSerializer.Serialize(this);
#endif

	void IJobMessage.Archive(ITraceInfo traceInfo, Dictionary<string, object?>? properties, string? detail, bool? isDetailJson)
		=> throw new NotImplementedException();

	bool IJobMessage.CanComplete()
		=> throw new NotImplementedException();

	bool IJobMessage.CanDelete()
		=> throw new NotImplementedException();

	bool IJobMessage.CanResume()
		=> throw new NotImplementedException();

	bool IJobMessage.CanSuspend()
		=> throw new NotImplementedException();

	IJobMessage IJobMessage.Clone()
		=> throw new NotImplementedException();

	void IJobMessage.Complete(ITraceInfo traceInfo, Dictionary<string, object?>? properties, string? detail, bool? isDetailJson)
		=> throw new NotImplementedException();

	void IJobMessage.CopyFrom(IJobMessage message)
		=> throw new NotImplementedException();

	void IJobMessage.Delete(ITraceInfo traceInfo, Dictionary<string, object?>? properties, string? detail, bool? isDetailJson)
		=> throw new NotImplementedException();

	void IJobMessage.Resume(ITraceInfo traceInfo, Dictionary<string, object?>? properties, string? detail, bool? isDetailJson)
		=> throw new NotImplementedException();

	void IJobMessage.SetErrorRetry(ITraceInfo traceInfo, DateTime? delayedToUtc, TimeSpan? delay, int maxRetryCount, Dictionary<string, object?>? properties, string? detail, bool? isDetailJson)
		=> throw new NotImplementedException();

	void IJobMessage.Suspend(ITraceInfo traceInfo, Dictionary<string, object?>? properties, string? detail, bool? isDetailJson)
		=> throw new NotImplementedException();

	public override string ToString()
		=> $" {JobMessageTypeId} - {EnumHelper.ConvertIntToEnum<JobMessageStatus>(Status)}{(string.IsNullOrWhiteSpace(EntityName) ? "" : $" | {EntityName}[{EntityId}]")}";
}
