#if NET6_0_OR_GREATER
using Envelope.ServiceBus.Messages;
using Envelope.ServiceBus.Serializers.JsonConverters.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public class DbJobMessageJsonConverter : JsonConverter<IJobMessage>
{
	private static readonly Type _deserializedJobMessage = typeof(DeserializedJobMessage);

	public override void Write(Utf8JsonWriter writer, IJobMessage value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("Read only converter");
	}

	public override IJobMessage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			return ((JsonConverter<DeserializedJobMessage>)options.GetConverter(_deserializedJobMessage)).Read(ref reader, _deserializedJobMessage, options);
		}
	}
}
#endif
