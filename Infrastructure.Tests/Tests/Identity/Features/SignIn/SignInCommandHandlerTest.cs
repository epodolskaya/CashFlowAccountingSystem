using ApplicationCore.Exceptions;
using FluentValidation;
using Infrastructure.Identity.Features.SignIn;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Infrastructure.Tests.Tests.Identity.Features.SignIn;

[TestFixture]
[TestOf(typeof(SignInCommandHandler))]
public class SignInCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [Test]
    public async Task ValidRequest()
    {
        await _mediator.Send
            (new SignInCommand
            {
                UserName = "head@gmail.com",
                Password = "P@ssword1"
            });
    }

    [Test]
    public async Task InvalidRequest()
    {
        ValidationException? exception = await Assert.ThrowsExceptionAsync<ValidationException>
                                             (async () => await _mediator.Send
                                                              (new SignInCommand
                                                              {
                                                                  UserName = string.Empty,
                                                                  Password = string.Empty
                                                              }));

        Assert.AreEqual(exception.Errors.Count(), 2);
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
                             (new SignInCommand
                              {
                                  UserName = "head@gmail.com",
                                  Password = "Invalid password"
                              },
                              new CancellationToken(true)));
    }
}