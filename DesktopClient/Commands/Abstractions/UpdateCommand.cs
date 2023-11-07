using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Commands.Abstractions;

public abstract class UpdateCommand<T> where T : StorableEntity
{
    public long Id { get; set; }
}