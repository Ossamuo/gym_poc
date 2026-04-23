
using Gym.Domain.Contexts.AccountContext.Exceptions;
using Gym.Domain.Contexts.AccountContext.ValueObjects;

namespace Gym.Domain.Test.AccountContext.ValueObjects;

[TestClass]
public class EmailTest
{
    [TestMethod]
    [DataRow("test@test.com")]
    [DataRow("www.test@test.com")]
    [DataRow("123test@test.com")]
    public void ShouldCreateEmail(string address)
    {
        var email = Email.Create(address);
        Assert.AreEqual(address, email.Address);
        
    }
    
    [TestMethod]
    [DataRow("")]
    [DataRow("     ")]
    public void ShouldThrowNullEmailException(string address)
    {
        Assert.Throws<EmailNullOrEmptyException>(() => Email.Create(address));
    }
    
    [TestMethod]
    [DataRow("www.test.com")]
    [DataRow("123@test@test.com")]
    public void ShouldThrowInvalidEmailException(string address)
    {
        Assert.Throws<InvalidEmailException>(() => Email.Create(address));

    }
    [TestMethod]
    [DataRow("t@t.a")]
    [DataRow("0123456789dasddpasoidaspodiaspdkssapoksopdm@dasoidaujsoiduasodi.com")]
    public void ShouldThrowInvalidEmailExceptionLength(string address)
    {
        Assert.Throws<InvalidEmailException>(() => Email.Create(address));

    }
}