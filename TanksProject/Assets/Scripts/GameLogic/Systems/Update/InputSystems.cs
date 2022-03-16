using Tanks.Data;
using Tanks.GameLogic.Systems.Input;
using Tanks.General.Services;
using Tanks.General.Services.Input;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class InputSystems : Feature
    {
        public InputSystems(Contexts contexts, RuntimeData runtimeData, IInputService inputService)
        {
            Add(new InputSystem(contexts, inputService));
            Add(new InputShootSystem(contexts));
            Add(new InputChangeControllableSystem(contexts));
        }
    }
}