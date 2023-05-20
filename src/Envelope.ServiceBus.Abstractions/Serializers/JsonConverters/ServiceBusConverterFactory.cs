//#if NET6_0_OR_GREATER
//using Envelope.ServiceBus.Serializers.JsonConverters.Model;
//using Envelope.ServiceBus.Messages;
//using Envelope.ServiceBus.Queries;
//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace Envelope.ServiceBus.Serializers.JsonConverters;

//public class ServiceBusConverterFactory : JsonConverterFactory
//{
//	private static readonly Lazy<Dictionary<Type, Type>> _converterTypes = new(() => new Dictionary<Type, Type>
//	{
//		{ typeof(IDbHost), typeof(DeserializedHost) },
//		{ typeof(IDbHostLog), typeof(DeserializedHostLog) },
//		{ typeof(IDbJob), typeof(DeserializedJob) },
//		{ typeof(IDbJobExecution), typeof(DeserializedJobExecution) },
//		{ typeof(IDbJobLog), typeof(DeserializedJobLog) },
//		{ typeof(IJobMessage), typeof(DeserializedJobMessage) }
//	});

//	public override bool CanConvert(Type typeToConvert)
//		=> typeToConvert.IsInterface && _converterTypes.Value.Keys.Any(t => t == typeToConvert);

//	public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
//	{
//		var ifcType = _converterTypes.Value.Keys.FirstOrDefault(t => typeToConvert.IsAssignableFrom(t));

//		if (ifcType != null)
//			typeToConvert = _converterTypes.Value[ifcType];

//		return options.GetConverter(typeToConvert);
//	}
//}
//#endif
