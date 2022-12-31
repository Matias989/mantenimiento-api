using mantenimiento_api.Models;
using mantenimiento_api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mantenimiento_api.Services
{
    public class AuthServices : IAuthServices
    {
        readonly ILogger _logger;
        readonly MantenimientoApiContext _context;
        readonly IConfiguration _config;
        public AuthServices(ILogger<UsersServices> logger, MantenimientoApiContext context, IConfiguration config) 
        {
            _context= context;
            _logger = logger;
            _config= config;
        }
        public string CreateToken(int id)
        {
            _logger.LogInformation("------Start AuthService - CreateToken ------");
            try
            {
                //Token Creation

                var secretKey = _config.GetValue<string>("Jwt:Key");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim("id",id.ToString()));

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = System.DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescription);

                var barerToken = tokenHandler.WriteToken(createdToken);

                return barerToken;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return String.Empty;
            }
            finally
            {
                _logger.LogInformation("------Finish AuthService - CreateToken ------");
            }
        }

        public void hashPassTest(string pass, out byte[] salt, out string password) 
        {
            var numberSalt = _config.GetValue<string>("Salt:Number");
            salt = Utils.SecurityHelper.GenerateSalt(int.Parse(numberSalt));
            password = Utils.SecurityHelper.HashPassword(pass, salt);
        }
    }
}
