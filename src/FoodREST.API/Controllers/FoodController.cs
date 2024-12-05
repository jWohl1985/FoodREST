using Ardalis.Result;
using FoodREST.API.Mapping;
using FoodREST.Application.Commands;
using FoodREST.Application.Queries;
using FoodREST.Contracts.Requests;
using FoodREST.Contracts.Responses;
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

        if (result.IsInvalid())
        {
            ValidationFailureResponse response = result.ValidationErrors.MapToResponse();
            return BadRequest(response);
        }

        return result.IsError()
            ? BadRequest()
            : Ok(result.Value.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Foods.Get)]
    public async Task<IActionResult> Get([FromRoute]Guid id, CancellationToken token)
    {
        var query = new GetFoodQuery() { Id = id };

        var result = await _mediator.Send(query, token);

        return result.IsNotFound()
            ? NotFound()
            : Ok(result.Value.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Foods.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var query = new GetAllFoodsQuery();

        var result = await _mediator.Send(query, token);

        return Ok(result.MapToResponse());
    }

    [HttpPut(ApiEndpoints.Foods.Update)]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateFoodRequest request, CancellationToken token)
    {
        var command = new UpdateFoodCommand(id, 
            request.Name, 
            request.Calories, 
            request.ProteinGrams, 
            request.CarbohydrateGrams, 
            request.FatGrams);

        var result = await _mediator.Send(command, token);

        if (result.IsInvalid())
        {
            ValidationFailureResponse response = result.ValidationErrors.MapToResponse();
            return BadRequest(response);
        }

        return result.IsNotFound()
            ? NotFound()
            : Ok(result.Value.MapToResponse());
    }

    [HttpDelete(ApiEndpoints.Foods.Delete)]
    public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken token)
    {
        var command = new DeleteFoodCommand() { Id = id };

        var result = await _mediator.Send(command, token);

        return result.IsNotFound()
            ? NotFound()
            : Ok();
    }
}
