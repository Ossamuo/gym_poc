using System.Net.Http.Headers;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.BookingSession;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared;

namespace Gym.Web.Contexts.ActivitiesContext.BookingSession;

public class Handler(IHttpClientFactory httpClientFactory) : IHandler<Request, Result<Response?>>
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
    {
        if (request.Token != null)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

        }
        var response  = await _client.PostAsJsonAsync($"api/v1/activities/booking", request, cancellationToken: cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<Result<Response?>>(cancellationToken: cancellationToken);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return new Result<Response?>(null, (int)response.StatusCode, result?.Message ?? "Error booking your activity.");
        
        return result ?? new Result<Response?>(null, 500, "Error booking your activity.");
    }
   
}

