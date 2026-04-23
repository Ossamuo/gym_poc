using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.AccountContext.UseCases.Edit
{
    public class Request : IRequest<Result<Response?>>
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

    }
}
