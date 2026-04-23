namespace Gym.Web.Shared;

public static class Configuration 
{
    public const string HttpClientName = "GymApp";
    public static string BackendUrl { get; set; } = string.Empty;
    public static string ImageUrl { get; set; } = string.Empty;

    public static SecretsConfiguration Secrets { get; } = new();


    public class SecretsConfiguration
    {        
        public string JwtPrivateKey { get; set; } = string.Empty;
    }
}