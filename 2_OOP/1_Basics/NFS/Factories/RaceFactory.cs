public static class RaceFactory
{
    public static Race CreateRace(string type, int length, string route, int prizePool, int bonus)
    {
        Race race;

        switch (type)
        {
            case "Casual":
                race = new CasualRace(length, route, prizePool);
                break;
            case "Drag":
                race = new DragRace(length, route, prizePool);
                break;
            case "Drift":
                race = new DriftRace(length, route, prizePool);
                break;
            case "TimeLimit":
                race = new TimeLimitRace(length, route, prizePool, bonus);
                break;
            case "Circuit":
                race = new CircuitRace(length, route, prizePool, bonus);
                break;
            default:
                return null;
        }

        return race;
    }
}
