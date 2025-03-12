using Application.Common.Interfaces.Authentication;
using Domain.Entities;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;



namespace Infrastructure.Common.Authentication;

public class JwtTokenGenerator:IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private  readonly JwtSettings _settings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _settings = jwtSettings.Value;
    }
    
    public string Generator(User user)
    {
        var signingKey = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        var clamis = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        };
        var toke = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            expires: _dateTimeProvider.Now.AddMinutes(_settings.ExpiryMinutes),
            claims: clamis,
            signingCredentials: signingKey

        );
        return new JwtSecurityTokenHandler().WriteToken(toke);
    }
}
