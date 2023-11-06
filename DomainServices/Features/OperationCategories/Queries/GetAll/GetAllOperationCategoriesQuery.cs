using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetAll;

internal class GetAllOperationCategoriesQuery : IRequest<ICollection<OperationCategory>> { }