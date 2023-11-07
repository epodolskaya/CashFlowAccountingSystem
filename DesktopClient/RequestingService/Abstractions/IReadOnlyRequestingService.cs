using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.RequestingService.Abstractions;

internal interface IReadOnlyRequestingService<T> where T : StorableEntity
{
    Task<ICollection<T>> GetAllAsync();

    Task<T> GetByIdAsync(long id);
}