using Gym.Domain.Contexts.AccountContext.UseCases.Detail;
using Gym.Domain.Contexts.SharedContext.Abstractions;
using Gym.Domain.Contexts.SharedContext.Results;
using Gym.Web.Shared;
using NuGet.Common;
using System.Net.Http.Headers;

namespace Gym.Web.Contexts.AccountContext.Detail
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

            var response = await _client.GetFromJsonAsync<Result<Response?>>($"api/v1/members/",  cancellationToken: cancellationToken);            
            //var result = await response.Content.ReadFromJsonAsync<Result<Response?>>(cancellationToken: cancellationToken);
            if (!response.IsSuccess)
                return new Result<Response?>(null, (int)400, response?.Message ?? "Error login in your account.");

            return response ?? new Result<Response?>(null, 500, "Error login in your account.");
        }
    }
}
