namespace Gym.Web.Shared.Security;
public class CookieHandler: DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-Request-With",["XMLHttpRequest"]);
        return base.SendAsync(request, cancellationToken);
    }
}
