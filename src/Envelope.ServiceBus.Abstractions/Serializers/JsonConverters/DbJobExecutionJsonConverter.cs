#if NET6_0_OR_GREATER
using Envelope.ServiceBus.Queries;
using Envelope.ServiceBus.Serializers.JsonConverters.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Envelope.ServiceBus.Serializers.JsonConverters;

public class DbJobExecutionJsonConverter : JsonConverter<IDbJobExecution>
{
	private static readonly Type _deserializedJobExecution = typeof(DeserializedJobExecution);

	public override void Write(Utf8JsonWriter writer, IDbJobExecution value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("Read only converter");
	}

	public override IDbJobExecution? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
			return ((JsonConverter<DeserializedJobExecution>)options.GetConverter(_deserializedJobExecution)).Read(ref reader, _deserializedJobExecution, options);
		}
	}
}
#endif
