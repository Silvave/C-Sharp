using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TimeLimitRace : Race
{
    private int goldTime;
    private string medal;
    private int prizeMoney;

    public TimeLimitRace(int length, string route, int prizePool, int goldTime)
        : base(length, route, prizePool)
    {
        this.goldTime = goldTime;
    }

    public void GetTimeCarStats(Race r)
    {
        Car c = r.Participants.First();
        c.PP = r.Length * ((c.Horsepower / 100) * c.Acceleration);

        if (c.PP <= this.goldTime)
        {
            this.medal = "Gold";
            this.prizeMoney = r.PrizePool;
        }
        else if (c.PP <= this.goldTime + 15)
        {
            this.medal = "Silver";
            prizeMoney = (r.PrizePool * 50) / 100;
        }
        else
        {
            this.medal = "Bronze";
            prizeMoney = (r.PrizePool * 30) / 100;
        }

        this.Winners = new List<Car> { c };
    }

    public override void SetWinners(List<Car> cars) { }

    public override string ToString()
    {
        Car finishedCar = this.Winners[0];

        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{this.Route} - {this.Length}")
          .AppendLine($"{finishedCar.Brand} {finishedCar.Model} - {finishedCar.PP} s.")
          .AppendLine($"{this.medal} Time, ${this.prizeMoney}.");

        return sb.ToString();
    }
}
