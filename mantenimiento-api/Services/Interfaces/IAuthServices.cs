using mantenimiento_api.Controllers.VM;
using System.Security.Claims;

namespace mantenimiento_api.Services.Interfaces
{
    public interface IAuthServices
    {
        string CreateToken(int id);
        int? ValidateToken(string token);
        void hashPassTest(string pass, out byte[] salt, out string password);
    }
}
