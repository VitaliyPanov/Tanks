using Tanks.General.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.GameLogic.Services.Input
{
    public sealed class TankInputService : ITankInputService
    {
        private readonly InputContext _context;

        public TankInputService(InputContext inputContext, IInputService inputService)
        {
            _context = inputContext;
            _context.SetDirection(Vector2.zero);
            inputService.RegisterTankListener(this);
        }

        public void OnMove(InputAction.CallbackContext context) =>
            _context.ReplaceDirection(_context.isPause ? Vector2.zero : context.ReadValue<Vector2>());

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (!_context.isPause)
                _context.isAttack = context.performed;
        }

        public void OnToggleNextTank(InputAction.CallbackContext context)
        {
            if (!_context.isPause)
                _context.isToggleNext = context.performed;
        }

        public void OnTogglePreviousTank(InputAction.CallbackContext context)
        {
            if (!_context.isPause)
                _context.isTogglePrevious = context.performed;
        }
    }
}