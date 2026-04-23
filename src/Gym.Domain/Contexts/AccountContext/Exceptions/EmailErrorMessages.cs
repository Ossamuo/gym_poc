namespace Gym.Domain.Contexts.AccountContext.Exceptions;

public static class EmailErrorMessages
{
    public static string Invalid { get; } = "Invalid email address.";
    public static string NullOrEmpty { get; } = "Email  address is null or empty.";
}