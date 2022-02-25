using Entitas;
using TanksGB.Data;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class TeamComponent : IComponent
    {
        public TeamType Type;
    }
}