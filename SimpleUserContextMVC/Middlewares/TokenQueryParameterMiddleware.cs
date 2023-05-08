using SimpleUser.MVC.Services;

namespace SimpleUser.MVC.Middlewares
{
    public class TokenQueryParameterMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenQueryParameterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IAccessTokenService accessTokenService)
        {
            try
            {
                //if has request header, do nothing
                if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader) && authHeader.Any() &&
                    authHeader[0].StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }

                var jwtToken = httpContext.Request.Cookies["JWToken"];
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    httpContext.Request.Headers.Add("Authorization", "Bearer " + jwtToken);
                }
            }
            finally
            {
                // Call the next middleware delegate in the pipeline 
                await _next.Invoke(httpContext);
            }
        }
    }
}
