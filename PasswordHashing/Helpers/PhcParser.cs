namespace PasswordHashing.Helpers;

internal static class PhcParser
{
    public static PhcHash? Parse(string passwordHash)
    {
        try
        {
            string[] parts = passwordHash.Split('$', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 5)
                return null;

            string[] parameters = parts[2].Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length != 3)
                return null;

            int memorySize = int.Parse(parameters[0].Split('=')[1]);
            int iterations = int.Parse(parameters[1].Split('=')[1]);
            int parallelism = int.Parse(parameters[2].Split('=')[1]);

            byte[] salt = Convert.FromBase64String(parts[3]);
            byte[] hash = Convert.FromBase64String(parts[4]);

            return new PhcHash
            {
                Hash = hash,
                Salt = salt,
                Iterations = iterations,
                DegreeOfParallelism = parallelism,
                MemorySizeInBytes = memorySize
            };
        }
        catch
        {
            return null;
        }
    }
}
