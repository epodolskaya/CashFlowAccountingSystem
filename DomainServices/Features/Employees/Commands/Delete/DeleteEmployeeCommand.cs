using MediatR;

namespace DomainServices.Features.Employees.Commands.Delete;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public DeleteEmployeeCommand(long id)
    {
        Id = id;
    }
}