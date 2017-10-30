using System.Text;

public abstract class Car
{
    private string brand;
    private string model;
    private int yearOfProduction;
    private int horsepower;
    private int acceleration;
    private int suspension;
    private int durability;
    private bool isRacing;
    private bool isInGarage;
    private int pp;

    public Car() { }

    public Car(string brand, string model, int yearOfProduction, int horsepower, int acceleration, int suspension, int durability)
    {
        this.Brand = brand;
        this.Model = model;
        this.YearOfProduction = yearOfProduction;
        this.Horsepower = horsepower;
        this.Acceleration = acceleration;
        this.Suspension = suspension;
        this.Durability = durability;
        this.isRacing = false;
        this.isInGarage = false;
    }

    public string Brand
    {
        get => this.brand;
        set => this.brand = value;
    }
    public string Model
    {
        get => this.model;
        set => this.model = value;
    }
    public int YearOfProduction
    {
        get => this.yearOfProduction;
        set => this.yearOfProduction = value;
    }
    public int Horsepower
    {
        get => this.horsepower;
        set => this.horsepower = value;
    }
    public int Acceleration
    {
        get => this.acceleration;
        set => this.acceleration = value;
    }
    public int Suspension
    {
        get => this.suspension;
        set => this.suspension = value;
    }
    public int Durability
    {
        get => this.durability;
        set => this.durability = value;
    }
    public bool IsRacing
    {
        get => this.isRacing;
        set => this.isRacing = value;
    }
    public bool IsInGarage
    {
        get => this.isInGarage;
        set => this.isInGarage = value;
    }
    public int PP
    {
        get => this.pp;
        set => this.pp = value;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"{this.Brand} {this.Model} {this.YearOfProduction}")
          .AppendLine($"{this.Horsepower} HP, 100 m/h in {this.Acceleration} s")
          .AppendLine($"{this.Suspension} Suspension force, {this.Durability} Durability");

        return sb.ToString();
    }
}
