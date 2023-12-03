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

        if (!await context.Departments.AnyAsync())
        {
            Department[] departments =
            {
                new Department
                {
                    Name = "Бухгалтерия"
                },
                new Department
                {
                    Name = "Производственный отдел"
                },
                new Department
                {
                    Name = "Отдел продаж"
                },
                new Department
                {
                    Name = "Отдел логистики"
                }
            };

            await context.Departments.AddRangeAsync(departments);
            await context.SaveChangesAsync();
        }

        if (!await context.Employees.AnyAsync())
        {
            Employee[] employees =
            {
                new Employee
                {
                    DateOfBirth = DateTime.Parse("02.02.2003"),
                    Name = "Сергей",
                    Surname = "Павлов",
                    PhoneNumber = "+375295578608",
                    Position = await context.Positions.SingleAsync(x => x.Name == "Финансовый аналитик"),
                    Department = await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия"),
                    Salary = 3000
                },
                new Employee
                {
                    DateOfBirth = DateTime.Parse("21.03.1990"),
                    Name = "Анатолий",
                    Surname = "Карпов",
                    PhoneNumber = "+375332261605",
                    Position = await context.Positions.SingleAsync(x => x.Name == "Глава отдела"),
                    Department = await context.Departments.SingleAsync(x => x.Name == "Производственный отдел"),
                    Salary = 4000
                }
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
                    Name = "Страховые выплаты",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Коммуникации и связь",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия"),
                        await context.Departments.SingleAsync(x => x.Name == "Отдел продаж"),
                        await context.Departments.SingleAsync(x => x.Name == "Отдел логистики")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Обучение персонала",
                    Departments = await context.Departments.ToListAsync(),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Канцелярия",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Выплата заработной платы",
                    Departments = await context.Departments.ToListAsync(),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Закупка сырья и материалов",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Производственный отдел")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Ремонт оборудования",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Производственный отдел")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Продажа старого оборудования",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Производственный отдел")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Доходы"),
                },
                new OperationCategory
                {
                    Name = "Продажа излишек сырья",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Производственный отдел")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Доходы"),
                },
                new OperationCategory
                {
                    Name = "Реклама и маркетинг",
                    Departments = new List<Department>
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Отдел продаж")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Аренда помещения",
                    Departments = await context.Departments.ToListAsync(),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Налоги",
                    Departments = await context.Departments.ToListAsync(),
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Транспорт",
                    Departments = new List<Department>()
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Отдел логистики")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Страхование грузов",
                    Departments = new List<Department>()
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Отдел логистики")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Таможенное оформление",
                    Departments = new List<Department>()
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Отдел логистики")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Расходы"),
                },
                new OperationCategory
                {
                    Name = "Платежи за логистические услуги",
                    Departments = new List<Department>()
                    {
                        await context.Departments.SingleAsync(x => x.Name == "Отдел логистики")
                    },
                    Type = await context.OperationTypes.SingleAsync(x => x.Name == "Доходы"),
                },
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
                    Category = await context.OperationCategories.SingleAsync(x => x.Name == "Страховые выплаты"),
                    Date = DateTime.Today,
                    Sum = 5000,
                    Department = await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия")
                },
                new Operation
                {
                    Category = await context.OperationCategories.SingleAsync(x => x.Name == "Коммуникации и связь"),
                    Date = DateTime.Today,
                    Sum = 1000,
                    Department = await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия")
                },
                new Operation
                {
                    Category = await context.OperationCategories.SingleAsync(x => x.Name == "Аренда помещения"),
                    Comment = "Выплата аренды офисного помещения",
                    Date = DateTime.Today,
                    Sum = 10000,
                    Department = await context.Departments.SingleAsync(x => x.Name == "Бухгалтерия")
                }
            };

            await context.Operations.AddRangeAsync(operations);
            await context.SaveChangesAsync();
        }
    }
}