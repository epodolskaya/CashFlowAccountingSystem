using ApplicationCore.Entity.BaseEntity;

namespace ApplicationCore.Entity;

public class OperationCategory : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Operation> Operations { get; init; } = new List<Operation>();

    public ICollection<Department> Departments { get; init; } = new List<Department>();
}