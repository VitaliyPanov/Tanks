using Entitas;
using TanksGB.GameLogic.Views;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class ViewComponent : IComponent
    {
        public IView Value;
    }
}