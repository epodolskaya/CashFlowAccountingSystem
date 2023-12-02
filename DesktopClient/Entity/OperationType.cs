using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;

public class OperationType : StorableEntity
{
    public string Name { get; set; }

    public ICollection<OperationCategory> OperationCategories { get; init; } = new List<OperationCategory>();
}