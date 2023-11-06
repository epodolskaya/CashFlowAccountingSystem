using ApplicationCore.Entity.BaseEntity;

namespace ApplicationCore.Entity;

public class Position : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; } = new List<Employee>();
}