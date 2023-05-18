namespace Jira.Service.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> GenerateTokenAsync(string username, string password);
    }
}
