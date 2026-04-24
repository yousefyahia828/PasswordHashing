namespace PasswordHashing.Helpers;

internal class PhcHash
{
    public required byte[] Hash { get; init; }
    public required byte[] Salt { get; init; }
    public required int DegreeOfParallelism { get; init; }
    public required int MemorySizeInBytes { get; init; }
    public required int Iterations { get; init; }
}