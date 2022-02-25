using System;
using General.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TanksGB.Core.Infrastructure.Services.Input
{
    internal sealed class InputService : IInputService, IDisposable
    {
        private readonly InputControls _input;
        public event Action<bool> OnAttackButtonEvent;
        public event Action<bool> OnToggleNextEvent;
        public event Action<bool> OnTogglePreviousEvent;
        public event Action<bool> OnAimButtonEvent;
        public Vector2 Axis => _input.Tank.Move.ReadValue<Vector2>();


        public InputService()
        {
            _input = new InputControls();
            SetTankControls();
        }

        private void SetTankControls()
        {
            _input.Tank.Enable();
            _input.Tank.Attack.performed += AttackButton;
            _input.Tank.Attack.canceled += AttackButton;
            _input.Tank.ToggleNextTank.performed += ToggleNextButtonDown;
            _input.Tank.ToggleNextTank.canceled += ToggleNextButtonDown;
            _input.Tank.TogglePreviousTank.performed += TogglePreviousButtonDown;
            _input.Tank.TogglePreviousTank.canceled += TogglePreviousButtonDown;
            _input.Tank.Aiminig.performed += AimButton;
            _input.Tank.Aiminig.canceled += AimButton;
        }

        private void AimButton(InputAction.CallbackContext obj) => OnAimButtonEvent?.Invoke(obj.performed);

        private void ToggleNextButtonDown(InputAction.CallbackContext obj) => OnToggleNextEvent?.Invoke(obj.performed);

        private void TogglePreviousButtonDown(InputAction.CallbackContext obj) => OnTogglePreviousEvent?.Invoke(obj.performed);

        private void AttackButton(InputAction.CallbackContext obj) => OnAttackButtonEvent?.Invoke(obj.performed);

        public void Dispose()
        {
            _input.Tank.Attack.performed -= AttackButton;
            _input.Tank.Attack.canceled -= AttackButton;
            _input.Tank.ToggleNextTank.performed -= ToggleNextButtonDown;
            _input.Tank.ToggleNextTank.canceled -= ToggleNextButtonDown;
            _input.Tank.TogglePreviousTank.performed -= TogglePreviousButtonDown;
            _input.Tank.TogglePreviousTank.canceled -= TogglePreviousButtonDown;
            _input.Tank.Aiminig.performed -= AimButton;
            _input.Tank.Aiminig.canceled -= AimButton;
            _input?.Dispose();
        }
    }
}