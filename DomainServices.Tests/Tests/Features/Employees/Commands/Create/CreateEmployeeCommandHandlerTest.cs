using ApplicationCore.Entity;
using DomainServices.Features.Employees.Commands.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Employees.Commands.Create;

[TestClass]
public class CreateEmployeeCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            Name = "Елизавета",
            Surname = "Подольская",
            PhoneNumber = "+375296555891",
            DepartmentId = 1,
            PositionId = 1,
            DateOfBirth = DateTime.Parse("21.03.2003"),
            Salary = 3200
        };

        Employee result = await _mediator.Send(command);

        Assert.IsNotNull(result);
    }

    [TestMethod]
    public async Task InvalidRequest()
    {
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            Name = string.Empty,
            Surname = string.Empty,
            PhoneNumber = "+375296555891000000",
            DepartmentId = -1,
            PositionId = -1,
            DateOfBirth = DateTime.MaxValue,
            Salary = -1
        };

        ValidationException exception = await Assert.ThrowsExceptionAsync<ValidationException>
                                            (async () => await _mediator.Send(command));

        Assert.AreEqual(7, exception.Errors.Count());
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            Name = "Елизавета",
            Surname = "Подольская",
            PhoneNumber = "+375296555891",
            DepartmentId = 1,
            PositionId = 1,
            DateOfBirth = DateTime.Parse("21.03.2003"),
            Salary = 3200
        };

        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send(command, new CancellationToken(true)));
    }
}