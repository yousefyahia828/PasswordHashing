using Microsoft.Extensions.DependencyInjection;
using PasswordHashing.Hasher;

namespace PasswordHashing.Extensions;

/// <summary>
/// Provides extension methods for registering password hashing services
/// and configuring related options in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the <see cref="IPasswordHasher"/> service and allows
    /// configuration of <see cref="PasswordHashOptions"/>.
    /// </summary>
    /// <param name="services">The dependency injection service collection.</param>
    /// <param name="configuration">
    /// An optional delegate to configure <see cref="PasswordHashOptions"/>.
    /// If not provided, default secure values will be used.
    /// </param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <remarks>
    /// This method applies default hashing settings, which can be overridden
    /// by the provided <paramref name="configuration"/> delegate.
    /// </remarks>
    public static IServiceCollection AddPasswordHasher(
        this IServiceCollection services,
        Action<PasswordHashOptions> configuration)
    {
        services.Configure<PasswordHashOptions>(options =>
        {
            options.SaltSize = 16;
            options.HashSize = 32;
            options.Iterations = 3;
            options.MemorySizeInBytes = 65536;
            options.DegreeOfParallelism = 2;
        });

        if (configuration is not null)
            services.Configure(configuration);

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }

    /// <summary>
    /// Registers the <see cref="IPasswordHasher"/> service using default configuration values.
    /// </summary>
    /// <param name="services">The dependency injection service collection.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddPasswordHasher(
        this IServiceCollection services)
    {
        return services.AddPasswordHasher(_ => { });
    }
}