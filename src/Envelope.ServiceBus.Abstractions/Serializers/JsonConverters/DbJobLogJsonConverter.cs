#if NET6_0_OR_GREATER
using Envelope.ServiceBus.Queries;
using Envelope.ServiceBus.Serializers.JsonConverters.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public class DbJobLogJsonConverter : JsonConverter<IDbJobLog>
{
	private static readonly Type _deserializedJobLog = typeof(DeserializedJobLog);

	public override void Write(Utf8JsonWriter writer, IDbJobLog value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("Read only converter");
	}

	public override IDbJobLog? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			return ((JsonConverter<DeserializedJobLog>)options.GetConverter(_deserializedJobLog)).Read(ref reader, _deserializedJobLog, options);
		}
	}
}
#endif
