using MediatR;

namespace DomainServices.Features.Operations.Commands.Delete;

public class DeleteOperationCommand : IRequest<Unit>
{
    public long Id { get; set; }

    public DeleteOperationCommand(long id)
    {
        Id = id;
    }
}