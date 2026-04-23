using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Detail
{
    public class Request : IRequest<Result<Response?>>
    {
        public string Token { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}
