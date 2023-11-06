using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetAll;

internal class GetAllPositionsQuery : IRequest<ICollection<Position>> { }