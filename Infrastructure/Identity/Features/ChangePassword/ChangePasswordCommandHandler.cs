using ApplicationCore.Exceptions;
using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Features.ChangePassword;
internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly UserManager<EmployeeAccount> _employeeAccountManager;

    private readonly IHttpContextAccessor _contextAccessor;

    public ChangePasswordCommandHandler(UserManager<EmployeeAccount> employeeAccountManager, IHttpContextAccessor contextAccessor)
    {
        _employeeAccountManager = employeeAccountManager;
        _contextAccessor = contextAccessor;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        string email = _contextAccessor.HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.Email).Value;

        EmployeeAccount? account = await _employeeAccountManager.FindByNameAsync(email);

        if (account is null)
        {
            throw new EntityNotFoundException($"{nameof(EmployeeAccount)} with email:{email} doesn't exist.");
        }

        IdentityResult result = await _employeeAccountManager.ChangePasswordAsync(account, request.OldPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new OperationFailureException(string.Join(Environment.NewLine, result.Errors.Select(x => x.Description)));
        }

        return Unit.Value;
    }
}