using Gym.Domain.Contexts.AccountContext.Entities;
using Gym.Domain.Contexts.AccountContext.Specifications;
using Gym.Domain.Contexts.AccountContext.UseCases.Detail;
using Gym.Domain.Contexts.AccountContext.UseCases.Detail.Contracts;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;

namespace Gym.Application.Contexts.AccountContext.UseCases.Detail
{
    public class Handler : IHandler<Request, Result<Response?>>
    {

        private readonly IRepository _repository;

        public Handler(IRepository repo)
        {
            _repository = repo;
        }
        public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {
            try
            {
                var specification = new GetByIdSpecification(request.Id);
                var  member = await _repository.GetByIdAsycn(specification);
                if (member == null)
                    return new Result<Response?>(null, 404, "Member not found.");

                var response = new Response(member.Name, member.Email, member.Image);
                return new Result<Response?>(response);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
