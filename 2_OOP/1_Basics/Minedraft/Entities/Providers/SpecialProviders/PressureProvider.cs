public class PressureProvider : Provider
{
    public PressureProvider(string id, double energyOutput)
        : base(id, energyOutput)
    {
        this.EnergyOutput = base.EnergyOutput * Constants.P_PROVIDER_INCREASE_ENERGY_MULTYPLIER;
    }
}
