using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Service;
using Microsoft.IdentityModel.Tokens;
using Presentation.Entities;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;


namespace Infrastructure.Authentiscation;

using Microsoft.Extensions.Options;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _provider;

    public JwtTokenGenerator(IDateTimeProvider provider, IOptions<JwtSettings> jwtSettings)
    {
        _provider = provider;
        _jwtSettings = jwtSettings.Value; // Lấy giá trị từ IOptions
    }

    
    public string GenerateJwtToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );



        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };



       var token = new JwtSecurityToken(
           issuer: _jwtSettings.Issuer,
           audience: _jwtSettings.Audience,
           expires: _provider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
           claims: claims,
           signingCredentials: signingCredentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
