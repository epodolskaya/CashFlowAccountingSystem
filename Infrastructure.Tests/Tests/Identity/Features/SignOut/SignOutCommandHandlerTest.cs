using Infrastructure.Identity.Features.SignOut;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests.Tests.Identity.Features.SignOut;

[TestClass]
[TestSubject(typeof(SignOutCommandHandler))]
public class SignOutCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task NotThrowException()
    {
        await _mediator.Send(new SignOutCommand());
    }
}