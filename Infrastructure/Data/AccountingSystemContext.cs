using ApplicationCore.Entity;
using Infrastructure.Data.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AccountingSystemContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Operation> Operations { get; set; }

    public DbSet<OperationCategory> OperationCategories { get; set; }

    public DbSet<OperationType> OperationTypes { get; set; }

    public DbSet<Position> Positions { get; set; }

    public DbSet<Department> Departments { get; set; }

    public AccountingSystemContext(DbContextOptions<AccountingSystemContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationCategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PositionEntityTypeConfiguration());
    }
}