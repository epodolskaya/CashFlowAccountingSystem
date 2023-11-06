using MediatR;

namespace DomainServices.Features.Operations.Commands.Delete;

internal class DeleteOperationCommand : IRequest<Unit>
{
    public long Id { get; set; }
}