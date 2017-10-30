public class HammerHarvester : Harvester
{
    public HammerHarvester(string id, double oreOutput, double energyRequirement)
        : base(id, oreOutput, energyRequirement)
    {
        this.OreOutput = base.OreOutput * Constants.H_HARVESTER_INCREASE_ORE_MULTYPLIER;
        this.EnergyRequirement = base.EnergyRequirement * Constants.H_HARVESTER_INCREASE_ENERGY_MULTYPLIER;
    }
}
