namespace DevCopilot2.Shared.Models;
public class AppConfig
{
    public ConnectionStrings ConnectionString { get; set; }
    public Endpoints Endpoints { get; set; } = new Endpoints();
    public JwtSettings DefaultJwtSettings { get; set; } = new JwtSettings();
}

public class ConnectionStrings
{
    public string DefaultConnectionString { get; set; }

}
public class Endpoints
{
    public string Api { get; set; }
    public string Web { get; set; }
}
public class JwtSettings
{
    public string SecurityKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int TokenExpiryInMinutes { get; set; }
    public int RefreshTokenExpiryInDay { get; set; }
    public bool ValidateLifetime { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateAudience { get; set; }
}
