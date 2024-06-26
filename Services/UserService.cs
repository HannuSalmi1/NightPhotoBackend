using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NightPhotoBackend.Dto;

using NightPhotoBackend.Helpers;
using NightPhotoBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NightPhotoBackend.Services
{
    public class UserService : IUserservice
    {
        private readonly NightPhotoDbContext _context;
        private readonly AppSettings _appSettings;
        private readonly IResponseCookies _responseCookies;
        private readonly IConfiguration _configuration;

        public UserService(NightPhotoDbContext context, IOptions<AppSettings> appSettings, IResponseCookies responseCookies, IConfiguration configuration)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _responseCookies = responseCookies;
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.UsersTable.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            // Create a cookie option
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // This makes the cookie HttpOnly
                Secure = true, // Transmit the cookie over HTTPS only
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7) // Set the expiration date
            };

            // Append the JWT as a cookie
            _responseCookies.Append("access_token", token, cookieOptions);

            return new AuthenticateResponse(user);
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        private string generateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var appSecret = _configuration.GetSection("AppSettings:Secret").Value;
            var key = Encoding.ASCII.GetBytes(appSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("username", user.Username.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "NightPhotoServer"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
