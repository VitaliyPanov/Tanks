using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AISystems : Feature
    {
        public AISystems(Contexts contexts, IDataService dataService)
        {
            Add(new AgentSetupSystem(contexts, dataService));
            Add(new AgentControlSystem(contexts));
            Add(new AgentToggleActiveSystem(contexts));
            Add(new AgentDeactivateSystem(contexts));
            Add(new AgentActivateSystem(contexts));
            Add(new AgentShootSystem(contexts, dataService));
        }
    }
}