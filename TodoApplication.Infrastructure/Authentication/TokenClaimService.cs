using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoApplication.Infrastructure.Authentication.Models;

namespace TodoApplication.Infrastructure.Authentication;

public class IdentityTokenClaimService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AuthenticationOptions _authenticationOptions;


    public IdentityTokenClaimService(UserManager<ApplicationUser> userManager,
        IOptions<AuthenticationOptions> authenticationOptions)
    {
        this._userManager = userManager;
        this._authenticationOptions = authenticationOptions.Value;
    }

    public async Task<string> GetTokenAsync(string userName)
    {

        var user = await _userManager.FindByNameAsync(userName);
        if(user == null)
            return null;
        var roles = await _userManager.GetRolesAsync(user);


        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.SecretKey));

        var token = new JwtSecurityToken(
            issuer: _authenticationOptions.Issuer,
            audience: _authenticationOptions.Audience,
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;

    }
}