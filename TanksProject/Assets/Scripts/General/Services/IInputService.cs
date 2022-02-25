using System;
using UnityEngine;

namespace General.Services
{
    public interface IInputService
    {
        event Action<bool> OnAttackButtonEvent;
        event Action<bool> OnToggleNextEvent;
        event Action<bool> OnTogglePreviousEvent;
        Vector2 Axis { get; }
    }
}