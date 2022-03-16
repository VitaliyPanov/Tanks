using System;
using UnityEngine;

namespace Tanks.General.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }
        event Action<bool> OnAttackButtonEvent;
        event Action<bool> OnToggleNextEvent;
        event Action<bool> OnTogglePreviousEvent;
        event Action<bool> OnToggleMiniMapEvent;

    }
}