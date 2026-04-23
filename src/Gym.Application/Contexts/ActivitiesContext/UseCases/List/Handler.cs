using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.List;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.List.Contracts;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Contexts.ActivitiesContext.UseCases.List
{
    public class Handler : IHandler<Request, Result<List<Response>?>>
    {
        private readonly IRepository _repository;
        public Handler(IRepository repo)
        {
            _repository = repo;
        }
        public async Task<Result<List<Response>?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {
            List<Response> list= await _repository.GetActivitiesAsync(request, cancellationToken);
            

            return new Result<List<Response>?>(list, 200);


        }
    }
}
