namespace Nabeey.Service.Interfaces;

public interface IAuthService
{
    ValueTask<string> GenerateTokenAsync(string phone, string originalPassword);
}
