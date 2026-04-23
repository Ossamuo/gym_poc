using Confluent.Kafka;
using Confluent.SchemaRegistry;
using System.Text;
using System.Text.Json;

namespace Gym.BookingMessage.Producer.Extensions;

public class CustomJsonSerializer<T> : IAsyncSerializer<T>
{
    private readonly ISchemaRegistryClient _client;
    public CustomJsonSerializer(ISchemaRegistryClient client) => _client = client;

    public async Task<byte[]> SerializeAsync(T data, SerializationContext context)
    {
        var json = JsonSerializer.Serialize(data);
        return await Task.Run(() => Encoding.UTF8.GetBytes(json));
    }
}
