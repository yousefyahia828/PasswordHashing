using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;
using PasswordHashing.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHashing.Hasher;

internal class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHashOptions _options;

    public PasswordHasher(IOptions<PasswordHashOptions> options)
    {
        _options = options.Value;
    }

    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        if (_options is null)
        {
            throw new InvalidOperationException("Missing password hash options");
        }

        var salt = RandomNumberGenerator.GetBytes(_options.SaltSize);

        using var argon2id = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = _options.DegreeOfParallelism,
            MemorySize = _options.MemorySizeInBytes,
            Iterations = _options.Iterations
        };

        var hashBytes = argon2id.GetBytes(_options.HashSize);

        var phcFormat = PhcFormatter.Format(new PhcHash
        {
            Hash = hashBytes,
            Salt = salt,
            DegreeOfParallelism = _options.DegreeOfParallelism,
            Iterations = _options.Iterations,
            MemorySizeInBytes = _options.MemorySizeInBytes
        });

        return phcFormat;
    }

    public bool VerifyPassword(string passwordHash, string password)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentNullException(nameof(passwordHash));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        var phcHash = PhcParser.Parse(passwordHash);

        if (phcHash is null)
            return false;

        using var argon2id = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = phcHash.Salt,
            DegreeOfParallelism = phcHash.DegreeOfParallelism,
            Iterations = phcHash.Iterations,
            MemorySize = phcHash.MemorySizeInBytes
        };

        var computedHash = argon2id.GetBytes(phcHash.Hash.Length);

        return CryptographicOperations.FixedTimeEquals(phcHash.Hash, computedHash);
    }
}
