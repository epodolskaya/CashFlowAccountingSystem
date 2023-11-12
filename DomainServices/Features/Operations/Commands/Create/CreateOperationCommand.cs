using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Commands.Create;

public class CreateOperationCommand : IRequest<Operation>
{
    public long TypeId { get; set; }

    public long CategoryId { get; set; }

    public string? Comment { get; set; }

    public decimal Sum { get; set; }

    public DateTime Date { get; set; }
}