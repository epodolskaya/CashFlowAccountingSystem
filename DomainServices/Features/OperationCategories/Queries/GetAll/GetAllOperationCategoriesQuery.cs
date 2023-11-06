using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationCategories.Queries.GetAll;

public class GetAllOperationCategoriesQuery : IRequest<ICollection<OperationCategory>> { }