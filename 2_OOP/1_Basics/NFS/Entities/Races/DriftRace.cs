using System.Collections.Generic;
using System.Linq;

public class DriftRace : Race
{
    public DriftRace(int length, string route, int prizePool)
        : base(length, route, prizePool)
    {
    }

    private int SuspensionP(Car c) => c.PP = c.Suspension + c.Durability;

    public override void SetWinners(List<Car> cars)
    {
        this.Winners = cars.OrderByDescending(SuspensionP).Take(3).ToList();
    }
}
