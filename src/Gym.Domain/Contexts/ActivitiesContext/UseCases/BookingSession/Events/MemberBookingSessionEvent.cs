using Gym.Domain.Contexts.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events
{
    public  class MemberBookingSessionEvent : INotification
    {
        public Guid PartnerId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid MemberId { get; set; }
    }
}
