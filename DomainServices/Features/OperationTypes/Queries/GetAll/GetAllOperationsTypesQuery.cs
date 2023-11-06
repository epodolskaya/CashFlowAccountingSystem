using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.OperationTypes.Queries.GetAll;

public class GetAllOperationsTypesQuery : IRequest<ICollection<OperationType>> { }