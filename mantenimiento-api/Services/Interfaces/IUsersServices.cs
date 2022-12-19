using mantenimiento_api.Models;

namespace mantenimiento_api.Services.Interfaces
{
    public interface IUsersServices
    {
        IEnumerable<User> GetUsers();
        User? GetUser(int id);
        User? GetUser(string email);
        int InsertUser(User user);
        bool DeleteUser(int id);
        bool UpdateUser(User user);
    }
}
