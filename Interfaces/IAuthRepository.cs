using LoginRegistrationJWT.Models;

namespace LoginRegistrationJWT.Interfaces
{
    public interface IAuthRepository
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        User GetUser(string username);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        bool RegisterUser(User user);
        bool UserExist(string username);
        bool Save();
        string CreateToken(User user);
    }
}
