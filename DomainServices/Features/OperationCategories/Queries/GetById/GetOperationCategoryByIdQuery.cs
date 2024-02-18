using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetById;

public class GetOperationCategoryByIdQuery : IRequest<OperationCategory>
{
    public GetOperationCategoryByIdQuery(long id)
    {
        Id = id;
    }

    public long Id { get; set; }
}