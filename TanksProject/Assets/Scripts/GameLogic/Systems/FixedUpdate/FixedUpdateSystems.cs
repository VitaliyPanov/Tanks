using Tanks.Data;
using Tanks.GameLogic.Systems.FixedUpdate.Events;
using Tanks.GameLogic.Systems.Update;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.FixedUpdate
{
    internal sealed class FixedUpdateSystems : Feature
    {
        public FixedUpdateSystems(Contexts contexts, RuntimeData runtimeData,
            IPoolService poolService)
        {
            Add(new TimeTrippingSystem(contexts.game, contexts.input));
            Add(new MovementSystem(contexts.game, contexts.input, runtimeData));
            Add(new EventsSystems(contexts, runtimeData, poolService));
            Add(new ParticlesRemoveSystem(contexts.game, poolService));
        }
    }
}