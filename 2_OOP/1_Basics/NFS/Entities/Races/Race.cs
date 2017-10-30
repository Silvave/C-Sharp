using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Race
{
    private int length;
    private string route;
    private int prizePool;
    private List<Car> participants;
    private bool isRaceFinished;
    private List<Car> winners;

    public Race(int length, string route, int prizePool)
    {
        this.length = length;
        this.route = route;
        this.prizePool = prizePool;
        this.participants = new List<Car>();
        this.winners = new List<Car>();
        this.isRaceFinished = false;
    }

    public bool IsRaceFinished
    {
        get => this.isRaceFinished;
        set => this.isRaceFinished = value;
    }

    public List<Car> Participants => this.participants;
    public List<Car> Winners
    {
        get => this.winners;
        set => this.winners = value;
    }

    public int PrizePool => this.prizePool;

    public int Length => this.length;
    public string Route => this.route;

    public abstract void SetWinners(List<Car> cars);

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{this.route} - {this.length}");

        for (int i = 0; i < this.Winners.Count; i++)
        {
            Car winnerCar = this.Winners[i];

            int money;
            if (i == 0)
            {
                money = (this.PrizePool * 50) / 100;
                sb.AppendLine($"1. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
            else if (i == 1)
            {
                money = (this.PrizePool * 30) / 100;
                sb.AppendLine($"2. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
            else
            {
                money = (this.PrizePool * 20) / 100;
                sb.AppendLine($"3. {winnerCar.Brand} {winnerCar.Model} {winnerCar.PP}PP - ${money}");
            }
        }

        return sb.ToString().Trim();
    }
}
