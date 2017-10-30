using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CircuitRace : Race
{
    private int laps;

    public CircuitRace(int length, string route, int prizePool, int laps)
        : base(length, route, prizePool)
    {
        this.laps = laps;
    }

    private int OverallP(Car c)
    {
        c.Durability -= this.laps * (this.Length * this.Length);
        c.PP = (c.Horsepower / c.Acceleration) + (c.Suspension + c.Durability);

        return c.PP;
    }

    public override void SetWinners(List<Car> cars)
    {
        this.Winners = cars
                        .OrderByDescending(OverallP)
                        .Take(4)
                        .ToList();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{this.Route} - {this.Length * this.laps}");

        for (int i = 0; i < this.Winners.Count; i++)
        {
            Car winnerCar = this.Winners[i];

            int money;
            if (i == 0)
            {
                money = (this.PrizePool * 40) / 100;
                sb.AppendLine($"1. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
            else if (i == 1)
            {
                money = (this.PrizePool * 30) / 100;
                sb.AppendLine($"2. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
            else if (i == 2)
            {
                money = (this.PrizePool * 20) / 100;
                sb.AppendLine($"3. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
            else
            {
                money = (this.PrizePool * 10) / 100;
                sb.AppendLine($"4. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
        }

        return sb.ToString().Trim();
    }
}
