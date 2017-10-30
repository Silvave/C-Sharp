using System;

public class SonicHarvester : Harvester
{
    private int sonicFactor;

    public SonicHarvester(string id, double oreOutput, double energyRequirement, int sonicFactor)
        : base(id, oreOutput, energyRequirement)
    {
        this.SonicFactor = sonicFactor;
        this.EnergyRequirement = base.EnergyRequirement / this.SonicFactor;
    }

    public int SonicFactor
    {
        get => this.sonicFactor;
        protected set
        {
            if (value < Constants.MIN_SONIC_FACTOR || value > Constants.MAX_SONIC_FACTOR)
            {
                throw new ArgumentException(Operations
                    .WorkerNotRegisteredMsg("Harvester", nameof(this.SonicFactor)));
            }
            this.sonicFactor = value;
        }
    }
}
