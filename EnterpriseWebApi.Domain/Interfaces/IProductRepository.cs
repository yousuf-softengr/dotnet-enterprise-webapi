using EnterpriseWebApi.Domain.Entities;

namespace EnterpriseWebApi.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
}
