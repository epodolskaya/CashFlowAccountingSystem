using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;

public class Position : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; init; } = new List<Employee>();
}