using ApplicationCore.Entity.BaseEntity;

namespace ApplicationCore.Entity;

public class Operation : StorableEntity
{
    public long TypeId { get; set; }

    public OperationType Type { get; set; }

    public long CategoryId { get; set; }

    public OperationCategory Category { get; set; }

    public string Comment { get; set; }

    public decimal Sum { get; set; }

    public DateTime Date { get; set; }

    public long DepartmentId { get; set; }

    public Department Department { get; set; }
}