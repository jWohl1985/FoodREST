using Ardalis.Result;
using FoodREST.Domain;
using MediatR;

namespace FoodREST.Application.Queries;

public sealed class GetFoodQuery : IRequest<Result<Food>>
{
    public Guid Id { get; init; }
}
