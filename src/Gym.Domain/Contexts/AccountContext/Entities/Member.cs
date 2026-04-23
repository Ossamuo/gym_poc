using Gym.Domain.Contexts.AccountContext.ValueObjects;
using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.SharedContext.Entities;

namespace Gym.Domain.Contexts.AccountContext.Entities;

public sealed class Member : Entity
{
    #region Properties

    public Email Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public Password Password { get; set; } = null!;
    public string Image { get; set; } = string.Empty;
    public List<Role> Roles { get; set; } = new();
    
    public List<Activity> Activities { get; set; } = new();
    
    
    #endregion

    #region Constructors

    private Member() : base(Guid.NewGuid())
    {
    }
    private Member(string name , string email, string? password = null) : base(Guid.NewGuid())
    {
        Name = name; 
        Email = Email.Create(email);
        Password = new Password(password);
    }

    private Member(string name, Email email, Password password) : base(Guid.NewGuid())
    {
        Name = name;
        Email = email;
        Password = password;
    }

    #endregion

    public static Member Create(string name, Email email, Password password)
    {
        return new Member(name, email, password);
    }
    public static Member Create(string name,string email, string password)
    {
        return new Member(name, email, password);
    }
}