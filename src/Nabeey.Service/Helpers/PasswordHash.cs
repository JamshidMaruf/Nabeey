using System.Security.Cryptography;

namespace Service.Helpers;

public class PasswordHash
{
    public static string Encrypt(string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashedPassword;
    }

    public static bool Verify(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}