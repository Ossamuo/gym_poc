using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.List;

namespace Gym.Domain.Contexts.ActivitiesContext.UseCases.List.Contracts
{
    public interface IRepository
    {
        Task<List<Response>> GetActivitiesAsync(Request request, CancellationToken cancellationToken);
    }
}
