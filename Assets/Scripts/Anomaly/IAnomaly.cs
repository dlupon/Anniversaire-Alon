public interface IAnomaly
{
    public bool IsActive { get; }

    public string Type { get; }

    public void Trigger();

    public void Fix();
}
