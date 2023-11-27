using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;

public class Employee : StorableEntity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PhoneNumber { get; set; }

    public decimal Salary { get; set; }

    public long PositionId { get; set; }

    public Position Position { get; set; }

    public long DepartmentId { get; set; }

    public Department Department { get; set; }
}