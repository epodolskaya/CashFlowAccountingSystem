using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;

public class OperationCategory : StorableEntity
{
    public string Name { get; set; }

    public long TypeId { get; set; }
    public OperationType Type { get; set; }

    public ICollection<Operation> Operations { get; init; } = new List<Operation>();

    public ICollection<Department> Departments { get; init; } = new List<Department>();
}