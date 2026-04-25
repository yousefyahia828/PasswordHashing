namespace PasswordHashing.Hasher;

/// <summary>
/// Represents configuration options for password hashing.
/// These settings control the behavior and security characteristics
/// of the underlying hashing algorithm (e.g., Argon2).
/// </summary>
public class PasswordHashOptions
{
    /// <summary>
    /// Gets or sets the size of the randomly generated salt in bytes.
    /// A larger salt increases resistance against precomputed attacks (e.g., rainbow tables).
    /// </summary>
    /// <value>Default is 16 bytes.</value>
    public int SaltSize { get; set; } = 16;

    /// <summary>
    /// Gets or sets the size of the generated hash in bytes.
    /// Larger hashes may provide better security but increase storage size.
    /// </summary>
    /// <value>Default is 32 bytes.</value>
    public int HashSize { get; set; } = 32;

    /// <summary>
    /// Gets or sets the number of iterations (time cost) used during hashing.
    /// Increasing this value makes hashing slower, improving resistance to brute-force attacks.
    /// </summary>
    /// <value>Default is 6 iterations.</value>
    public int Iterations { get; set; } = 6;

    /// <summary>
    /// Gets or sets the amount of memory (in bytes) used by the hashing algorithm.
    /// Higher values increase resistance to GPU/ASIC attacks but require more system resources.
    /// </summary>
    /// <value>Default is 65536 bytes (64 KB).</value>
    public int MemorySizeInBytes { get; set; } = 65536;

    /// <summary>
    /// Gets or sets the degree of parallelism (number of threads) used during hashing.
    /// Higher values can improve performance on multi-core systems.
    /// </summary>
    /// <value>Default value depends on the number of available processors to the current process.</value>
    public int DegreeOfParallelism { get; set; } = Environment.ProcessorCount;
}
