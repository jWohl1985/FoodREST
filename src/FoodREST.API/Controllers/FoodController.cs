using Ardalis.Result;
using FoodREST.API.Mapping;
using FoodREST.Application.Commands;
using FoodREST.Application.Queries;
using FoodREST.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace FoodREST.API.Controllers;

[ApiController]
public class FoodController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOutputCacheStore _outputCacheStore;

    public FoodController(IMediator mediator, IOutputCacheStore outputCacheStore)
    {
        _mediator = mediator;
        _outputCacheStore = outputCacheStore;
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

        await _outputCacheStore.EvictByTagAsync(OutputCacheTags.FoodTag, token);

        return result.IsError()
            ? Problem()
            : Ok(result.Value.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Foods.Get)]
    [OutputCache(PolicyName = OutputCachePolicies.FoodCachePolicy)]
    public async Task<IActionResult> Get([FromRoute]Guid id, CancellationToken token)
    {
        var query = new GetFoodQuery() { Id = id };

        var result = await _mediator.Send(query, token);

        return result.IsNotFound()
            ? NotFound()
            : Ok(result.Value.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Foods.GetAll)]
    [OutputCache(PolicyName = OutputCachePolicies.FoodCachePolicy)]
    public async Task<IActionResult> GetAll([FromQuery]GetAllFoodsRequest request, CancellationToken token)
    {
        var query = new GetAllFoodsQuery() { Options = request.MapToOptions() };

        var result = await _mediator.Send(query, token);

        var foodsResponse = result.Foods.MapToResponse(request.Page, request.PageSize, result.Count);

        return Ok(foodsResponse);
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

        if (result.IsNotFound())
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(OutputCacheTags.FoodTag, token);
        return Ok(result.Value.MapToResponse());
    }

    [HttpDelete(ApiEndpoints.Foods.Delete)]
    public async Task<IActionResult> Delete([FromRoute]Guid id, CancellationToken token)
    {
        var command = new DeleteFoodCommand() { Id = id };

        var result = await _mediator.Send(command, token);

        if (result.IsNotFound())
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(OutputCacheTags.FoodTag, token);
        return Ok();
    }
}
