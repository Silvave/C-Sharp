using System;
using System.Text;

public abstract class Provider : Worker
{
    private double energyOutput;

    public Provider(string id, double energyOutput)
        : base(id)
    {
        this.EnergyOutput = energyOutput;
    }

    public double EnergyOutput
    {
        get => this.energyOutput;
        protected set
        {
            if (value <= Constants.MIN_ENERGY || value >= Constants.MAX_PROVIDER_ENERGY_REQUIREMENT)
            {
                throw new ArgumentException(Operations
                    .WorkerNotRegisteredMsg("Provider", nameof(this.EnergyOutput)));
            }
            this.energyOutput = value;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        string type;
        if (this is SolarProvider)
        {
            type = "Solar";
        }
        else
        {
            type = "Pressure";
        }

        sb.AppendLine($"{type} Provider - {this.Id}")
          .Append($"Energy Output: {this.EnergyOutput}");

        return sb.ToString();
    }
}
