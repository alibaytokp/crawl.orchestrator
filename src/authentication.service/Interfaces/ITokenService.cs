namespace authentication.service.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(string username);
}
