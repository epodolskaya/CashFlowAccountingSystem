namespace Infrastructure.Identity.Constants;

public class JwtSettings
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public long TokenLifetimeMinutes { get; set; } = 120;
}