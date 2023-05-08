using Microsoft.AspNetCore.Http;
using SimpleUser.MVC.Services;

namespace SimpleUser.MVC.Middlewares
{
    public class AccessTokenHandler : DelegatingHandler
    {
        private readonly IAccessTokenService _accessTokenService;

        public AccessTokenHandler(IAccessTokenService accessTokenService)
        {
            _accessTokenService = accessTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", "Bearer " + _accessTokenService.GetToken());
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
