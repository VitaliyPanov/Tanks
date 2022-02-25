using TanksGB.Data;

namespace TanksGB.GameLogic.Systems.Init
{
    internal sealed class InitSystems : Feature
    {
        public InitSystems(Contexts contexts, SceneStaticData staticData, RuntimeData runtimeData)
        {
            Add(new TanksInitSystem(contexts, staticData, runtimeData));
            Add(new TeamsInitSystem(contexts));
        }
    }
}