using MediatR;

namespace DomainServices.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public DeleteEmployeeCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}