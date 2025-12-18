using EnterpriseWebApi.Domain.Entities;
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
        return await _repo.GetAllAsync();
    }
}
