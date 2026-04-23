namespace Gym.Domain.Contexts.AccountContext.UseCases.Authenticate;

public class Response
{
    public string Token { get; set; } = string.Empty;
    public string Id { get; set; }= string.Empty;
    public string Name { get; set; }= string.Empty;
    public string Email { get; set; }= string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}