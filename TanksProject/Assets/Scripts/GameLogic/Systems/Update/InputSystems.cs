using Tanks.GameLogic.Systems.Input;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class InputSystems : Feature
    {
        public InputSystems(GameContext gameContext, InputContext inputContext)
        {
            Add(new InputShootSystem(gameContext, inputContext));
            Add(new InputChangeControllableSystem(gameContext, inputContext));
        }
    }
}