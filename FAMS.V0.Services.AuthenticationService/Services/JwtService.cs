using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Settings;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace FAMS.V0.Services.AuthenticationService.Services;

public class JwtService
{
    private readonly JwtSettings _jwtSettings;

    public JwtService(IConfiguration configuration)
    {
        _jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ?? new JwtSettings();
    }
    
    public (string accessToken, string refreshToken) GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Role", user.Role.ToString())
        };

        var accessToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(20)),
            signingCredentials: credentials
        );
        
        var refreshToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromHours(8)),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return (tokenHandler.WriteToken(accessToken), tokenHandler.WriteToken(refreshToken));
    }

    public string RefreshToken(string refreshToken)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(refreshToken);

        if (jwtSecurityToken.ValidTo < DateTime.Now)
        {
            throw new SecurityTokenExpiredException();
        }

        var newToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: jwtSecurityToken.Claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(20)),
            signingCredentials: credentials
            
        );

        return tokenHandler.WriteToken(newToken);
    }
}