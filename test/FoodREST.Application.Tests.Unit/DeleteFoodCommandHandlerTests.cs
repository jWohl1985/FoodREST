using Ardalis.Result;
using FluentAssertions;
using FoodREST.Application.Commands;
using FoodREST.Application.Interfaces;
using NSubstitute;

namespace FoodREST.Application.Tests.Unit;

public class DeleteFoodCommandHandlerTests
{
    private readonly IFoodRepository _foodRepository = Substitute.For<IFoodRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private DeleteFoodCommandHandler _sut;

    public DeleteFoodCommandHandlerTests()
    {
        _sut = new DeleteFoodCommandHandler(_foodRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenRequestIsValid()
    {
        // Arrange
        var command = new DeleteFoodCommand() { Id = Guid.NewGuid() };
        _foodRepository.DeleteFoodAsync(command.Id).Returns(true);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        await _foodRepository.Received(1).DeleteFoodAsync(command.Id);
        await _unitOfWork.Received(1).SaveChangesAsync();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenRequestFails()
    {
        // Arrange
        var command = new DeleteFoodCommand() { Id = Guid.NewGuid() };
        _foodRepository.DeleteFoodAsync(command.Id).Returns(false);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        await _foodRepository.Received(1).DeleteFoodAsync(command.Id);
        await _unitOfWork.DidNotReceive().SaveChangesAsync();
        result.IsNotFound().Should().BeTrue();
    }
}
