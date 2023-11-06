using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

internal class GetOperationCategoryByIdQuery : IRequest<OperationCategory>
{
    public long Id { get; set; }
}