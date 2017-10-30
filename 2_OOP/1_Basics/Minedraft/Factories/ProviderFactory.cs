public class ProviderFactory 
{
    public static Provider CreateProvider(string type, string id, double energyOutput)
    {
        Provider provider;

        switch (type)
        {
            case "Solar":
                provider = new SolarProvider(id, energyOutput);
                break;
            case "Pressure":
                provider = new PressureProvider(id, energyOutput);
                break;
            default:
                return null;
        }

        return provider;
    }
}
