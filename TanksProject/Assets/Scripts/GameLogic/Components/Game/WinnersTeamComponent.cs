using Entitas;
using Entitas.CodeGeneration.Attributes;
using Tanks.Data;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    [Unique]
    public sealed class WinnersTeamComponent : IComponent
    {
        public TeamType Value;
    }
}