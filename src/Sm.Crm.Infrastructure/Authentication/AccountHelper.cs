using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Sm.Crm.Infrastructure.Authentication;

public class AccountHelper
{
    public static byte[] GenerateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(salt);
        return salt;
    }

    public static string HashCreate(string password)
    {
        var salt = GenerateSalt();

        var bytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 100000,
            numBytesRequested: 256 / 8);

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
    }

    public static bool HashValidate(string hash, string value)
    {
        try
        {
            var parts = hash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var bytes = KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA1, 100000, 256 / 8);

            return parts[1].Equals(Convert.ToBase64String(bytes));
        }
        catch
        {
            return false;
        }
    }
}