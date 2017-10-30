using System;
using System.Linq;

public class Engine
{
    public void Run()
    {
        CarManager cm = new CarManager();

        string command;
        while ((command = Console.ReadLine()) != Constants.END_OF_PROGRAM)
        {
            var cmdArgs = command.Split();

            int id;
            string type;
            string cmdType = cmdArgs[0];
            switch (cmdType)
            {
                case "register":
                    id = int.Parse(cmdArgs[1]);
                    type = cmdArgs[2];
                    string brand = cmdArgs[3];
                    string model = cmdArgs[4];
                    int year = int.Parse(cmdArgs[5]);
                    int horsepower = int.Parse(cmdArgs[6]);
                    int acceleration = int.Parse(cmdArgs[7]);
                    int suspension = int.Parse(cmdArgs[8]);
                    int durability = int.Parse(cmdArgs[9]);

                    cm.Register(id, type, brand, model, year, horsepower, acceleration, suspension, durability);
                    break;
                case "check":
                    id = int.Parse(cmdArgs[1]);

                    Console.WriteLine(cm.Check(id));
                    break;
                case "open":
                    id = int.Parse(cmdArgs[1]);
                    type = cmdArgs[2];
                    int length = int.Parse(cmdArgs[3]);
                    string route = cmdArgs[4];
                    int prizePool = int.Parse(cmdArgs[5]);
                    int bonus = 0;

                    if (type == "TimeLimit" || type == "Circuit")
                    {
                        bonus = int.Parse(cmdArgs[6]);
                    }

                    cm.Open(id, type, length, route, prizePool, bonus);
                    break;
                case "participate":
                    int carId = int.Parse(cmdArgs[1]);
                    int raceId = int.Parse(cmdArgs[2]);

                    cm.Participate(carId, raceId);
                    break;
                case "start":
                    id = int.Parse(cmdArgs[1]);

                    if (!cm.Races[id].IsRaceFinished)
                    {
                        string raceResult = cm.Start(id);
                        Console.WriteLine(raceResult);
                    }
                    break;
                case "park":
                    id = int.Parse(cmdArgs[1]);

                    cm.Park(id);
                    break;
                case "unpark":
                    id = int.Parse(cmdArgs[1]);

                    cm.Unpark(id);
                    break;
                case "tune":
                    int tuneIndex = int.Parse(cmdArgs[1]);
                    string addOn = cmdArgs[2];

                    cm.Tune(tuneIndex, addOn);
                    break;
                default:
                    break;
            }
        }
    }
}
