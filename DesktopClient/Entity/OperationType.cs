using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;

public class OperationType : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Operation> Operations { get; init; } = new List<Operation>();
}