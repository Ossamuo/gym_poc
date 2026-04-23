using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.ActivitiesContext.UseCases.List
{
    /// <summary>
    /// TODO Change the result for a Paged result, with the total of items, the page number, the page size and the list of items
    /// </summary>
    public class Request : IRequest<Result<List<Response>?>>
    {
        public string Token { get; set; } = string.Empty;
        public Guid MemberId { get; set; }
        public DateTime StartAt { get; set; }
    }
}
