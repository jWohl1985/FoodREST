using Ardalis.Result;
using FoodREST.API.Mapping;
using FoodREST.Application.Commands;
using FoodREST.Application.Queries;
using FoodREST.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodREST.API.Controllers;

[ApiController]
public class FoodController : ControllerBase
{
    private readonly IMediator _mediator;

    public FoodController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(ApiEndpoints.Foods.Create)]
    public async Task<IActionResult> Create([FromBody]CreateFoodRequest request, CancellationToken token)
    {
        var command = new CreateFoodCommand(
            name: request.Name,
            calories: request.Calories,
            proteinGrams: request.ProteinGrams,
            carbohydrateGrams: request.CarbohydrateGrams,
            fatGrams: request.FatGrams);

        var result = await _mediator.Send(command, token);

        return result.IsError()
            ? BadRequest()
            : Ok(result.Value.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Foods.Get)]
    public async Task<IActionResult> Get([FromRoute] string id, CancellationToken token)
    {
        if (!Guid.TryParse(id, out Guid guid))
        {
            return BadRequest();
        }

        var query = new GetFoodQuery() { Id = guid };

        var result = await _mediator.Send(query, token);

        return result.IsNotFound()
            ? NotFound()
            : Ok(result.Value.MapToResponse());
    }
}
