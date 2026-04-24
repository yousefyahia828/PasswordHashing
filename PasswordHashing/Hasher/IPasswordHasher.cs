/// <summary>
/// Provides functionality for securely hashing passwords and verifying them
/// using a strong one-way hashing algorithm (e.g., Argon2).
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the specified plain-text password using a secure algorithm and returns
    /// a formatted string containing the hash and all required parameters (e.g., salt, iterations).
    /// </summary>
    /// <param name="password">The plain-text password to hash.</param>
    /// <returns>
    /// A hashed representation of the password, typically encoded in a standard format
    /// (e.g., PHC string format) that includes the algorithm parameters.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="password"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="password"/> is empty or invalid.
    /// </exception>
    string HashPassword(string password);

    /// <summary>
    /// Verifies whether the provided plain-text password matches the specified hashed password.
    /// </summary>
    /// <param name="passwordHash">
    /// The stored hashed password (including algorithm parameters and salt).
    /// </param>
    /// <param name="password">The plain-text password to verify.</param>
    /// <returns>
    /// <c>true</c> if the password matches the hash; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// This method should perform comparisons in constant time to mitigate timing attacks.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="passwordHash"/> or <paramref name="password"/> is <c>null</c>.
    /// </exception>
    bool VerifyPassword(string passwordHash, string password);
}