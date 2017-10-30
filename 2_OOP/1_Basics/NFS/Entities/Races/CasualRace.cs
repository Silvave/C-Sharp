using System.Collections.Generic;
using System.Linq;

public class CasualRace : Race
{
    public CasualRace(int length, string route, int prizePool)
        : base(length, route, prizePool)
    {
    }

    private int OverallP(Car c) => c.PP = (c.Horsepower / c.Acceleration) + (c.Suspension + c.Durability);

    public override void SetWinners(List<Car> cars)
    {
        this.Winners = cars.OrderByDescending(OverallP).Take(3).ToList();
    }
}
