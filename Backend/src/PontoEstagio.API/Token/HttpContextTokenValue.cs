
using PontoEstagio.Infrastructure.Security.Tokens;

namespace PontoEstagio.API.Token;

public class HttpContextTokenValue : ITokenProvider
{
        readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTokenValue(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? TokenOnRequest()
        {
            var authorizationHeader = _httpContextAccessor
                                                .HttpContext!
                                                .Request
                                                .Headers["Authorization"]
                                                .FirstOrDefault();

            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                 return authorizationHeader.Substring("Bearer ".Length).Trim();
 
            return null;
        }
}