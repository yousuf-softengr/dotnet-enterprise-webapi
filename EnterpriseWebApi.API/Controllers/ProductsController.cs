using EnterpriseWebApi.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseWebApi.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediator.Send(new GetProductsQuery()));
    }
}
