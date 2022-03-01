using Tanks.Data;

namespace Tanks.GameLogic.Systems.Events
{
    public sealed class EventsSystems : Feature
    {
        public EventsSystems(Contexts contexts, SceneStaticData staticData)
        {
            Add(new TriggeredShellExplosionSystem(contexts, staticData));
            Add(new GameEventSystems(contexts));
        }
    }
}