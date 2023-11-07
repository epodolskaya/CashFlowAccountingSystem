using DesktopClient.Commands.Abstractions;
using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.RequestingService.Abstractions;

internal interface IRequestingService<T> : IReadOnlyRequestingService<T> where T : StorableEntity
{
    Task<T> CreateAsync(CreateCommand<T> createCommand);

    Task<T> UpdateAsync(UpdateCommand<T> createCommand);

    Task DeleteAsync(long id);
}