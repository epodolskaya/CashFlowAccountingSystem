using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationTypes.Queries.GetAll;

internal class GetAllOperationsTypesQuery : IRequest<ICollection<OperationType>> { }