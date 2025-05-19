public interface IAnomaly
{
    public bool IsActive { get; }

    public void Trigger();

    public void Fix();
}
