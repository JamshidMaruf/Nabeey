namespace Nabeey.Service.Interfaces;

public interface IAuthService
{
    Task<string> GenerateTokenAsync(string phone, string originalPassword);
}
