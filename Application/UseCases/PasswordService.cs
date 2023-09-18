using Application.Helpers;
using Application.Interfaces;

namespace Application.UseCases
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return Encrypt.GetSHA256(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return Encrypt.GetSHA256(password) == hashedPassword;
        }
    }
}
