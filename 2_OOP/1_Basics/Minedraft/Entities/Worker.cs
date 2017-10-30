public abstract class Worker
{
    private string id;

    protected Worker(string id)
    {
        this.Id = id;
    }

    public string Id
    {
        get => this.id;
        protected set => this.id = value;
    }
}
