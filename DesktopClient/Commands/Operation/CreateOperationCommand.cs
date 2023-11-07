using DesktopClient.Commands.Abstractions;

namespace DesktopClient.Commands.Operation;

public class CreateOperationCommand : CreateCommand<Entity.Operation>
{
    public long TypeId { get; set; }

    public long CategoryId { get; set; }

    public string Comment { get; set; }

    public decimal Sum { get; set; }

    public DateTime Date { get; set; }
}