using EnterpriseWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWebApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
