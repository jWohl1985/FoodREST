using Ardalis.Result;
using MediatR;

namespace FoodREST.Application.Commands;

public class DeleteFoodCommand : IRequest<Result>
{
    public required Guid Id { get; init; }
}
