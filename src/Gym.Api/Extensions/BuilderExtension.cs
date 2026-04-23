using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Gym.Application;
using Gym.Application.Contexts.SharedContext.Messages;
using Gym.Domain;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Gym.Api.Extensions;

public static class BuilderExtension
{

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {

        Configuration.Database.ConnectionString =
            builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;


        Configuration.Secrets.ApiKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;
        Configuration.Secrets.JwtPrivateKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
        Configuration.Secrets.PasswordSaltKey =
            builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;

        Configuration.SendGrid.ApiKey =
            builder.Configuration.GetSection("SendGrid").GetValue<string>("ApiKey") ?? string.Empty;

        Configuration.Email.DefaultFromName =
            builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromName") ?? string.Empty;
        Configuration.Email.DefaultFromEmail =
            builder.Configuration.GetSection("Email").GetValue<string>("DefaultFromEmail") ?? string.Empty;


        Configuration.Kafka.BootstrapServers =
            builder.Configuration.GetSection("Kafka").GetValue<string>("BootstrapServer") ?? string.Empty;

        Configuration.Kafka.Topic =
            builder.Configuration.GetSection("Kafka").GetValue<string>("TopicName") ?? string.Empty;

        Configuration.Kafka.SchemaRegistryUrl =
            builder.Configuration.GetSection("Kafka").GetValue<string>("SchemaRegistryUrl") ?? string.Empty;

    }

    public static void AddOpenTelemetryServices(this WebApplicationBuilder builder)
    {
        var serviceName = "api-dotnet";


        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithMetrics(metrics =>
            {
                metrics
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")//server information
                .AddMeter("Gym.App.BusinessMetrics")
                .AddHttpClientInstrumentation() //
                .AddAspNetCoreInstrumentation()                
                .AddRuntimeInstrumentation()                
                .AddOtlpExporter(opt =>
                {
                    opt.Endpoint = new Uri("http://localhost:4317");
                    opt.Protocol = OtlpExportProtocol.Grpc; 
                });
            });
    }

    public static void AddAppDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                , migrationOptions => migrationOptions.MigrationsAssembly("Gym.Api")
                ));
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {

        builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        builder.Services.AddAuthorization();
    }


    public static void AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediator(typeof(Mediator).Assembly);
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }
    public static void AddSchemaRegistryClient(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<ISchemaRegistryClient>(sp =>
        {
            var config = new SchemaRegistryConfig
            {
                Url = Configuration.Kafka.SchemaRegistryUrl,
            };
            return new CachedSchemaRegistryClient(config);
        });
    }
    public static void AddKafkaServices(this WebApplicationBuilder builder)
    {


        // 2. Register  the producer
        builder.Services.AddSingleton<IProducer<string, MemberBookingSessionEvent>>(sp =>
        {
            var schemaClient = sp.GetRequiredService<ISchemaRegistryClient>();

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = Configuration.Kafka.BootstrapServers,
                //Acks = Acks.All,
                //EnableIdempotence = true,
                //MessageSendMaxRetries = 5,
                //LingerMs = 5,
                //BatchNumMessages = 1000,
                //CompressionType = CompressionType.Snappy
            };

            return new ProducerBuilder<string, MemberBookingSessionEvent>(producerConfig)
                .SetValueSerializer(new CustomJsonSerializer<MemberBookingSessionEvent>(schemaClient))
                .Build();
        });

    }

}