using EnterpriseWebApi.Application.Products.Commands;
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }
}
