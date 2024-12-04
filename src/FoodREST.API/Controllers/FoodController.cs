using Ardalis.Result;
using FoodREST.API.Mapping;
using FoodREST.Application.Commands;
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
    public async Task<IActionResult> Create([FromBody]CreateFoodRequest request)
    {
        var command = new CreateFoodCommand(
            name: request.Name,
            calories: request.Calories,
            proteinGrams: request.ProteinGrams,
            carbohydrateGrams: request.CarbohydrateGrams,
            fatGrams: request.FatGrams);

        var result = await _mediator.Send(command);

        return result.IsError()
            ? BadRequest()
            : Ok(result.Value.MapToResponse());
    }
}
