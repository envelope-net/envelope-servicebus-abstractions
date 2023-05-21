#if NET6_0_OR_GREATER
using Envelope.ServiceBus.Queries;
using Envelope.ServiceBus.Serializers.JsonConverters.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public class DbHostLogJsonConverter : JsonConverter<IDbHostLog>
{
	private static readonly Type _deserializedHostLog = typeof(DeserializedHostLog);

	public override void Write(Utf8JsonWriter writer, IDbHostLog value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("Read only converter");
	}

	public override IDbHostLog? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			return ((JsonConverter<DeserializedHostLog>)options.GetConverter(_deserializedHostLog)).Read(ref reader, _deserializedHostLog, options);
		}
	}
}
#endif
