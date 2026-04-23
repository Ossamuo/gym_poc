using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession
{
    public class Request : IRequest<Result<Response?>>
    {
        public string Token { get; set; } = string.Empty;
        public Guid ActivityId { get; set; }
        public Guid MemberId { get; set; }
        public Guid PartnerId { get; set; }

    }
}