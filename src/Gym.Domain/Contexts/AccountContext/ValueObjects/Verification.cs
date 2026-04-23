using Gym.Domain.Contexts.SharedContext.ValueObjects;

namespace Gym.Domain.Contexts.AccountContext.ValueObjects;

public class Verification : ValueObject
{
    public Verification()
    {
        
    }
    public string Code { get; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt { get; private set; } = null;

    public bool IsActive => VerifiedAt != null && ExpiresAt == null;
    public void Verify(string code)
    {
        if (!IsActive)
        {
            throw new Exception("Invalid verification code");
        }
        if(ExpiresAt > DateTime.UtcNow)
            throw new Exception("Invalid verification code");
        
        if(!string.Equals(code.Trim(), Code, StringComparison.InvariantCultureIgnoreCase))
            throw new Exception("Invalid verification code");
        
    }

}