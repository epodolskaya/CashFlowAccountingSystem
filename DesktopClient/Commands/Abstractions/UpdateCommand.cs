using DesktopClient.Entity.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Commands.Abstractions;
public abstract class UpdateCommand<T> where T : StorableEntity
{
    public long Id { get; set; }
}
