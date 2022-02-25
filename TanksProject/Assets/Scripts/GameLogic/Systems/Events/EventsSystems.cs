namespace TanksGB.GameLogic.Systems.Events
{
    public sealed class EventsSystems : Feature
    {
        public EventsSystems(Contexts contexts)
        {
            Add(new TriggeredShellExplosionSystem(contexts));
        }
    }
}