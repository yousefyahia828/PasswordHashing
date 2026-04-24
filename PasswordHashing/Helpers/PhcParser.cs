namespace PasswordHashing.Helpers;

internal static class PhcParser
{
    public static PhcHash? Parse(string passwordHash)
    {
        try
        {
            string[] parts = passwordHash.Split('$');

            if (parts.Length != 5)
                return null;

            string parameters = parts[2];

            int memorySize = int.Parse(parameters.Split(',')[0]);
            int iterations = int.Parse(parameters.Split(',')[1]);
            int parallelism = int.Parse(parameters.Split(',')[2]);

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
