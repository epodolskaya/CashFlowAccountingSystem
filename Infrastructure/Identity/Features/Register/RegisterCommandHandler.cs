using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infrastructure.Identity.Entity;
using Infrastructure.Identity.Interfaces;
using MediatR;

namespace Infrastructure.Identity.Features.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IAuthorizationService _authorizationService;

    private readonly IReadOnlyRepository<Employee> _employeeRepository;

    public RegisterCommandHandler(IAuthorizationService authorizationService, IReadOnlyRepository<Employee> employeeRepository)
    {
        _authorizationService = authorizationService;
        _employeeRepository = employeeRepository;
    }

    private Task<bool> IsEmployeeExistsAsync(long employeeId)
    {
        return _employeeRepository.ExistsAsync(x => x.Id == employeeId);
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!await IsEmployeeExistsAsync(request.EmployeeId))
        {
            throw new EntityNotFoundException($"{nameof(Employee)} with id:{request.EmployeeId} doesn't exist.");
        }

        EmployeeAccount newAccount = new EmployeeAccount()
        {
            UserName = request.Email,
            PasswordHash = request.Password,
            Email = request.Email,
            EmployeeId = request.EmployeeId,
        };

        await _authorizationService.RegisterAsync(newAccount);
        
        return Unit.Value;
    }
}