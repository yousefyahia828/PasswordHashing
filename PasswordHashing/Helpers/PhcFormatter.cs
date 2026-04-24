using System.Text;

namespace PasswordHashing.Helpers;

internal static class PhcFormatter
{
    public static string Format(PhcHash phcHash)
    {
        StringBuilder builder = new();

        builder.Append("$argon2id");
        builder.Append("$v=19");
        builder.Append($"$m={phcHash.MemorySizeInBytes},t={phcHash.Iterations},p={phcHash.DegreeOfParallelism}");
        builder.Append($"${Convert.ToBase64String(phcHash.Salt)}");
        builder.Append($"${Convert.ToBase64String(phcHash.Hash)}");

        return builder.ToString();
    }
}
