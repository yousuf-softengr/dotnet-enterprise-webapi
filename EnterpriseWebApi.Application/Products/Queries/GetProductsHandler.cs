using EnterpriseWebApi.Domain.Entities;
using EnterpriseWebApi.Domain.Exceptions;
using EnterpriseWebApi.Domain.Interfaces;
using MediatR;

namespace EnterpriseWebApi.Application.Products.Queries;

public class GetProductsHandler
    : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repo;

    public GetProductsHandler(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Product>> Handle(
    GetProductsQuery request,
    CancellationToken cancellationToken)
    {
        var products = await _repo.GetAllAsync();

        if (!products.Any())
            throw new NotFoundException("No products found");

        return products;
    }

}
