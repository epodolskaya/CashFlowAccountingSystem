using DesktopClient.Commands.Abstractions;
using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.RequestingService.Abstractions;

internal interface IRequestingService<TEntity> : IReadOnlyRequestingService<TEntity> where TEntity : StorableEntity
{
    Task<TEntity> CreateAsync<TCommand>(CreateCommand<TEntity> createCommand);

    Task<TEntity> UpdateAsync<TCommand>(UpdateCommand<TEntity> createCommand);

    Task DeleteAsync(long id);
}