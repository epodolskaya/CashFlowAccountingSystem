using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetAll;

internal class GetAllOperationsQuery : IRequest<ICollection<Operation>> { }