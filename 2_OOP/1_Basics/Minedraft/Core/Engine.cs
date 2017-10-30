using System;
using System.Linq;

public class Engine
{
    private DraftManager drafManager;
    private string returnResult;
    private bool isRunning;

    public Engine()
    {
        this.drafManager = new DraftManager();
        this.isRunning = true;
    }

    public void Run()
    {
        while (this.isRunning)
        {
            string[] cmdArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ExecuteCommand(cmdArgs);
        }
    }

    private void ExecuteCommand(string[] cmdArgs)
    {
        string command = cmdArgs[0];

        switch (command)
        {
            case "RegisterHarvester":
                this.returnResult = drafManager.RegisterHarvester(cmdArgs.Skip(1).ToList());
                break;
            case "RegisterProvider":
                this.returnResult = drafManager.RegisterProvider(cmdArgs.Skip(1).ToList());
                break;
            case "Day":
                this.returnResult = drafManager.Day();
                break;
            case "Mode":
                this.returnResult = drafManager.Mode(cmdArgs.Skip(1).ToList());

                this.returnResult = Operations.SuccessfullyChangedModeMsg(this.returnResult);
                break;
            case "Check":
                this.returnResult = drafManager.Check(cmdArgs.Skip(1).ToList());
                break;
            case "Shutdown":
                this.returnResult = drafManager.ShutDown();
                this.isRunning = false;
                break;
        }

        Console.WriteLine(this.returnResult);
    }
}
