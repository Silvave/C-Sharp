using System.Collections.Generic;
using System.Linq;

public class DragRace : Race
{
    public DragRace(int length, string route, int prizePool)
        : base(length, route, prizePool)
    {
    }

    private int EngineP(Car c) => c.PP = c.Horsepower / c.Acceleration;

    public override void SetWinners(List<Car> cars)
    {
        this.Winners = cars.OrderByDescending(EngineP).Take(3).ToList();
    }
}
