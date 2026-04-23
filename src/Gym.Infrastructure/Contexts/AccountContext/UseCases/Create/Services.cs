using Gym.Domain;
using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Gym.Infrastructure.Contexts.AccountContext.UseCases.Create;

public class Services : IService
{
    public async Task SendEmailVerificationAsync(Member member, CancellationToken cancellationToken)
    {
        
        //refactor need for better understanding 
        // var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        // var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
        // const string subject = "Verify your account";
        // var to = new EmailAddress(member.Email, member.Name);
        // var content = $"Code Verification {member.Email.Verification.Code}";
        // var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        // await client.SendEmailAsync(msg, cancellationToken);
        Console.WriteLine($"Sending email verification email to {member.Email} code {member.Email.Verification}");
    }
}