using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RestaurantReservation.Application.Abstractions.Authentication;
using RestaurantReservation.Domain.Abstractions;
using RestaurantReservation.Domain.Users;
using RestaurantReservation.Infrastructure.Authentication.Models;

namespace RestaurantReservation.Infrastructure.Authentication;

public class AuthenticationService:IAuthenticationService
{
    private static readonly Error UserExists = new(
        "UserExists",
        "Email is already taken");
    private static readonly Error UserCannotCreated = new(
        "UserCannotCreated",
        "User cannot be created");
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityTokenClaimService _tokenClaimService;

    public AuthenticationService(UserManager<ApplicationUser> userManager,
        IdentityTokenClaimService tokenClaimService)
    {
        _userManager = userManager;
        _tokenClaimService = tokenClaimService;
    }
    public async Task<Result<string>> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        
        var existingUser = await _userManager.FindByNameAsync(user.Email);
        if (existingUser != null) 
            return Result.Failure<string>(UserExists);
        var applicationUser = new ApplicationUser()
        {
            Email = user.Email,
            UserName = user.Email,
        };
        
        var result = await _userManager.CreateAsync(applicationUser,password);
        if (!result.Succeeded)
            return Result.Failure<string>(UserCannotCreated);

        var token =await  _tokenClaimService.GetTokenAsync(applicationUser.UserName);
        return token;

    }
}