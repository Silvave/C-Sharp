public class Operations
{
    public static string SuccessfullyChangedModeMsg(string mode)
        => $"Successfully changed working mode to {mode} Mode";

    public static string WorkerRegisteredMsg(string workerType, string worker, string id)
        => $"Successfully registered {workerType} {worker} - {id}";

    public static string WorkerNotRegisteredMsg(string workerType, string propType)
        => $"{workerType} is not registered, because of it's {propType}";

    public static string NoWorkerFoundMsg(string id)
        => $"No element found with id - {id}";
}
