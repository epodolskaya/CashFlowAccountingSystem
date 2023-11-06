using MediatR;

namespace DomainServices.Features.Employees.Commands.Delete;

internal class DeleteEmployeeCommand : IRequest<Unit>
{
    public long Id { get; set; }
}