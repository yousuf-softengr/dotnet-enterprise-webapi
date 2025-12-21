using MediatR;

namespace EnterpriseWebApi.Application.Products.Commands;

public record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<Guid>;
