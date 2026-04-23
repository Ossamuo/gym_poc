using Gym.Domain.Contexts.ActivitiesContext.UseCases.List;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared;
using System.Net.Http.Headers;

namespace Gym.Web.Contexts.ActivitiesContext.List
{
    public class Handler(IHttpClientFactory httpClientFactory) : IHandler<Request, Result<List<Response>?>>
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Result<List<Response>?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {
            if (request.Token != null)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

            }

            var response = await _client.PostAsJsonAsync($"api/v1/activities/", request, cancellationToken: cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<Result<List<Response>?>>(cancellationToken: cancellationToken);
            if (!result.IsSuccess)
                return new Result<List<Response>?>(null, 400, result.Message ?? "Error retreving data.");
            //return new Result<List<Response>?>(null, (int)400, response?.Message ?? "Error login in your account.");

            return result ?? new Result<List<Response>?>(null, 400, "Error retreving data.") ; 
        }
    }
}
