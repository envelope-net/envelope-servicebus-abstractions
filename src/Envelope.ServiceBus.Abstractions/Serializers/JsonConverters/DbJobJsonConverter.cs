#if NET6_0_OR_GREATER
using Envelope.ServiceBus.Queries;
using Envelope.ServiceBus.Serializers.JsonConverters.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public class DbJobJsonConverter : JsonConverter<IDbJob>
{
	private static readonly Type _deserializedJob = typeof(DeserializedJob);

	public override void Write(Utf8JsonWriter writer, IDbJob value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("Read only converter");
	}

	public override IDbJob? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			return ((JsonConverter<DeserializedJob>)options.GetConverter(_deserializedJob)).Read(ref reader, _deserializedJob, options);
		}
	}
}
#endif
