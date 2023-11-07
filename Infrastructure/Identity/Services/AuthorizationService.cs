using ApplicationCore.Exceptions;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSettings _jwtSettings;

    private readonly ITokenClaimsService _tokenClaimsService;

    private readonly UserManager<EmployeeAccount> _userManager;

    public AuthorizationService(ITokenClaimsService tokenClaimsService,
                                UserManager<EmployeeAccount> userManager,
                                IHttpContextAccessor contextAccessor,
                                IOptions<JwtSettings> settings)
    {
        _tokenClaimsService = tokenClaimsService;
        _userManager = userManager;
        _httpContextAccessor = contextAccessor;
        _jwtSettings = settings.Value;
    }

    public async Task SignInAsync(string email, string password)
    {
        EmployeeAccount? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new AuthorizationException("Invalid login or password.");
        }

        bool passwordCorrect = await _userManager.CheckPasswordAsync(user, password);

        if (!passwordCorrect)
        {
            throw new AuthorizationException("Invalid login or password.");
        }

        string token = await _tokenClaimsService.GetTokenAsync(user.Email);

        _httpContextAccessor.HttpContext.Response.Headers.Add("Authorization", "Bearer " + token);
    }

    public async Task<long> RegisterAsync(EmployeeAccount user)
    {
        IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash);

        if (!result.Succeeded)
        {
            throw new AuthorizationException
                ($"Unable to register such user. Reasons: {string.Join(',', result.Errors.Select(x => x.Description))}");
        }

        await _userManager.AddToRoleAsync(user, RoleName.DepartmentHead);

        return user.Id;
    }

    public Task SingOutAsync()
    {
        _httpContextAccessor.HttpContext?.Request.Headers.Remove("Authorization");

        return Task.CompletedTask;
    }
}