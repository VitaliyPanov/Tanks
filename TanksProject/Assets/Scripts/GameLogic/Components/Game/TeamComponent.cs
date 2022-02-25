using Entitas;
using Tanks.Data;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class TeamComponent : IComponent
    {
        public TeamType Type;
    }
}