using ApplicationCore.Exceptions;
using Infrastructure.Identity.Features.DeleteAccount;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests.Tests.Identity.Features.DeleteAccount;

[TestClass]
[TestSubject(typeof(DeleteAccountCommandHandler))]
public class DeleteAccountCommandHandlerTest
{
    private readonly IMediator _mediator = DependencyContainer.GetServiceProvider().GetRequiredService<IMediator>();

    [TestMethod]
    public async Task ValidRequest()
    {
        await _mediator.Send
            (new DeleteAccountCommand
            {
                UserName = "head@gmail.com"
            });
    }

    [TestMethod]
    public async Task InvalidUserName()
    {
        await Assert.ThrowsExceptionAsync<EntityNotFoundException>
            (async () => await _mediator.Send
                             (new DeleteAccountCommand
                             {
                                 UserName = "NotExistingUserName"
                             }));
    }

    [TestMethod]
    public async Task CancellationSupported()
    {
        await Assert.ThrowsExceptionAsync<OperationCanceledException>
            (async () => await _mediator.Send
                             (new DeleteAccountCommand
                              {
                                  UserName = "head@gmail.com"
                              },
                              new CancellationToken(true)));
    }
}