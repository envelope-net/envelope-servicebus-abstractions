#if NET6_0_OR_GREATER
using Envelope.ServiceBus.Serializers.JsonConverters;
using System.Text.Json;

namespace Envelope.Extensions;

public static class JsonSerializerOptionsExtensions
{
	public static JsonSerializerOptions AddServiceBusReadConverters(this JsonSerializerOptions options)
	{
		if (options == null)
			throw new ArgumentNullException(nameof(options));

		JsonConvertersConfig.AddServiceBusReadConverters(options.Converters);
		return options;
	}
}
#endif
