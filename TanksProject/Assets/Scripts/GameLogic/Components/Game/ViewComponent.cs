using Entitas;
using Tanks.GameLogic.Views;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class ViewComponent : IComponent
    {
        public IView Value;
    }
}