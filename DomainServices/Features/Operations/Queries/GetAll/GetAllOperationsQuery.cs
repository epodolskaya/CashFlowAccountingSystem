using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Operations.Queries.GetAll;

public class GetAllOperationsQuery : IRequest<ICollection<Operation>> { }