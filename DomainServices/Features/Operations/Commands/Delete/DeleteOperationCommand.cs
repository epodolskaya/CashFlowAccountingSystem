using MediatR;

namespace DomainServices.Features.Operations.Commands.Delete;

public class DeleteOperationCommand : IRequest<Unit>
{
    public DeleteOperationCommand(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}