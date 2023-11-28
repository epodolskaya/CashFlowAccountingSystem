namespace DesktopClient.Commands.Abstractions;

public abstract class UpdateCommand<T>
{
    public long Id { get; set; }
}