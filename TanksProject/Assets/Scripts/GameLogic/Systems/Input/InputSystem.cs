using Entitas;
using Tanks.General.Services;
using Tanks.General.Services.Input;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Input
{
    internal sealed class InputSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<GameEntity> _entities;
        private readonly InputContext _context;

        public InputSystem(InputContext inputContext, IInputService inputService)
        {
            _context = inputContext;
            _inputService = inputService;

            _context.SetDirection(Vector2.zero);
            _inputService.OnAttackButtonEvent += ActivateFire;
            _inputService.OnToggleNextEvent += ToggleNextMovable;
            _inputService.OnTogglePreviousEvent += TogglePreviousMovable;

            //TODO: Realise dispose
        }

        public void Execute() => _context.ReplaceDirection(_context.isPause ? Vector2.zero : _inputService.Axis);

        private void ToggleNextMovable(bool performed)
        {
            if (!_context.isPause)
                _context.isToggleNext = performed;
        }

        private void TogglePreviousMovable(bool performed)
        {
            if (!_context.isPause)
                _context.isTogglePrevious = performed;
        }

        private void ActivateFire(bool performed)
        {
            if (!_context.isPause)
                _context.isAttack = performed;
        }
    }
}