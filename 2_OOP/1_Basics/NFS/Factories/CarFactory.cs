public static class CarFactory
{
    public static Car CreateCar(string type, string brand, string model, int year, int horsepower, int acceleration, int suspension, int durability)
    {
        Car car;

        switch (type)
        {
            case "Performance":
                car = new PerformanceCar(brand, model, year, horsepower, acceleration, suspension, durability);
                break;
            case "Show":
                car = new ShowCar(brand, model, year, horsepower, acceleration, suspension, durability);
                break;
            default:
                return null;
        }

        return car;
    }
}
