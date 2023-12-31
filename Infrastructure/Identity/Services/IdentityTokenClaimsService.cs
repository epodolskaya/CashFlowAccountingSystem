﻿using ApplicationCore.Exceptions;
using Infrastructure.Data;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity.Services;

public class IdentityTokenClaimsService : ITokenClaimsService
{
    private readonly JwtSettings _jwtSettings;
    private readonly AccountingSystemContext _repository;
    private readonly UserManager<EmployeeAccount> _userManager;

    public IdentityTokenClaimsService(UserManager<EmployeeAccount> userManager,
                                      IOptions<JwtSettings> settings,
                                      AccountingSystemContext repository)
    {
        _userManager = userManager;
        _repository = repository;
        _jwtSettings = settings.Value;
    }

    public async Task<string> GetTokenAsync(string userEmail)
    {
        EmployeeAccount? user = await _userManager.FindByEmailAsync(userEmail);

        if (user is null)
        {
            throw new EntityNotFoundException($"{nameof(EmployeeAccount)} with email {userEmail} doesn't exist.");
        }

        long departmentId = await _repository.Employees.Where(x => x.Id == user.EmployeeId)
                                             .Select(x => x.DepartmentId)
                                             .SingleOrDefaultAsync();

        ICollection<string> roles = await _userManager.GetRolesAsync(user);

        List<Claim> claims = new List<Claim>
        {
            new Claim(CustomClaimName.AccountId, user.Id.ToString()),
            new Claim(CustomClaimName.EmployeeId, user.EmployeeId.ToString()),
            new Claim(CustomClaimName.DepartmentId, departmentId.ToString()),
            new Claim(ClaimTypes.Email, userEmail)
        };

        foreach (string role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        JwtSecurityToken token =
            new JwtSecurityToken
                (_jwtSettings.Issuer,
                 _jwtSettings.Audience,
                 claims,
                 DateTime.Now,
                 DateTime.Now.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
                 new SigningCredentials
                     (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                      SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}