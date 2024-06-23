namespace NightPhotoBackend.Services
{
    public class ResponseCookies : IResponseCookies
    {
        public void Append(string key, string value)
        {
            throw new NotImplementedException();
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResponseCookies(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Append(string key, string value, CookieOptions options)
        {
            var httpContext = new HttpContextAccessor().HttpContext;

            // Append the cookie
            httpContext.Response.Cookies.Append(key, value, options);
        }

        public void Delete(string key)
        {
            throw new NotImplementedException();
        }

        public void Delete(string key, CookieOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
