﻿using ApplicationCore.Entity;
using MediatR;

namespace DomainServices.Features.Employees.Queries.GetByDepartmentId;

public class GetEmployeesByDepartmentIdQuery : IRequest<ICollection<Employee>>
{
    public long DepartmentId { get; init; }

    public GetEmployeesByDepartmentIdQuery(long departmentId)
    {
        DepartmentId = departmentId;
    }
}