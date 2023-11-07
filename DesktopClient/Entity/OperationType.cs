﻿using DesktopClient.Entity.BaseEntity;

namespace DesktopClient.Entity;

public class OperationType : StorableEntity
{
    public string Name { get; set; }

    public ICollection<Operation> Operations { get; } = new List<Operation>();
}