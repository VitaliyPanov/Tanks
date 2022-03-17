using Tanks.GameLogic.Systems.Input;
using Tanks.General.Services;
using Tanks.General.Services.Input;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class InputSystems : Feature
    {
        public InputSystems(GameContext gameContext, InputContext inputContext, IInputService inputService)
        {
            Add(new InputSystem(inputContext, inputService));
            Add(new InputShootSystem(gameContext, inputContext));
            Add(new InputChangeControllableSystem(gameContext, inputContext));
        }
    }
}