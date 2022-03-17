using Tanks.Data;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.Events
{
    public sealed class EventsSystems : Feature
    {
        public EventsSystems(Contexts contexts, RuntimeData runtimeData, IPoolService poolService)
        {
            Add(new TriggeredShellExplosionSystem(contexts.game, runtimeData, poolService));
            Add(new GameEventSystems(contexts));
        }
    }
}