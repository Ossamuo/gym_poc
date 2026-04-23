using Gym.Domain.Contexts.AccountContext.UseCases.Edit;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared;
using System.Net.Http.Headers;

namespace Gym.Web.Contexts.AccountContext.Edit
{
    public class Handler(IHttpClientFactory httpClientFactory) : IHandler<Request, Result<Response?>>
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Result<Response?>> HandlerAsync(Request request, CancellationToken cancellationToken = default)
        {
            if (request.Token != null)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);

            }
            try
            {
                var response = await _client.PostAsJsonAsync($"api/v1/members/edit", request, cancellationToken: cancellationToken);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return new Result<Response?>(null, (int)response.StatusCode, "Error creating your account.");
                }
                var result = await response.Content.ReadFromJsonAsync<Result<Response?>>(cancellationToken: cancellationToken);
                return result ?? new Result<Response?>(null, 500, "Error creating your account.");

            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
