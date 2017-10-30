using System.Collections.Generic;
using System.Linq;

public class CarManager
{
    private Dictionary<int, Car> cars;
    private Dictionary<int, Race> races;
    private Garage garage;

    public CarManager()
    {
        this.cars = new Dictionary<int, Car>();
        this.races = new Dictionary<int, Race>();
        this.garage = new Garage();
    }

    public Dictionary<int, Car> Cars => this.cars;
    public Dictionary<int, Race> Races => this.races;
    public Garage Garage => this.garage;


    public void Register(int id, string type, string brand, string model, int yearOfProduction, int horsepower, int acceleration, int suspension, int durability)
    {
        if (!this.Cars.ContainsKey(id))
        {
            Car newCar = CarFactory.CreateCar(type, brand, model, yearOfProduction, horsepower, acceleration, suspension, durability);

            this.Cars.Add(id, newCar);
        }
    }

    public string Check(int id)
    {
        return this.Cars[id].ToString();
    }

    public void Open(int id, string type, int length, string route, int prizePool, int bonus)
    {
        if (!this.Races.ContainsKey(id))
        {
            Race newRace;

            newRace = RaceFactory.CreateRace(type, length, route, prizePool, bonus);

            this.Races.Add(id, newRace);
        }
    }

    public void Participate(int carId, int raceId)
    {
        if (this.Races[raceId].IsRaceFinished || this.cars[carId].IsInGarage)
        {
            return;
        }

        this.cars[carId].IsRacing = true;

        this.Races[raceId].Participants.Add(this.cars[carId]);
    }

    public string Start(int id)
    {
        if (!this.Races[id].Participants.Any())
        {
            return Constants.RACE_HAS_NO_PARTICIPANTS_MSG;
        }


        this.Races[id].SetWinners(this.Races[id].Participants);

        this.Races[id].IsRaceFinished = true;

        if (this.Races[id] is TimeLimitRace)
        {
            TimeLimitRace timeLimitRace = this.Races[id] as TimeLimitRace;

            timeLimitRace.GetTimeCarStats(timeLimitRace);

            return timeLimitRace.ToString();
        }

        foreach (int key in this.Cars.Keys)
        {
            bool isCarStillRacing = this.Races.Values
                                                .Where(r => !r.IsRaceFinished)
                                                .Any(r => r.Participants.Any(x => x.Equals(this.Cars[key])));

            if (this.Cars[key].IsRacing && !isCarStillRacing)
            {
                this.Cars[key].IsRacing = false;
            }
        }

        return this.Races[id].ToString();
    }

    public void Park(int id)
    {
        if (this.Cars[id].IsRacing)
        {
            return;
        }

        this.Garage.ParkedCars.Add(this.Cars[id]);

        this.Cars[id].IsInGarage = true;
    }

    public void Unpark(int id)
    {
        if (this.Cars[id].IsRacing)
        {
            return;
        }

        int index = this.garage.ParkedCars.FindIndex(c => c.Equals(this.Cars[id]));

        this.Garage.ParkedCars.RemoveAt(index);

        this.Cars[id].IsInGarage = false;
    }

    public void Tune(int tuneIndex, string addOn)
    {
        if (!garage.ParkedCars.Any())
        {
            return;
        }
                
        int horsepowerIncrease = tuneIndex;
        int suspensionIncrease = (tuneIndex * 50) / 100;

        for (int i = 0; i < garage.ParkedCars.Count; i++)
        {
            Car parkedCar = garage.ParkedCars[i];

            parkedCar.Horsepower += horsepowerIncrease;
            parkedCar.Suspension += suspensionIncrease;

            if (parkedCar is ShowCar)
            {
                ShowCar showCar = parkedCar as ShowCar;

                showCar.Stars = (showCar.Stars + tuneIndex);
            }
            else
            {
                PerformanceCar performanceCar = parkedCar as PerformanceCar;

                performanceCar.AddOns.Add(addOn);
            }
        }
    }
}
