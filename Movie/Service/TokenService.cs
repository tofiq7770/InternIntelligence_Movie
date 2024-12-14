using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Movie.Models;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        // Retrieve the secret key from configuration (appsettings.json)
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    public Task<Token> CreateAccessTokenAsync(TokenCreationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Token> CreateIdentityTokenAsync(TokenCreationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<string> CreateSecurityTokenAsync(Token token)
    {
        throw new NotImplementedException();
    }

    // Generate JWT Token for the user
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        // Create signing credentials with the secret key
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7), // Token expiration
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token); // Return the generated token as a string
    }
}
