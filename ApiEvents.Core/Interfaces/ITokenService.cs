namespace ApiEvents.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateTokenEvents(string name, string permission);
    }
}