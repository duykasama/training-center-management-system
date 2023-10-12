using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FAMS.V0.Shared.Entities;
using FAMS.V0.Shared.Settings;
using Microsoft.IdentityModel.Tokens;

namespace FAMS.V0.Services.AuthenticationService.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public (string accessToken, string refreshToken) GenerateToken(User user)
    {
        var jwtSettings = _configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Role", user.Role.ToString())
        };

        var accessToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(20)),
            signingCredentials: credentials
        );
        
        var refreshToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromHours(8)),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return (tokenHandler.WriteToken(accessToken), tokenHandler.WriteToken(refreshToken));
    }
}