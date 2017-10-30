using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DraftManager
{
    private string resultMsg;
    private string systemMode;
    private double totalMinedOre;
    private double totalStoredEnergy;
    private HashSet<Harvester> harvesters;
    private HashSet<Provider> providers;

    public DraftManager()
    {
        this.systemMode = Constants.SYSTEM_FULL_MODE;
        this.harvesters = new HashSet<Harvester>();
        this.providers = new HashSet<Provider>();
    }

    public double TotalMinedOre
    {
        get => this.totalMinedOre;
        protected set
        {
            this.totalMinedOre = value;
        }
    }

    public double TotalStoredEnergy
    {
        get => this.totalStoredEnergy;
        protected set
        {
            this.totalStoredEnergy = value;
        }
    }

    public string SystemMode
    {
        get => this.systemMode;
        protected set
        {
            this.systemMode = value;
        }
    }

    public string RegisterHarvester(List<string> arguments)
    {
        string type = arguments[0];
        string id = arguments[1];
        double oreOutput = double.Parse(arguments[2]);
        double energyRequirement = double.Parse(arguments[3]);
        int sonicFactor = 0;

        if (type == "Sonic")
        {
            sonicFactor = int.Parse(arguments[4]);
        }

        Harvester createdHarvested;
        try
        {
            if (type == "Sonic")
            {
                createdHarvested = HarvesterFactory
                    .CreateHarvester(id, oreOutput, energyRequirement, sonicFactor);
            }
            else
            {
                createdHarvested = HarvesterFactory
                    .CreateHarvester(id, oreOutput, energyRequirement);
            }
        }   
        catch (ArgumentException ex)
        {
            this.resultMsg = ex.Message;

            return this.resultMsg;
        }

        bool isWorkerIdUnique = !this.harvesters.Any(h => h.Id == createdHarvested.Id) &&
                                    !this.providers.Any(p => p.Id == createdHarvested.Id);

        if (!isWorkerIdUnique)
        {
            this.resultMsg = Operations.WorkerNotRegisteredMsg("Harvester", "Id");
        }
        else
        {
            // Registering harvester
            this.harvesters.Add(createdHarvested);

            this.resultMsg = Operations.WorkerRegisteredMsg(type, "Harvester", createdHarvested.Id);
        }

        return this.resultMsg;
    }

    public string RegisterProvider(List<string> arguments)
    {
        string type = arguments[0];
        string id = arguments[1];
        double energyOutput = double.Parse(arguments[2]);

        try
        {
            Provider createdProvider = ProviderFactory.CreateProvider(type, id, energyOutput);

            bool isWorkerIdUnique = !this.harvesters.Any(h => h.Id == createdProvider.Id) &&
                                    !this.providers.Any(p => p.Id == createdProvider.Id);

            if (!isWorkerIdUnique)
            {
                return Operations.WorkerNotRegisteredMsg("Provider", "Id");
            }

            // Registering provider
            this.providers.Add(createdProvider);

            this.resultMsg = Operations.WorkerRegisteredMsg(type, "Provider", createdProvider.Id);
        }
        catch (ArgumentException ex)
        {
            this.resultMsg = ex.Message;
        }

        return this.resultMsg;
    }

    public string Day()
    {
        UpdateWorkDayStats(out double minedOreForDay, out double storedEnergyForDay);

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("A day has passed.")
          .AppendLine($"Energy Provided: {(int)storedEnergyForDay}")
          .Append($"Plumbus Ore Mined: {(int)minedOreForDay}");

        this.resultMsg = sb.ToString();

        return this.resultMsg;
    }

    private void UpdateWorkDayStats(out double minedOreDay, out double storedEnergyDay)
    {
        minedOreDay = 0;
        storedEnergyDay = this.providers.Sum(p => p.EnergyOutput);
        double storedEnergy = this.TotalStoredEnergy + storedEnergyDay;

        double spendedEnergyDay = 0;
        double neededEnergyDay = 0;
        if (this.SystemMode == Constants.SYSTEM_FULL_MODE)
        {
            neededEnergyDay = this.harvesters.Sum(h => h.EnergyRequirement);

            if (neededEnergyDay <= storedEnergy)
            {
                spendedEnergyDay = neededEnergyDay;
                minedOreDay = this.harvesters.Sum(h => h.OreOutput);
            }
        }
        else if (this.SystemMode == Constants.SYSTEM_HALF_MODE)
        {
            neededEnergyDay = this.harvesters.Sum(h => h.EnergyRequirement * 0.60);

            if (neededEnergyDay <= storedEnergy)
            {
                spendedEnergyDay = neededEnergyDay;
                minedOreDay = this.harvesters.Sum(h => h.OreOutput * 0.50);
            }
        }

        this.TotalMinedOre = this.TotalMinedOre + minedOreDay;
        this.TotalStoredEnergy = storedEnergy - spendedEnergyDay;
    }

    public string Mode(List<string> arguments)
    {
        string mode = arguments[0];

        this.SystemMode = mode;

        return this.SystemMode;
    }

    public string Check(List<string> arguments)
    {
        string id = arguments[0];

        Harvester harvester = this.harvesters.FirstOrDefault(h => h.Id == id);
        Provider provider = this.providers.FirstOrDefault(p => p.Id == id);

        if (harvester != null)
        {
            this.resultMsg = harvester.ToString();
        }
        else if (provider != null)
        {
            this.resultMsg = provider.ToString();
        }
        else
        {
            this.resultMsg = Operations.NoWorkerFoundMsg(id);
        }

        return this.resultMsg;
    }

    public string ShutDown()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("System Shutdown")
          .AppendLine($"Total Energy Stored: {this.TotalStoredEnergy}")
          .Append($"Total Mined Plumbus Ore: {this.TotalMinedOre}");

        this.resultMsg = sb.ToString();

        return this.resultMsg;
    }
}
