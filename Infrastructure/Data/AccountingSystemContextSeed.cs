using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AccountingSystemContextSeed
{
    public static async Task SeedAsync(AccountingSystemContext context)
    {
        if (!await context.Positions.AnyAsync())
        {
            Position[] positions =
            {
                new Position
                {
                    Name = "Финансовый аналитик"
                },
                new Position
                {
                    Name = "Глава отдела"
                },
                new Position
                {
                    Name = "Бухгалтер"
                },
                new Position
                {
                    Name = "Экономист"
                },
                new Position
                {
                    Name = "HR"
                }
            };

            await context.Positions.AddRangeAsync(positions);
            await context.SaveChangesAsync();
        }

        if (!await context.Employees.AnyAsync())
        {
            Employee[] employees = new Employee[]
            {
                new Employee()
                {
                    DateOfBirth = DateTime.Parse("02.02.2003"),
                    Name = "Сергей",
                    Surname = "Павлов",
                    PhoneNumber = "+375295578608",
                    Position = await context.Positions.SingleAsync(x => x.Name == "Финансовый аналитик"),
                    Salary = 3000
                },
                new Employee()
                {
                    DateOfBirth = DateTime.Parse("21.03.1990"),
                    Name = "Анатолий",
                    Surname = "Карпов",
                    PhoneNumber = "+375332261605",
                    Position = await context.Positions.SingleAsync(x => x.Name == "Глава отдела"),
                    Salary = 4000
                },
            };

            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }

        if (!await context.OperationTypes.AnyAsync())
        {
            OperationType[] types =
            {
                new OperationType
                {
                    Name = "Доходы"
                },
                new OperationType
                {
                    Name = "Расходы"
                }
            };

            await context.OperationTypes.AddRangeAsync(types);
            await context.SaveChangesAsync();
        }

        if (!await context.OperationCategories.AnyAsync())
        {
            OperationCategory[] categories =
            {
                new OperationCategory
                {
                    Name = "Продажа товаров и услуг"
                },
                new OperationCategory
                {
                    Name = "Инвестиции"
                },
                new OperationCategory
                {
                    Name = "Продажа активов"
                },
                new OperationCategory
                {
                    Name = "Выплата заработной платы"
                },
                new OperationCategory
                {
                    Name = "Закупка сырья и материалов"
                },
                new OperationCategory
                {
                    Name = "Реклама и маркетинг"
                },
                new OperationCategory
                {
                    Name = "Аренда помещения"
                },
                new OperationCategory
                {
                    Name = "Налоги"
                }
            };

            await context.OperationCategories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Operations.AnyAsync())
        {
            Operation[] operations =
            {
                new Operation
                {
                    Category = await context.OperationCategories.SingleAsync(x => x.Name == "Продажа товаров и услуг"),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Доходы"),
                    Comment = "Продажа излишек заготовок производства",
                    Date = DateTime.Today,
                    Sum = 5000
                },
                new Operation
                {
                    Category = await context.OperationCategories.SingleAsync(x => x.Name == "Инвестиции"),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Доходы"),
                    Comment = "Получение дивидендов",
                    Date = DateTime.Today,
                    Sum = 1000
                },
                new Operation
                {
                    Category = await context.OperationCategories.SingleAsync(x => x.Name == "Аренда помещения"),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                    Comment = "Выплата аренды офисного помещения",
                    Date = DateTime.Today,
                    Sum = 10000
                }
            };

            await context.Operations.AddRangeAsync(operations);
            await context.SaveChangesAsync();
        }
    }
}