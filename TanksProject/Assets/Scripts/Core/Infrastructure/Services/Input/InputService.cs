using Tanks.General.Services.Input;

namespace Tanks.Core.Infrastructure.Services.Input
{
    internal sealed class InputService : IInputService
    {
        private readonly InputControls _input;

        public InputService() => _input = new InputControls();

        public void RegisterTankListener(InputControls.ITankActions system)
        {
            _input.Tank.SetCallbacks(system);
            if (!_input.Tank.enabled)
                _input.Tank.Enable();
        }

        public void RegisterUIListener(InputControls.IUIActions system)
        {
            _input.UI.SetCallbacks(system);
            if (!_input.UI.enabled)
                _input.UI.Enable();
        }
    }
}