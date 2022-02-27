using UnityEngine;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AISystems : Feature
    {
        public AISystems(Contexts contexts)
        {
            Add(new AIInitSystem(contexts));
            Add(new AgentSetupSystem(contexts));
        }
    }
}