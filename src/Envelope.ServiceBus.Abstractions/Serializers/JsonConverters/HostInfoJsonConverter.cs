#if NET6_0_OR_GREATER
using Envelope.Enums;
using Envelope.Infrastructure;
using Envelope.ServiceBus.Hosts;
using Envelope.ServiceBus.Serializers.JsonConverters.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public class HostInfoJsonConverter : JsonConverter<IHostInfo>
{
	private static readonly Type _environmentInfo = typeof(EnvironmentInfo);

	public override void Write(Utf8JsonWriter writer, IHostInfo value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("Read only converter");
	}

	public override IHostInfo? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			return null;
		}

		if (reader.TokenType != JsonTokenType.StartObject)
		{
			throw new JsonException();
		}
		else
		{
			var stringComparison = options.PropertyNameCaseInsensitive
				? StringComparison.OrdinalIgnoreCase
				: StringComparison.Ordinal;

			var hostInfo = new DeserializedHostInfo
			{
			};

			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
				{
					return hostInfo;
				}

				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string? value;
					var propertyName = reader.GetString();
					reader.Read();
					switch (propertyName)
					{
						case var name when string.Equals(name, nameof(IHostInfo.HostName), stringComparison):
							hostInfo.HostName = reader.GetString()!;
							break;
						case var name when string.Equals(name, nameof(IHostInfo.HostId), stringComparison):
							value = reader.GetString();
							hostInfo.HostId = Guid.TryParse(value, out var hostId) ? hostId : hostId;
							break;
						case var name when string.Equals(name, nameof(IHostInfo.InstanceId), stringComparison):
							value = reader.GetString();
							hostInfo.InstanceId = Guid.TryParse(value, out var instanceId) ? instanceId : instanceId;
							break;
						case var name when string.Equals(name, nameof(IHostInfo.HostStatus), stringComparison):
							if (reader.TokenType != JsonTokenType.Null && reader.TryGetInt32(out var idHostStatus))
								hostInfo.HostStatus = EnumHelper.ConvertIntToEnum<HostStatus>(idHostStatus);
							break;
						case var name when string.Equals(name, nameof(IHostInfo.EnvironmentInfo), stringComparison):
							hostInfo.EnvironmentInfo = reader.TokenType == JsonTokenType.Null ? default : ((JsonConverter<EnvironmentInfo>)options.GetConverter(_environmentInfo)).Read(ref reader, _environmentInfo, options);
							break;
					}
				}
			}

			return default;
		}
	}
}
#endif
