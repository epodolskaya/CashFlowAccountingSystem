using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;
public class Department : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; init; } = new List<Employee>();

    public ICollection<Operation> Operations { get; init; } = new List<Operation>();

    public ICollection<OperationCategory> OperationCategories { get; init; } = new List<OperationCategory>();
}
