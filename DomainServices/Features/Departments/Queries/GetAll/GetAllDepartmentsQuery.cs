using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Departments.Queries.GetAll;

public class GetAllDepartmentsQuery : IRequest<ICollection<Department>> { }