using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Gym.BookingMessage.Producer.Extensions;
using Gym.BookingMessage.Producer.Workers;
using Gym.Domain;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions.Contratcs;
using Gym.Domain.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Constracts;
using Gym.Infrastructure.Contexts.PartnerContext.UseCases.BookingSession;
using Gym.Infrastructure.Contexts.PartnerContext.UseCases.Events.MemberBookingSession;
using Gym.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

// Load user-secrets (dotnet user-secrets set "Key" "Value")
builder.Configuration.AddUserSecrets<Program>();

// Populate static Configuration from appsettings
Configuration.Database.ConnectionString =
    builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

Configuration.Kafka.BootstrapServers =
    builder.Configuration.GetSection("Kafka").GetValue<string>("BootstrapServer") ?? string.Empty;

Configuration.Kafka.Topic =
    builder.Configuration.GetSection("Kafka").GetValue<string>("TopicName") ?? string.Empty;

Configuration.Kafka.SchemaRegistryUrl =
    builder.Configuration.GetSection("Kafka").GetValue<string>("SchemaRegistryUrl") ?? string.Empty;


// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Configuration.Database.ConnectionString));

// Kafka Schema Registry & Producer
builder.Services.AddSingleton<ISchemaRegistryClient>(_ =>
    new CachedSchemaRegistryClient(new SchemaRegistryConfig
    {
        Url = Configuration.Kafka.SchemaRegistryUrl
    }));

builder.Services.AddSingleton<IProducer<string, MemberBookingSessionEvent>>(sp =>
{
    var schemaClient = sp.GetRequiredService<ISchemaRegistryClient>();
    var config = new ProducerConfig { BootstrapServers = Configuration.Kafka.BootstrapServers };
    return new ProducerBuilder<string, MemberBookingSessionEvent>(config)
        .SetValueSerializer(new CustomJsonSerializer<MemberBookingSessionEvent>(schemaClient))
        .Build();
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IServices, Service>();

// Worker
builder.Services.AddHostedService<BookingProducerWorker>();

var host = builder.Build();
host.Run();
