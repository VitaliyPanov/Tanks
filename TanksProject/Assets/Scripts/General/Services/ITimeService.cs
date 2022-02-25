namespace General.Services
{
    public interface ITimeService
    {
        float DeltaTime();
        float FixedDeltaTime();
        float RealtimeSinceStartup();
    }
}