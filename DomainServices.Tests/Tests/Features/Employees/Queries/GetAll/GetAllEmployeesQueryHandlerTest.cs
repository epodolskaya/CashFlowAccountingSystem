using ApplicationCore.Entity;
using DomainServices.Features.Employees.Queries.GetAll;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Employees.Queries.GetAll;

[TestClass]
public class GetAllEmployeesQueryHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ReturnsNotNull()
    {
        ICollection<Employee> result = await _mediator.Send(new GetAllEmployeesQuery());

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(new GetAllEmployeesQuery(), new CancellationToken(true)));
    }
}