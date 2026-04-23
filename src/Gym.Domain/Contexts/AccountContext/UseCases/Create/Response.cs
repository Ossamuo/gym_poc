

namespace Gym.Domain.Contexts.AccountContext.UseCases.Create;

public record Response(Guid Id, string Name, string Email,string EmailVerificationCode);
