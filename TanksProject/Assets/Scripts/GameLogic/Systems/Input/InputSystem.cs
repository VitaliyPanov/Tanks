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
        private readonly InputContext _inputContext;

        public InputSystem(Contexts contexts, IInputService inputService)
        {
            _inputContext = contexts.input;
            _inputService = inputService;

            _inputContext.SetDirection(Vector2.zero);
            _inputService.OnAttackButtonEvent += ActivateFire;
            _inputService.OnToggleNextEvent += ToggleNextMovable;
            _inputService.OnTogglePreviousEvent += TogglePreviousMovable;

            //TODO: Realise dispose
        }

        public void Execute() => _inputContext.ReplaceDirection(_inputContext.isPause ? Vector2.zero : _inputService.Axis);

        private void ToggleNextMovable(bool performed)
        {
            if (!_inputContext.isPause)
                _inputContext.isToggleNext = performed;
        }

        private void TogglePreviousMovable(bool performed)
        {
            if (!_inputContext.isPause)
                _inputContext.isTogglePrevious = performed;
        }

        private void ActivateFire(bool performed)
        {
            if (!_inputContext.isPause)
                _inputContext.isAttack = performed;
        }
    }
}