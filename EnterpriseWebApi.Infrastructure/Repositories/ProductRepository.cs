using EnterpriseWebApi.Domain.Entities;
using EnterpriseWebApi.Domain.Interfaces;
using EnterpriseWebApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseWebApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
}
