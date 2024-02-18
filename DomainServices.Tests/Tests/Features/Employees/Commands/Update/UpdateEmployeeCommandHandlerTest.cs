using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using DomainServices.Features.Employees.Commands.Update;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainServices.Tests.Tests.Features.Employees.Commands.Update;

[TestClass]
public class UpdateEmployeeCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            Id = 1,
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
        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            Id = 0,
            Name = string.Empty,
            Surname = string.Empty,
            PhoneNumber = string.Empty,
            DepartmentId = 0,
            PositionId = 0,
            DateOfBirth = DateTime.MaxValue,
            Salary = -1
        };

        ValidationException exception = await Assert.ThrowsExceptionAsync<ValidationException>
                                            (async () => await _mediator.Send(command));

        Assert.AreEqual(8, exception.Errors.Count());
    }

    [TestMethod]
    public async Task UpdateNotExistingEntity()
    {
        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            Id = long.MaxValue,
            Name = "Елизавета",
            Surname = "Подольская",
            PhoneNumber = "+375296555891",
            DepartmentId = 1,
            PositionId = 1,
            DateOfBirth = DateTime.Parse("21.03.2003"),
            Salary = 3200
        };

        await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () => await _mediator.Send(command));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        UpdateEmployeeCommand command = new UpdateEmployeeCommand
        {
            Id = 1,
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