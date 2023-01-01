using mantenimiento_api.Models;
using mantenimiento_api.Services.Interfaces;

namespace mantenimiento_api.Services
{
    public class UsersServices : IUsersServices
    {
        readonly ILogger _logger;
        readonly MantenimientoApiContext _context;
        readonly IConfiguration _config;
        public UsersServices(ILogger<UsersServices> logger, MantenimientoApiContext context, IConfiguration config) 
        {
            _logger = logger;
            _context = context;
            _config = config;
        }
        public bool DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    _logger.LogWarning("no se pudo encontrar usuario con id: {0}", id);
                    return false;
                }

                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return false;
            }
        }

        public User? GetUser(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                user.Password = null;
                user.Salt = null;
                return user;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return null;
            }
        }

        public User? GetUser(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(x => x.Email == email);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return null;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            try
            {
                return _context.Users.Select
                    ( usu =>
                        new User 
                        {
                            Id= usu.Id,
                            Name= usu.Name,
                            Email= usu.Email,
                            Active= usu.Active,
                            IdRol= usu.IdRol,
                            Password= null,
                            Salt= null
                        }
                    );
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message,e);
                return new List<User>();
            }
        }

        public int InsertUser(User user)
        {
            try
            {
                var numberSalt = _config.GetValue<string>("Salt:Number");
                user.Salt = Utils.SecurityHelper.GenerateSalt(int.Parse(numberSalt));
                user.Password = Utils.SecurityHelper.HashPassword(user.Password,user.Salt);
                _context.Users.Add(user);
                _context.SaveChanges();
                return user.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return 0;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return false;
            }
        }
    }
}
