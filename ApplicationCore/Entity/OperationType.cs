﻿using ApplicationCore.Entity.BaseEntity;

namespace ApplicationCore.Entity;
public class OperationType : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Operation> Operations { get; } = new List<Operation>();
}