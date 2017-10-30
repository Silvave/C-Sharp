using System;
using System.Text;

public abstract class Harvester : Worker
{
    private double oreOutput;
    private double energyRequirement;

    public Harvester(string id, double oreOutput, double energyRequirement)
        : base(id)
    {
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
    }

    public double OreOutput
    {
        get => this.oreOutput;
        protected set
        {
            if (value < Constants.MIN_ORE)
            {
                throw new ArgumentException(Operations
                    .WorkerNotRegisteredMsg("Harvester", nameof(this.OreOutput)));
            }
            this.oreOutput = value;
        }
    }

    public double EnergyRequirement
    {
        get => this.energyRequirement;
        protected set
        {
            if (value < Constants.MIN_ENERGY || value > Constants.MAX_ENERGY_REQUIREMENT)
            {
                throw new ArgumentException(Operations
                    .WorkerNotRegisteredMsg("Harvester", nameof(this.EnergyRequirement)));
            }
            this.energyRequirement = value;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        string type;
        if (this is HammerHarvester)
        {
            type = "Hammer";
        }
        else
        {
            type = "Sonic";
        }

        sb.AppendLine($"{type} Harvester - {this.Id}")
          .AppendLine($"Ore Output: {this.OreOutput}")
          .Append($"Energy Requirement: {this.EnergyRequirement}");

        return sb.ToString();
    }
}
