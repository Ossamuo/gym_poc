using Gym.Domain.Contexts.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Create.Events
{
   
    public class MemberCreatedEvent : INotification
    {
        public Guid MemberId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string EmailVerificationCode { get; set; } = string.Empty;
    }
}
