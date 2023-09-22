using System.Security.Cryptography;

namespace Service.Helpers;

public class PasswordHash
{
    public static void Encrypt(string password, out byte[] passwordhash, out byte[] passwordsalt)
    {
        using(var hmac=new HMACSHA512())
        {
            passwordsalt = hmac.Key;
            passwordhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    public static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using (var hmac = new HMACSHA512(salt))
        {
            var computedHash =
                hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }
    }
}
