using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Commands.Abstractions;

public abstract class CreateCommand<T> where T : StorableEntity { }