using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using Infrastructure.Data;
using Infrastructure.Identity.Entity;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Features.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    private readonly AccountingSystemContext _repository;

    public RegisterCommandHandler(IAuthorizationService authorizationService, AccountingSystemContext repository)
    {
        _authorizationService = authorizationService;
        _repository = repository;
    }

    private Task<bool> IsEmployeeExistsAsync(long employeeId)
    {
        return _repository.Employees.AnyAsync(x => x.Id == employeeId);
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!await IsEmployeeExistsAsync(request.EmployeeId))
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.EmployeeId} doesn't exist.");
        }

        EmployeeAccount newAccount = new EmployeeAccount
        {
            UserName = request.Email,
            PasswordHash = request.Password,
            Email = request.Email,
            EmployeeId = request.EmployeeId
        };

        await _authorizationService.RegisterAsync(newAccount);

        return Unit.Value;
    }
}