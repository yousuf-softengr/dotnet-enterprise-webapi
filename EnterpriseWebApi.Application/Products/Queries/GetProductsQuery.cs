using EnterpriseWebApi.Domain.Entities;
using MediatR;

namespace EnterpriseWebApi.Application.Products.Queries;

public record GetProductsQuery : IRequest<IEnumerable<Product>>;
