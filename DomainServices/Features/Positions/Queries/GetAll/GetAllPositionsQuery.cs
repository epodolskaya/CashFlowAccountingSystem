using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Positions.Queries.GetAll;

public class GetAllPositionsQuery : IRequest<ICollection<Position>> { }