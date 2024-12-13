using authentication.service.Models;

namespace authentication.service.Extensions;

public static class ConfigurationExtensions
{
    public static AdminCredentials GetAdminCredentials(this IConfiguration configuration)
    {
        return new AdminCredentials(configuration["JwtSettings:Username"], configuration["JwtSettings:Password"]);
    }
    
}
