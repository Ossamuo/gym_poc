using System.Text.RegularExpressions;
using Gym.Domain.Contexts.AccountContext.Exceptions;
using Gym.Domain.Contexts.SharedContext.Extensions;
using Gym.Domain.Contexts.SharedContext.ValueObjects;

namespace Gym.Domain.Contexts.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    //create the Constants

    #region Constants

    public const int MinLength = 6;
    public const int MaxLength = 60;
    public const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    #endregion

    //create the properties address and hash

    #region Properties

    public string Address { get; private set; } = string.Empty;
    public string Hash { get; private set; } = string.Empty;

    public Verification Verification { get; private set; } = new Verification();

    #endregion

    // create the constructors with the rules 

    #region Constructors
    
    private Email()
    {
    }

    private Email(string address, string hash)
    {
        Address = address;
        Hash = hash;
    }

    #endregion


    //create the factories
    public static Email Create(string address)
    {
        if (string.IsNullOrEmpty(address)
            || string.IsNullOrWhiteSpace(address))
            throw new EmailNullOrEmptyException();

        address = address.Trim();
        address = address.ToLower().Trim();
        if (!Regex.IsMatch(address, Pattern))
            throw new InvalidEmailException();

        if (address.Length < MinLength)
            throw new InvalidEmailException();
        if (address.Length > MaxLength)
            throw new InvalidEmailException();
        
        return new Email(address, address.ToBase64());
    }
    public void ResendVerification() => Verification = new  Verification();
    
    public static implicit operator string(Email email) 
        => email.ToString();
    
    public override string ToString()
    {
        return Address;
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();
    
    
}