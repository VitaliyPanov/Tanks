using General.Services;
using TanksGB.Data;
using TanksGB.GameLogic.Systems.Input;

namespace TanksGB.GameLogic.Systems.Update
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