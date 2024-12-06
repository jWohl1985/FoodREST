using FoodREST.Domain;

namespace FoodREST.Application.Tests.Unit;

public class FoodFixture
{
    public FoodFixture()
    {
        Banana = new Food(Guid.NewGuid(), "Banana", 110, 2, 27, 1);
        BeefJerky = new Food(Guid.NewGuid(), "Beef Jerky", 90, 15, 10, 2);
    }

    public Food Banana { get; private set; }

    public Food BeefJerky { get; private set; }
}
