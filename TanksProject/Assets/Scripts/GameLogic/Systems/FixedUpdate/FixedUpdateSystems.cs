using Tanks.Data;
using Tanks.GameLogic.Systems.Events;
using Tanks.GameLogic.Systems.Update;

namespace Tanks.GameLogic.Systems.FixedUpdate
{
    internal sealed class FixedUpdateSystems : Feature
    {
        public FixedUpdateSystems(Contexts contexts, SceneStaticData staticData, RuntimeData runtimeData)
        {
            Add(new TimeTrippingSystem(contexts));
            Add(new MovementSystem(contexts, runtimeData));
            Add(new EventsSystems(contexts, staticData));
        }
    }
}