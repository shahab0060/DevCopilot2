using DevCopilot2.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevCopilot2.API.Helpers;

public static class JwtHelper
{
    public static string GenerateJwtToken(List<Claim> claims, JwtSettings jwtSettings)
    {

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.TokenExpiryInMinutes),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey)), SecurityAlgorithms.HmacSha256));

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenOptions);
    }


}
