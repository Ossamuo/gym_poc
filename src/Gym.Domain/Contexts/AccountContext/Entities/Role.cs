using Gym.Domain.Contexts.SharedContext.Entities;

namespace Gym.Domain.Contexts.AccountContext.Entities;

public class Role : Entity
{
    public Role():base(Guid.CreateVersion7())
    {
        
    }
    public string Name { get; set; } = string.Empty;
    public List<Member> Members { get; set; } = new() ;
}