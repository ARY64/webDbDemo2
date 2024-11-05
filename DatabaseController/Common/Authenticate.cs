using System.Security.Cryptography;
namespace WebDB.Models;
public static class Authenticate
{
    public static int Iterations { get; set; } = 10000;
    public static bool SetPassword(User user, string password, int iterations)
    {
        if (user == null || string.IsNullOrWhiteSpace(password))
            return false;

        byte[] saltByteArray = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltByteArray);
        using var hasher = new Rfc2898DeriveBytes(password, saltByteArray, iterations, HashAlgorithmName.SHA3_256);
        user.PasswordHash = hasher.GetBytes(32);
        user.PasswordSalt = saltByteArray;
        user.PasswordIterations = iterations;
        return true;
    }
    public static bool SetPassword(User user, string password)
    {
        return SetPassword(user, password, Iterations);
    }   
    public static bool getPassword(User user, string password)
    {
        if (user == null || string.IsNullOrWhiteSpace(password) || user.PasswordHash == null || user.PasswordSalt == null)
            return false;

        using var hasher = new Rfc2898DeriveBytes(password, user.PasswordSalt, user.PasswordIterations, HashAlgorithmName.SHA3_256);
        var hash = hasher.GetBytes(32);
        return hash.SequenceEqual(user.PasswordHash);
    }

}