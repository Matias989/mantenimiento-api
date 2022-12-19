using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace mantenimiento_api.Utils
{
    public static class ErrorHelper
    {
        public static string GetErrorsFromModelState(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            var errors = new StringBuilder();
            foreach (var itemModelState in modelState.Values)
            {
                foreach (var error in itemModelState.Errors)
                {
                    errors.AppendLine(error.ErrorMessage);
                }
            }
            return errors.ToString();
        }
    }

    public static class SecurityHelper
    {
        public static byte[] GenerateSalt(int nSalt)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            return salt;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public static bool CheckHashes(string authPassword, string userPassword, byte[] userSalt)
        {
            var authHashPassGenerated = SecurityHelper.HashPassword(authPassword, userSalt);

            return authHashPassGenerated != userPassword;
        }

    }
}
