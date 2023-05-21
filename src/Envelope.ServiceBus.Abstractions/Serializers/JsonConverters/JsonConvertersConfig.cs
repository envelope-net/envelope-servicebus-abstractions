#if NET6_0_OR_GREATER
using Envelope.Extensions;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public static class JsonConvertersConfig
{
	public static void AddServiceBusReadConverters(IList<JsonConverter> converters)
	{
		if (converters == null)
			throw new ArgumentNullException(nameof(converters));

		Services.Serializers.JsonConverters.JsonConvertersConfig.AddServiceReadConverters(converters);
		converters.AddUniqueItem(new HostInfoJsonConverter());
		converters.AddUniqueItem(new DbHostJsonConverter());
		converters.AddUniqueItem(new DbHostLogJsonConverter());
		converters.AddUniqueItem(new DbJobExecutionJsonConverter());
		converters.AddUniqueItem(new DbJobJsonConverter());
		converters.AddUniqueItem(new DbJobLogJsonConverter());
		converters.AddUniqueItem(new DbJobMessageJsonConverter());
	}
}
#endif
