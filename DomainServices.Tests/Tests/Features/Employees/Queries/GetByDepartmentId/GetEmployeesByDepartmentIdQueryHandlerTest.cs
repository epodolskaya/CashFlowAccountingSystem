using ApplicationCore.Entity;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GetEmployeesByDepartmentIdQuery =
    DomainServices.Features.Employees.Queries.GetByDepartmentId.GetEmployeesByDepartmentIdQuery;

namespace DomainServices.Tests.Tests.Features.Employees.Queries.GetByDepartmentId;

[TestClass]
public class GetEmployeesByDepartmentIdQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<Employee> result = await _mediator.Send(new GetEmployeesByDepartmentIdQuery(1));

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidParameter()
    {
        await Assert.ThrowsExceptionAsync<ValidationException>
            (async () => await _mediator.Send(new GetEmployeesByDepartmentIdQuery(-1)));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetEmployeesByDepartmentIdQuery(1), new CancellationToken(true)));
    }
}