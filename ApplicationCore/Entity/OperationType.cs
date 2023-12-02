using ApplicationCore.Entity.BaseEntity;

namespace ApplicationCore.Entity;

public class OperationType : StorableEntity
{
    public string Name { get; set; }

    public ICollection<OperationCategory> OperationCategories { get; init; } = new List<OperationCategory>();
}