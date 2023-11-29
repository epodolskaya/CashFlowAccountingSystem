using ApplicationCore.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.Features.Departments.Queries.GetAll;
public class GetAllDepartmentsQuery : IRequest<ICollection<Department>>
{
}