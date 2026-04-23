using Gym.Domain.Contexts.AccountContext.UseCases.Create;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared;

namespace Gym.Web.Contexts.AccountContext.Create;

public class Handler (IHttpClientFactory httpClientFactory):  IHandler<Request, Result<Response?>>
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

    public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
    {
        var response  = await _client.PostAsJsonAsync($"api/v1/members/", request, cancellationToken: cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<Result<Response?>>(cancellationToken: cancellationToken);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return new Result<Response?>(null, (int)response.StatusCode, result?.Message ?? "Error creating your account.");
        
        return result ?? new Result<Response?>(null, 500, "Error creating your account.");
    }
}