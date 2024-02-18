using ApplicationCore.Exceptions;
using FluentValidation;
using Infrastructure.Identity.Features.Register;
using Infrastructure.Identity.Features.SignIn;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Infrastructure.Tests.Tests.Identity.Features.Register;

[TestClass]
[TestSubject(typeof(RegisterCommandHandler))]
public class RegisterCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [Test]
    public async Task ValidRequest()
    {
        await _mediator.Send
            (new RegisterCommand
            {
                Email = "head1@gmail.com",
                Password = "P@ssword1",
                ConfirmPassword = "P@ssword1",
                EmployeeId = 1
            });
    }

    [Test]
    public async Task InvalidRequest()
    {
        ValidationException? exception = await Assert.ThrowsExceptionAsync<ValidationException>
                                             (async () => await _mediator.Send
                                                              (new RegisterCommand
                                                              {
                                                                  Email = string.Empty,
                                                                  Password = string.Empty,
                                                                  ConfirmPassword = string.Empty,
                                                                  EmployeeId = 0
                                                              }));

        Assert.AreEqual(9, exception.Errors.Count());
    }

    [Test]
    public async Task InvalidPassword()
    {
        await Assert.ThrowsExceptionAsync<AuthorizationException>
            (async () => await _mediator.Send
                             (new SignInCommand
                             {
                                 UserName = "head@gmail.com",
                                 Password = "Invalid password"
                             }));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send
                             (new RegisterCommand
                              {
                                  Email = string.Empty,
                                  Password = string.Empty,
                                  ConfirmPassword = string.Empty,
                                  EmployeeId = 0
                              },
                              new CancellationToken(true)));
    }
}