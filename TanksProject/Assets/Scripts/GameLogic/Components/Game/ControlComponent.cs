using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace TanksGB.GameLogic.Components.Game
{
    [Game, FlagPrefix("try")]
    public sealed class ControlComponent : IComponent {}
}