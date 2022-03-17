using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AISystems : Feature
    {
        public AISystems(Contexts contexts, IDataService dataService)
        {
            Add(new AgentSetupSystem(contexts.aI, dataService));
            Add(new AgentControlSystem(contexts.aI));
            Add(new AgentToggleActiveSystem(contexts.aI));
            Add(new AgentDeactivateSystem(contexts.aI));
            Add(new AgentActivateSystem(contexts.aI, contexts.game));
            Add(new AgentShootSystem(contexts.aI, contexts.input, dataService));
        }
    }
}