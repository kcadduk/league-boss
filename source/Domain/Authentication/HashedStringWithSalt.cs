namespace LeagueBoss.Domain.Authentication;

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

public record HashedStringWithSalt
{
    private byte[] _salt = new byte[16];
    private HashedStringWithSalt()
    {
    }

    public string Value { get; private init; } = null!;
    public override string ToString() => Value;

    public bool IsMatch(string input)
    {
        var inputPasswordHash = CreateHash(input, _salt);

        return ToString() == inputPasswordHash.value;
    }
    
    public static bool TryParse(string hashedStringWithSalt, [MaybeNullWhen(false)] out HashedStringWithSalt result)
    {
        byte[] hashBytes;
        try
        {
            hashBytes = Convert.FromBase64String(hashedStringWithSalt);
            if(hashBytes.Length < 36)
            {
                result = null;
                return false;
            }

            result = Parse(hashedStringWithSalt);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    public static HashedStringWithSalt Parse(string hashedStringWithSalt)
    {
        var hashBytes = Convert.FromBase64String(hashedStringWithSalt);
        
        var salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        var hash = new byte[hashBytes.Length - 16];
        Array.Copy(hashBytes, 16, hash, 0, hashBytes.Length - 16);
        
        return new HashedStringWithSalt { Value = hashedStringWithSalt, _salt = salt };
    }

    public static HashedStringWithSalt Create(string plainTextString)
    {
        if (string.IsNullOrEmpty(plainTextString))
        {
            throw new ArgumentException("Cannot hash an empty string");
        }
        
        var (value, salt) = CreateHash(plainTextString);
        return new HashedStringWithSalt { Value = value, _salt = salt};
    }

    private static byte[] CreateSalt()
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }
    
    private static (string value, byte[] salt) CreateHash(string plainTextString, byte[]? salt = default)
    {
        salt ??= CreateSalt();

        const int iterations = 10000;
        var hashAlgorithm = HashAlgorithmName.SHA256;

        using var pbkdf2 = new Rfc2898DeriveBytes(plainTextString, salt, iterations, hashAlgorithm);
        var hash = pbkdf2.GetBytes(20);
        
        var hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        var inputHash = Convert.ToBase64String(hashBytes);
        
        return (inputHash, salt);
    }
}