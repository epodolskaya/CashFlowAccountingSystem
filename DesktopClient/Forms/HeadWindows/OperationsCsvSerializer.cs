using DesktopClient.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Forms.HeadWindows;
static internal class OperationsCsvSerializer
{
    private const char Separator = ';';

    private static readonly string Header = string.Join
        (Separator,
         nameof(Operation.Id),
         nameof(Operation.Sum),
         nameof(Operation.DepartmentId),
         nameof(Operation.Date),
         nameof(Operation.Comment),
         nameof(Operation.CategoryId));

    public static IEnumerable<string> Serialize(IEnumerable<Operation> operations)
    {
        IEnumerable<string> content = operations.Select
            (x => string.Join
                (Separator,
                 x.Id,
                 x.Sum,
                 x.DepartmentId,
                 x.Date.ToString("dd.MM.yyyy"),
                 x.Comment,
                 x.CategoryId));

        return content.Prepend(Header);
    }

    public static IEnumerable<Operation> Deserialize(IEnumerable<string> lines)
    {
        if (lines.First() != Header)
        {
            throw new Exception("Неверный формат файла");
        }

        return lines.Skip(1).Select
            (x =>
            {
                string[] properties = x.Split(Separator);

                return new Operation()
                {
                    Id = long.Parse(properties[0]),
                    Sum = decimal.Parse(properties[1]),
                    DepartmentId = long.Parse(properties[2]),
                    Date = DateTime.Parse(properties[3]),
                    Comment = properties[4],
                    CategoryId = long.Parse(properties[5])
                };
            });
    }
}