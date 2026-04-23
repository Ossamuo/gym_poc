using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;

namespace Gym.Domain.Contexts.PartnerContext.UseCases.BookingSessions
{
    public class Request : IRequest<Result<Response?>>
    {
        public string Token { get; set; } = string.Empty;
        public Guid ActivityId { get; set; }
        public Guid MemberId { get; set; }
        public  Guid PartnerId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
