using System.Security.AccessControl;

namespace Gym.Domain;

public static class Configuration
{
    public const int DefaultStatusCode  = 200;

    public static SecretsConfiguration Secrets { get; } = new();

    public static DatabaseConfiguration Database { get; } = new();
    
    public static EmailConfiguration Email { get; } = new();
    
    public static SendGridConfiguration SendGrid { get; } = new();

    public static KafkaConfiguration Kafka { get; } = new();

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }

    public class SecretsConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
        public string JwtPrivateKey { get; set; } = string.Empty;

        /// <summary>
        /// Additional password for concat the with the user password
        /// </summary>
        public string PasswordSaltKey { get; set; } = string.Empty;
    }

    public class EmailConfiguration
    {
        public string DefaultFromEmail { get; set; } = string.Empty;
        public string DefaultFromName { get; set; } = string.Empty;
    }
    
    public class SendGridConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string SchemaRegistryUrl { get; set; } = string.Empty;
    }
}