using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PerformanceCar : Car
{
    private List<string> addOns;

    public PerformanceCar(string brand, string model, int yearOfProduction,
        int horsepower, int acceleration, int suspension, int durability)
        : base(brand, model, yearOfProduction, horsepower, acceleration, suspension, durability)
    {
        this.Horsepower = base.Horsepower + (base.Horsepower * 50) / 100;
        this.Suspension = base.Suspension - (base.Suspension * 25) / 100;
        this.addOns = new List<string>();
    }

    public List<string> AddOns => this.addOns;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder(base.ToString());

        if (addOns.Any())
        {
            sb.Append($"Add-ons: {string.Join(", ", this.AddOns)}");
        }
        else
        {
            sb.Append($"Add-ons: None");
        }

        return sb.ToString();
    }
}
