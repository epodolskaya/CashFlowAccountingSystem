using ApplicationCore.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entity;
public class Department : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Employee> Employees { get; init; } = new List<Employee>();

    public ICollection<Operation> Operations { get; init; } = new List<Operation>();

    public ICollection<OperationCategory> OperationCategories { get; init; } = new List<OperationCategory>();
}
