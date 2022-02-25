using TanksGB.Data;
using TanksGB.GameLogic.Systems.Events;
using TanksGB.GameLogic.Systems.Update;

namespace TanksGB.GameLogic.Systems.FixedUpdate
{
    internal sealed class FixedUpdateSystems : Feature
    {
        public FixedUpdateSystems(Contexts contexts, RuntimeData runtimeData)
        {
            Add(new TimeTrippingSystem(contexts));
            Add(new MovementSystem(contexts, runtimeData));
            Add(new EventsSystems(contexts));
        }
    }
}