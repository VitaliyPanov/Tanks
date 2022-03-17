using Tanks.Data;

namespace Tanks.GameLogic.Systems.Init
{
    public sealed class InitSystems : Feature
    {
        public InitSystems(Contexts contexts, SceneStaticData staticData, RuntimeData runtimeData)
        {
            Add(new TanksInitSystem(contexts.game, contexts.aI, staticData, runtimeData));
            Add(new TeamsInitSystem(contexts.game));
        }
    }
}